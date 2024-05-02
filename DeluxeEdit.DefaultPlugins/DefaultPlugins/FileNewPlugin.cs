using Model;
using Model.Interface;
using System;
using System.IO;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;
using CustomFileApiFile;
using DeluxeEdit.DefaultPlugins.Views;
using System.Threading.Tasks;

namespace DefaultPlugins
{
    public class FileNewPlugin : INamedActionPlugin
    {

        public bool ParameterIsSelectedText { get; set; } = false;


        public Version Version { get; set; }
        public string VersionString { get; set; } = "0.2";


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
        public string Id { get; set; } = "FileNewPlugin";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }

        public List<string> ContentBuffer;
        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";


        public object CreateControl(bool showToo)
        {
            var result = new MainEdit();

            return result;
        }

        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            string oldDir =@"c:\";

            if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog= new DeluxeFileDialog();
            var result=dialog.ShowFileOpenDialog(oldDir);
            return null;
        }


        public bool CanReadMore { get { return reader.BaseStream.CanRead; } }
                

 

        public FileNewPlugin()
        { 
            ContentBuffer = new List<string>(); 
          //  OpenEncoding = Encoding.UTF8; m 
            Configuration = new ConfigurationOptions();
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "New"; 
            Configuration.KeyCommand.Keys =  new List<Key> { Key.LeftCtrl, Key.N };
            Version = Version.Parse(VersionString);
        }

        public async Task<string> Perform(ActionParameter parameter)
        {
            return "";
          
        }

        public string MenuAction(ActionParameter parameter)
        {
            throw new NotImplementedException();
        }


    
    }



}
