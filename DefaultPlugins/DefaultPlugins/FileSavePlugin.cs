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

namespace DefaultPlugins
{
    public class FileSavePlugin : INamedActionPlugin
    {
         public long FileSize { get; set; }

        public ActionParameter? Parameter { get; set; }

        public object? Control { get; set; }
        public Type? ControlType { get; set; } = typeof(DefaultPlugins.Views.MainEdit);
        public string? GuiAction(INamedActionPlugin instance)
        {
            string oldDir = @"c:\";

            if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog = new DeluxeFileDialog();
            var result = dialog.ShowFileSaveDialog(oldDir);
            return null;
        }



        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }

        private StreamWriter? writer;

        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "";
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
            Configuration.KeyCommand = new List<Key> { Key.LeftCtrl, Key.S };
        }
        public string Perform(ActionParameter parameter)
        {
            Parameter = parameter;
            FileSize = new FileInfo(Path).Length;

            WritePortion(parameter);
            if (ContentBuffer.Count > SystemConstants.ReadBufferSizeLines) ContentBuffer.Clear();

            return "";

        }
        public void WritePortion(ActionParameter parameter)
        {


            if (writer == null)
            {
                using (var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter))
                {
                    var stream = mmf.CreateViewStream();
                    writer = OpenEncoding == null ? writer = new StreamWriter(stream) : new StreamWriter(stream, OpenEncoding);

                }
            }


            if (!File.Exists(parameter.Parameter)) throw new FileNotFoundException(parameter.Parameter);

            writer.WriteLinesMax(ContentBuffer, SystemConstants.ReadPortionBufferSizeLines);
            writer.Flush();
            writer.Close();
        }

    }

}
    