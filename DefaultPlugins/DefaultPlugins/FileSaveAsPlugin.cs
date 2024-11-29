using Model;
using Shared;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Extensions;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomFileApiFile;

namespace DefaultPlugins
{
    public class FileSaveAsPlugin : INamedActionPlugin
    {
        public const string VersionString = "0.2";

        public bool ParameterIsSelectedText { get; set; } = false;





        public Version Version { get; set; } = new Version(VersionString);


        public ActionParameter? Parameter { get; set; } = new ActionParameter(); 

        public Stream? InputStream { get; set; } = null;

        public bool Enabled { get; set; } 



        private StreamWriter? writer;

        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }


        public ConfigurationOptions Configuration { get; set; } = new ConfigurationOptions();
        public string Path { get; set; } = "";
        public Type Id { get; set; } = typeof(FileSaveAsPlugin);


        public void SetConfig()
        {
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "Save As";
            Configuration.KeyCommand.Keys = new List<Key> {  Key.LeftShift, Key.LeftCtrl, Key.S };

        }

        public FileSaveAsPlugin()
        {
            SetConfig();
        }
        public object CreateControl(bool showToo)
        {
            return new object();
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

            WritesAllPortions(progress);
            var result=await Task.FromResult(new List<string> { });
            return result;
        }


        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progress)
        {

            Parameter = parameter;
            var result = await Task.FromResult(new List<string> { });

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

