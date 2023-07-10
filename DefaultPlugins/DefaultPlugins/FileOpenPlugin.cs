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
        public ActionParameter? Parameter { get; set; }

        public object? Control { get; set; }
        public Type? ControlType{ get; set; }=typeof(DefaultPlugins.Views.MainEdit);
        public long FileSize { get; set; }

        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }

        private StreamReader? reader;
        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }

        public List<string> ContentBuffer;

        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";



        public string? GuiAction(INamedActionPlugin instance)
        {
            string oldDir =@"c:\";

            if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog= new DeluxeFileDialog();
            var result=dialog.ShowFileOpenDialog(oldDir);
            return null;
        }


        public bool CanReadMore()
        {
            var result = false;
            if (reader != null) result = reader.BaseStream.CanRead;
            return result;
        }

 

        public FileOpenPlugin()
        {
            ContentBuffer = new List<string>();
          //  OpenEncoding = Encoding.UTF8;
            Configuration = new ConfigurationOptions();
            Configuration.KeyCommand = new List<Key> { Key.LeftCtrl, Key.O }; 
        }

        public string Perform(ActionParameter parameter)
        {
              Parameter=parameter;
            FileSize = new FileInfo(Path).Length;


            ContentBuffer.Clear();
            var result = ReadPortion(parameter);
            if (ContentBuffer.Count > SystemConstants.ReadBufferSizeLines) ContentBuffer.Clear();

            
            ContentBuffer.AddRange(result);
            return String.Join(Environment.NewLine, result);
        } 
        public List<string> ReadPortion(ActionParameter parameter)
        {
            var result = new List<string>();


            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter);
                var stream = mmf.CreateViewStream();
                reader = OpenEncoding == null ? reader = new StreamReader(stream, true) : new StreamReader(stream, OpenEncoding);
            }
            

            if (!File.Exists(parameter.Parameter)) throw new FileNotFoundException(parameter.Parameter);

            result =  reader.ReadLinesMax(SystemConstants.ReadPortionBufferSizeLines).ToList();

            return result;
        }

    }



}
