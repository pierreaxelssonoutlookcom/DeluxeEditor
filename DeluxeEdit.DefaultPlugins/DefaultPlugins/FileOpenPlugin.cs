using DeluxeEdit.DefaultPlugins.GuiActions;
using DeluxeEdit.Model;
using DeluxeEdit.Model.Interface;
using DeluxeEdit.Shared;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DeluxeEdit.Extensions;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;

namespace DeluxeEdit.DefaultPlugins
{
    public class FileOpenPlugin : INamedActionPlugin
    {
         public object? Control { get; set; }
        public Type? ControlType{ get; set; }=typeof(DeluxeEdit.DefaultPlugins.Views.MainEdit);
        public string? GuiAction(INamedActionPlugin instance)
        {
            var x = new FileOpenGuiAction();
            var result= x.GuiAction(instance);
            return result;
        }

      

        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }

        public char[] MyKeyCommand { get; set; } = new char[] { SystemConstants.ControlKey, 'o' };
        private StreamReader? reader;
        public bool CanReadMore()
        {
            var result = false;
            if (reader != null) result = reader.BaseStream.CanRead;
            return result;
        }

        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }

        private List<string> resultBuffer;

        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";
 


        public FileOpenPlugin()
        {
            resultBuffer = new List<string>();
          //  OpenEncoding = Encoding.UTF8;
            Configuration = new ConfigurationOptions();
            Configuration.KeyCommand = new List<Key> { Key.LeftCtrl, Key.O }; 
        }

        public string Perform(ActionParameter parameter)
        {
            resultBuffer.Clear();
            var result = ReadPortion(parameter);
            if (resultBuffer.Count > SystemConstants.ReadBufferSizeLines) resultBuffer.Clear();

            
            resultBuffer.AddRange(result);
            return String.Join(Environment.NewLine, result);
        } 
        public List<string> ReadPortion(ActionParameter parameter)
        {
            var result = new List<string>();



            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter);
                var stream = mmf.CreateViewStream();
                reader = OpenEncoding == null ? reader = new StreamReader(stream, true) : new StreamReader(stream, OpenEncoding);
            }
            

            if (!File.Exists(parameter.Parameter)) throw new FileNotFoundException(parameter.Parameter);
          result=  reader.ReadLinesMax(SystemConstants.ReadPortionBufferSizeLines).ToList();

            return result;
        }

    }



}
