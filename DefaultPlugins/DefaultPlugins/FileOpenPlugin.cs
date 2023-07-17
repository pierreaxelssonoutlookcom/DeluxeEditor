using Model;
using Model.Interface;
using Shared;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Extensions;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;
using CustomFileApiFile;

namespace DefaultPlugins
{
    public class FileOpenPlugin : INamedActionPlugin
    {
        public long FileSize { get; set; }
        public long BytesRead { get; set; }

        public ActionParameter? Parameter { get; set; }

        public object? Control { get; set; }
        public Type? ControlType{ get; set; }=typeof(DefaultPlugins.Views.MainEdit);
    
        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }
        public Stream InputStream { get; set; }

        private MemoryMappedViewStream MýStream;
        private StreamReader? reader;
        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }

        public List<string> ContentBuffer;

        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";



        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            string oldDir =@"c:\";

            if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog= new DeluxeFileDialog();
            var result=dialog.ShowFileOpenDialog(oldDir);
            return null;
        }


        public bool CanReadMore { get { return reader.BaseStream.CanRead; } }
                

 

        public FileOpenPlugin()
        {
            ContentBuffer = new List<string>();
          //  OpenEncoding = Encoding.UTF8; m 
            Configuration = new ConfigurationOptions();
            Configuration.KeyCommand = new List<Key> { Key.LeftCtrl, Key.O }; 
        }

        public string Perform(ActionParameter parameter, string indata)
        {
              Parameter=parameter;
            FileSize = File.Exists(parameter.Parameter) ?   new FileInfo(parameter.Parameter).Length: 0;

            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter);
                MýStream = mmf.CreateViewStream();
                reader = OpenEncoding == null ? reader = new StreamReader(MýStream, true) : new StreamReader(MýStream, OpenEncoding);
            }


            var result = ReadAllPortions(parameter);

            ContentBuffer.Clear();
            if (ContentBuffer.Count > SystemConstants.ReadBufferSizeLines) ContentBuffer.Clear();

            
            ContentBuffer.AddRange(result);
            return String.Join(Environment.NewLine, result);
        }
        public List<string> ReadAllPortions(ActionParameter parameter)
        {
             var lastPortion=new List<string>();

            while (CanReadMore)
            {
                lastPortion=ReadPortion(parameter);
            }

            if (!CanReadMore)
            { 
                reader.Close();
                reader = null;
            }
            return lastPortion;
        }

        public List<string> ReadPortion(ActionParameter parameter)
        {
            //todo:how do I share file data between different plugins


            if (!File.Exists(parameter.Parameter)) throw new FileNotFoundException(parameter.Parameter);

            

            
            var linesRead=  reader.ReadLinesMax(SystemConstants.ReadPortionBufferSizeLines);
            BytesRead += linesRead.Bytes;
            


            return linesRead.Items;
        }

    }



}
