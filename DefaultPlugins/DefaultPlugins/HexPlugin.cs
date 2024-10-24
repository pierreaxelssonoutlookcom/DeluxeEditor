using Model;
using Model.Interface;
using DeluxeEdit.DefaultPlugins.Views;
using System;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared;
using Extensions;
using CustomFileApiFile;
using System.Windows;
using DefaultPlugins.Views;
using System.Linq;

namespace DefaultPlugins
{
    public class HexPlugin : INamedActionPlugin
    {

        public bool ParameterIsSelectedText { get; set; } = false;


        public Version Version { get; set; } = new Version();
        public string VersionString { get; set; } = "0.2";


        public ActionParameter? Parameter { get; set; } = new ActionParameter();

        public bool Enabled { get; set; }

        private MemoryMappedViewStream? MýStream = null;
        private StreamReader? reader;
        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "FileOpenPlugin";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }

        public ConfigurationOptions Configuration { get; set; } = new ConfigurationOptions();
        public string Path { get; set; } = "";


        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            string oldDir = @"c:\";

            //if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog = new DeluxeFileDialog();
            var result = dialog.ShowFileOpenDialog(oldDir);
            return result;
        }



        public object CreateControl(bool showToo)
        {
            object view = new Hex();
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
            Configuration.ShowInMenuItem = "Hex"; ;
            Configuration.KeyCommand.Keys = new List<Key> { Key.LeftCtrl, Key.O };
            Version = Version.Parse(VersionString);
        }
        public HexPlugin()
        {
            SetConfig();
        }


        public async Task<IEnumerable<string>> Perform(IProgress<long> progress)
        {
            var result = await ReadAllPortion(progress);
            return result;
        }

        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progress)
        {
            if (parameter == null) throw new ArgumentNullException();
            Parameter = parameter;

            var lines = await ReadAllPortion(progress);
            return String.Join(Environment.NewLine, lines);

        }

        public async Task<List<string>> ReadAllPortion(IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException();
            if (File.Exists(Parameter.Parameter) == false) throw new FileNotFoundException(Parameter.Parameter);

            var total = new List<string>();

            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                MýStream = mmf.CreateViewStream();

                
                reader = OpenEncoding == null ? reader = new StreamReader(MýStream, true) : new StreamReader(MýStream, OpenEncoding);
            }
            if (MýStream == null) throw new ArgumentNullException();

            long fileSize = new FileInfo(Parameter.Parameter).Length;
            for (int i = 0; i <= fileSize/ SystemConstants.ReadBufferSizeBytes; i++)
            {
                var result = await ReadPortion(progress);
                if (result != null)
                    total.AddRange(result);

            }
            return total;
        }
        public async Task<List<string>?> ReadPortion(IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException();
           
            if (File.Exists(Parameter.Parameter) == false) throw new FileNotFoundException(Parameter.Parameter);

            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                MýStream = mmf.CreateViewStream();
                reader = OpenEncoding == null ? reader = new StreamReader(MýStream, true) : new StreamReader(MýStream, OpenEncoding);
            }

            if (MýStream == null) throw new ArgumentNullException();

            var oldBuffer = new byte[SystemConstants.ReadBufferSizeBytes];
            var readCount = await MýStream.ReadAsync(oldBuffer);
            var buffer = oldBuffer.Take(readCount).ToArray();

            var result = new List<string>();
            var sb= new StringBuilder();
            foreach (byte b in buffer)
            {
               if (b == '\0') break;
                           
                sb.AppendFormat("{0:x2}", b);
            }
            
           result.Add (sb.ToString());

            //todo:how do I share file data between different plugins
            if (progress != null)
                progress.Report(MýStream.Position);


            return result;
        
           


        }

 }
}
