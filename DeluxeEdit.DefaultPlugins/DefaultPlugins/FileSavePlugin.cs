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
using System.Windows.Markup.Localizer;
using DeluxeEdit.DefaultPlugins.Views;
using System.Windows.Forms;
using System.Windows;
using MS.WindowsAPICodePack.Internal;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Diagnostics;

namespace DefaultPlugins
{
    public class FileSavePlugin : INamedActionPlugin
    {
        public bool ParameterIsSelectedText { get; set; } = false;




        public string VersionString { get; set; } = "0.2";

        public Version Version { get; set; }

        public long FileSize { get; set; }
        public long BytesWritten { get; set; }

        public ActionParameter? Parameter { get; set; }

        

        public  Stream InputStream {  get; set; }
        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }
        //done:make dynamic
      



        private StreamWriter? writer;

        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "FileSavePlugin";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }


        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";



        public FileSavePlugin()
        {
            //  OpenEncoding = Encoding.UTF8;
            Configuration = new ConfigurationOptions();
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "Save";
            Configuration.KeyCommand.Keys = new List<Key> { Key.LeftCtrl, Key.S };
            Version = Version.Parse(VersionString);
        }
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

        public static FileSavePlugin CastNative(INamedActionPlugin item)
        {
            if (item is FileSavePlugin && item != null)
                return item as FileSavePlugin;
            else
                return null;


        }
        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            string oldDir = @"c:\";

            if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
        _:    var dialog = new DeluxeFileDialog();
            var result = dialog.ShowFileSaveDialog(oldDir);
            return result;
        }
        public async Task<IEnumerable<string>> Perform(IProgress<long> progress)
        {
            FileSize = File.Exists(Parameter.Parameter) ? new FileInfo(Parameter.Parameter).Length : 0;

            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                InputStream = mmf.CreateViewStream();
                writer = OpenEncoding == null ? new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }

            WritesAllPortions(progress);
            return null;

        }


        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progress )
        {
              Parameter = parameter;  
            FileSize = File.Exists(parameter.Parameter)? new FileInfo(parameter.Parameter).Length: 0;
            
            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter);
                InputStream= mmf.CreateViewStream();
                writer = OpenEncoding == null ?  new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }

            WritesAllPortions(progress);
                    
               

            return "";

        }
        public void WritesAllPortions(IProgress<long> progresss)
        {
            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                InputStream = mmf.CreateViewStream();
                writer = OpenEncoding == null ? new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }
            for (int i = 0; i < Parameter.InData.Count / SystemConstants.ReadPortionBufferSizeLines; i++)
            {
                var batch = Parameter.InData.Take(SystemConstants.ReadPortionBufferSizeLines).ToList();
                WritePortion(batch, progresss);

            }



        }


        public async void WritePortion(List<string> indata, IProgress<long> progress
            )
        {
            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                InputStream = mmf.CreateViewStream();
                writer = OpenEncoding == null ? new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }


            if (!File.Exists(Parameter.Parameter)) throw new FileNotFoundException(Parameter.Parameter);
            int lineCount= await writer.WriteLinesMax(indata, SystemConstants.ReadPortionBufferSizeLines);
          if (progress != null) progress.Report(lineCount);
                
            await writer.FlushAsync();

        }

    }





}

 