using Model;
using Model.Interface;
using Shared;
using System;
using System.IO;
using System.Text;
using System.Windows.Controls;
using Extensions;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;
using CustomFileApiFile;
using DeluxeEdit.DefaultPlugins.Views;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Shapes;
using static System.Windows.Forms.LinkLabel;
using System.Threading;

namespace DefaultPlugins
{
    public class FileOpenPlugin : INamedActionPlugin
    {

        public bool ParameterIsSelectedText { get; set; } = false;

        public object CreateControl(bool showToo)
        {
            object view = new MainEdit();
            Window win = null;
            var result = view;
            if (showToo)
            {
                win = new Window();
                result = win;

                win.Content = view;
                win.Show();

            }

            return result;
        }

        public Version Version { get; set; }

        public long FileSize { get; set; }
        public long BytesRead { get; set; }

        public ActionParameter? Parameter { get; set; }


    
        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }
        public Stream InputStream { get; set; }

        private MemoryMappedViewStream MýStream;
        private StreamReader? reader;
        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "FileOpenPlugin";
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
            return result;
        }


        public bool CanReadMore { get { return reader.BaseStream.CanRead; } }
                

 

        public FileOpenPlugin()
        {
            ContentBuffer = new List<string>(); 
          //  OpenEncoding = Encoding.UTF8; m 
            Configuration = new ConfigurationOptions();
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "Open"; ;
            Configuration.KeyCommand.Keys =  new List<Key> { Key.LeftCtrl, Key.O };
            Version =   Version.Parse("0.1");
        }

        public async Task<string> Perform(ActionParameter parameter)
        {
              Parameter=parameter;
            FileSize = File.Exists(parameter.Parameter) ?   new FileInfo(parameter.Parameter).Length: 0;


            ContentBuffer.Clear();
            var result = await ReadPortion(parameter);
            ContentBuffer.AddRange(result);
            return  String.Join(Environment.NewLine, result);
        }
        public async Task<List<string>> ReadAllPortions(ActionParameter parameter)
        {
             var result=new List<string>();

                var localResult = await ReadPortion(parameter);
                result.AddRange(localResult);

                while (localResult.Count>0 )
                {
                    localResult = await ReadPortion(parameter);
                    result.AddRange(localResult);
                }

 
                reader.Close();
                reader = null;
            return result;
        }

        public async Task<List<string>> ReadPortion(ActionParameter parameter)
        {
            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter);
                MýStream = mmf.CreateViewStream();
                reader = OpenEncoding == null ? reader = new StreamReader(MýStream, true) : new StreamReader(MýStream, OpenEncoding);
            }

            //todo:how do I share file data between different plugins

            if (!File.Exists(parameter.Parameter)) 
                throw new FileNotFoundException(parameter.Parameter); 
            var lines = new List<string>();

            for (int i = 0; i>SystemConstants.ReadPortionBufferSizeLines;i++) 
            { 
                string line = await reader.ReadLineAsync();
                if (line != null)
                    lines.Add(line);
                else
                    break;
            }


            return lines;




        }

    }
}
