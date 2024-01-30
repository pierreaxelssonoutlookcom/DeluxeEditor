using Model;
using Model.Interface;
using Shared;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DeluxeEdit.Extensions;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;
using CustomFileApiFile;
using System.Windows.Markup.Localizer;

namespace DefaultPlugins
{
    public class FileSavePlugin : INamedActionPlugin
    {

        public Version Version { get; set; }

        public long FileSize { get; set; }
        public long BytesWritten { get; set; }

        public ActionParameter? Parameter { get; set; }

        public object? Control { get; set; }
        public Type? ControlType { get; set; } = typeof(DefaultPlugins.Views.MainEdit);
        

        public  Stream InputStream {  get; set; }
        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }
        //done:make dynamic
        public bool CanWriteMore { get { return FileSize != 0 && BytesWritten < FileSize && ContentBuffer.Count > 0; } }



        private StreamWriter? writer;

        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "FileSavePlugin";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }

        public List<string> ContentBuffer;

        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";



        public FileSavePlugin()
        {
            ContentBuffer = new List<string>();
            //  OpenEncoding = Encoding.UTF8;
            Configuration = new ConfigurationOptions();
            Configuration.ShowInMenu = "&File";
            Configuration.ShowInMenuItem = "&Save";
            Configuration.KeyCommand = new List<Key> { Key.LeftCtrl, Key.S };
            Version = Version.Parse("0.1");
        }
        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            string oldDir = @"c:\";

            if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog = new DeluxeFileDialog();
            var result = dialog.ShowFileSaveDialog(oldDir);
            return result;
        }

        public string Perform(ActionParameter parameter)
        {
            ContentBuffer = parameter.InData.Split(Environment.NewLine).ToList();
              Parameter = parameter;
            FileSize = File.Exists(parameter.Parameter)? new FileInfo(parameter.Parameter).Length: 0;
            
            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter);
                InputStream= mmf.CreateViewStream();
                writer = OpenEncoding == null ?  new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }

            WritesAllPortions(parameter);
            if (ContentBuffer.Count > SystemConstants.ReadBufferSizeLines) ContentBuffer.Clear();

            return "";

        }
        public void WritesAllPortions(ActionParameter parameter)
        {
       
            while (CanWriteMore)
            {
               WritePortion(parameter);
            }

            if (!CanWriteMore)
            {
                writer.Close();
                writer = null;
            }
            
        }

        public void WritePortion(ActionParameter parameter)
        {

            if (!File.Exists(parameter.Parameter)) throw new FileNotFoundException(parameter.Parameter);
            long bytes= writer.WriteLinesMax(ContentBuffer, SystemConstants.ReadPortionBufferSizeLines);
            BytesWritten += bytes;
            writer.Flush();

        }

    }

}
    