using DeluxeEdit.DefaultPlugins.GuiActions;
using DeluxeEdit.Model;
using DeluxeEdit.Model.Interface;
using DeluxeEdit.Shared;
using System;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace DeluxeEdit.DefaultPlugins
{
    public class FileOpenPlugin : INamedActionPlugin
    {
         public object? Control { get; set; }
        public Type? ControlType{ get; set; }=typeof(MainEdit);
        public string GuiAction(INamedActionPlugin instance)
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
        public Encoding OpenEncoding { get; set; }
        public string Name { get; set; } = "";
        public string Titel { get; set; } = "";
        public int SortOrdder { get; set; }
        public ActionParameter Parameter { get; set; } 
        public string Result { get; set; } = "";
            
        public PresentationOptions PresentationOptions { get; set; }
        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";


        public FileOpenPlugin()
        {
            OpenEncoding = Encoding.UTF8;
            PresentationOptions = new PresentationOptions();
           Parameter = new ActionParameter();
        }

        public string Perform(ActionParameter parameter)
        {
            var result = ReadPortion(parameter);
            return result;
        }
        public string ReadPortion(ActionParameter parameter)
        {
            string? result;

            if (!File.Exists(parameter.Parameter)) throw new FileNotFoundException(parameter.Parameter);
            reader = reader ?? new StreamReader(new FileStream(parameter.Parameter, FileMode.Open));

            var buffy = new char[SystemConstants.FileBufferSize];
            reader.ReadBlock(buffy);
            result= reader.ToString();
            return result ?? "";
        }

    }


}
