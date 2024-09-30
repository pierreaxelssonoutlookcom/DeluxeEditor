using Model;
using Model.Interface;
using Shared;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Extensions;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;
using DeluxeEdit.DefaultPlugins.Views;
using System.Windows;
using System.Threading.Tasks;
using CustomFileDialogs;
using System.Reflection.Metadata;

namespace DefaultPlugins
{
    public class FileSaveAsPlugin : INamedActionPlugin
    {
        public bool ParameterIsSelectedText { get; set; } = false;




        public  string VersionString { get; set; } = "0.2";

        public Version Version { get; set; }= new Version();

 
        public ActionParameter? Parameter { get; set; } = new ActionParameter(); 

        public Stream? InputStream { get; set; } = null;

        public bool Enabled { get; set; } 



        private StreamWriter? writer;

        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "FileSavePlugin";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }


        public ConfigurationOptions Configuration { get; set; } = new ConfigurationOptions();
        public string Path { get; set; } = "";


        public void SetConfig()
        {
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "Save As";
            Configuration.KeyCommand.Keys = new List<Key> {  Key.LeftShift, Key.LeftCtrl, Key.S };
            Version=Version.Parse(VersionString ?? "0.0" );

        }

        public FileSaveAsPlugin()
        {
            SetConfig();
        }
        public object CreateControl(bool showToo)
        {
            object view = new MainEdit();
            Window? win = null;
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

        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {

            string oldDir = @"c:\";
            if (Parameter!=null)
                oldDir= new DirectoryInfo(Parameter.Parameter).FullName;
            var   dialog = new DeluxeFileDialog();


            var result = dialog.ShowFileSaveDialog(oldDir);
            return result;
        }
        public async Task<IEnumerable<string>> Perform(IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException(); 
            if (!File.Exists(Parameter.Parameter)) throw new FileNotFoundException(Parameter.Parameter);

            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                InputStream = mmf.CreateViewStream();
                writer = OpenEncoding == null ? new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }

            WritesAllPortions(progress);
            var result = new List<string>();
            return result;
        }


        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progress)
        {
            if (parameter == null) throw new ArgumentNullException();

            Parameter = parameter;
            if (!File.Exists(parameter.Parameter)) throw new FileNotFoundException(parameter.Parameter);
             
            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter);
                InputStream = mmf.CreateViewStream();
                writer = OpenEncoding == null ? new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }

            WritesAllPortions(progress);


            return String.Empty;
        }
        public void WritesAllPortions(IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException();

            if (!File.Exists(Parameter.Parameter)) throw new FileNotFoundException(Parameter.Parameter);
            if (writer == null) { }
            if (writer == null)
            {
                if (Parameter == null) throw new ArgumentNullException();

                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                InputStream = mmf.CreateViewStream();
                writer = OpenEncoding == null ? new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }
            for (int i = 0; i < Parameter.InData.Count / SystemConstants.ReadPortionBufferSizeLines; i++)
            {
                var batch = Parameter.InData.Take(SystemConstants.ReadPortionBufferSizeLines).ToList();
                WritePortion(batch, progress);
}



        }


        public async void WritePortion(List<string> indata, IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException();

            if (!File.Exists(Parameter.Parameter)) throw new FileNotFoundException(Parameter.Parameter);




            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                InputStream = mmf.CreateViewStream();
                writer = OpenEncoding == null ? new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }


            int lineCount = await writer.WriteLinesMax(indata, SystemConstants.ReadPortionBufferSizeLines);
            if (progress != null) progress.Report(lineCount);

            await writer.FlushAsync();

        }

    }





}

