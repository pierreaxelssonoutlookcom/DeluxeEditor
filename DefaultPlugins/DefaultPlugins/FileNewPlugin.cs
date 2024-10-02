using Model;
using Model.Interface;
using DeluxeEdit.DefaultPlugins.Views;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using CustomFileApiFile;

namespace DefaultPlugins
{
    public class FileNewPlugin : INamedActionPlugin
    {

        public bool ParameterIsSelectedText { get; set; } = false;


        public Version Version { get; set; }
        public string VersionString { get; set; } = "0.2";


        public long FileSize { get; set; }
        public long BytesRead { get; set; }

        public ActionParameter? Parameter { get; set; } = new ActionParameter();
       
        public bool Enabled { get; set; }

        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "FileNewPlugin";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }

        public List<string> ContentBuffer { get; set; } = new List<string>();
        public ConfigurationOptions Configuration { get; set; }= new ConfigurationOptions();
        public string Path { get; set; } = "";

        private  StringReader reader;

 

        public FileNewPlugin()
        {
            SetConfig();
        }
        public object CreateControl(bool showToo)
        {
            return new object();  
        }
       public  void SetConfig()
        {
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "New";
            Configuration.KeyCommand.Keys = new List<Key> { Key.LeftCtrl, Key.N };
            Version = Version.Parse(VersionString ?? "0.0");

        }

        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            string oldDir = @"c:\";

            if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog = new DeluxeFileDialog();
            var result = dialog.ShowFileOpenDialog(oldDir);
            return result;
        }

        public async Task<IEnumerable<string>> Perform(IProgress<long> progress)
        {
            return null;
        }






        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progress)
        {
            return String.Empty;
        }

        public string MenuAction(ActionParameter parameter)
        {
            throw new NotImplementedException();
        }


    
    }



}
