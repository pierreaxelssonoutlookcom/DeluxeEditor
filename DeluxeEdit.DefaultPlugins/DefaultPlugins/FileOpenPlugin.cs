using Model;
using Model.Interface;
using Shared;
using System;
using System.IO;
using System.Text;
using Extensions;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;
using DeluxeEdit.DefaultPlugins.Views;
using System.Windows;
using System.Threading.Tasks;
using CustomFileDialogs;

namespace DefaultPlugins
{
    public class FileOpenPlugin : INamedActionPlugin
    {

        public bool ParameterIsSelectedText { get; set; } = false;


        public Version Version { get; set; }
        public string VersionString { get; set; } = "0.2";


        public long FileSize { get; set; }
        public long BytesRead { get; set; }

        public ActionParameter? Parameter { get; set; }


    
        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }

        private MemoryMappedViewStream MýStream;
        private StreamReader? reader;
        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "FileOpenPlugin";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }
            
        public ConfigurationOptions Configuration { get; set; } = new ConfigurationOptions();
       public string Path { get; set; } = "";
        public long GetFileLeLength(ActionParameter parameter)
         {
            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter);
                MýStream = mmf.CreateViewStream();
                reader = OpenEncoding == null ? reader = new StreamReader(MýStream, true) : new StreamReader(MýStream, OpenEncoding);
            }
            long result = MýStream.Length;
            return result;


        }



        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            string oldDir =@"c:\";

            if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog= new DeluxeFileDialog();
            var result=dialog.ShowFileOpenDialog(oldDir);
            return result;
        }



        public object CreateControl(bool showToo)
        {
            object view = new MainEdit();
            var result = view;
            if (showToo)
            {
                var win = new Window();
                result = win;

                win.Content = view;
                win.Show();

            }

            return result;
        }


        public void SetConfig()
        {
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "Open"; ;
            Configuration.KeyCommand.Keys = new List<Key> { Key.LeftCtrl, Key.O };
            Version = Version.Parse(VersionString);
        }
        public FileOpenPlugin()
        {
            SetConfig();        }
        public static FileOpenPlugin CastNative(INamedActionPlugin item)
        { 
            if (item is FileOpenPlugin)
                return item as FileOpenPlugin;
            else
                return null;
        }
        public async Task<IEnumerable<string>> Perform(IProgress<long> progresss)
        {
            var result = await ReadAllPortion(progresss);
            return result;
        }

        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progress)
        { 
            if (parameter == null) throw new ArgumentNullException();
            Parameter= parameter;

            var lines = await ReadAllPortion(progress);         
           return String.Join(Environment.NewLine, lines);

        }

        public async Task<List<string>> ReadAllPortion(IProgress<long> progresss ) 
        {
            var result = new List<string>();
            var total = new List<string>();

            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                MýStream = mmf.CreateViewStream();
                reader = OpenEncoding == null ? reader = new StreamReader(MýStream, true) : new StreamReader(MýStream, OpenEncoding);
            }
           
            
            while ((result = await ReadPortion(progresss)) != null)

            {
                total.AddRange(result); 
            }
return total;
        }
        public async Task<List<string>> ReadPortion(IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException();

            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                MýStream = mmf.CreateViewStream();
                reader = OpenEncoding == null ? reader = new StreamReader(MýStream, true) : new StreamReader(MýStream, OpenEncoding);
            }




            //todo:how do I share file data between different plugins
            if (Parameter == null) throw new ArgumentNullException();
            if (!File.Exists(Parameter.Parameter)) throw new FileNotFoundException(Parameter.Parameter);

            var result = await reader.ReadLinesMax(SystemConstants.ReadBufferSizeLines);
            
            if (progress != null)
                progress.Report(MýStream.Position);


            return result;




        }

    }
}
