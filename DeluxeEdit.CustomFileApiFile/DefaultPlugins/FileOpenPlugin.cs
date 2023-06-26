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
    
        public PresentationOptions PresentationOptions { get; set; }
        public string Path { get; set; } = "";
 


        public FileOpenPlugin()
        {
          //  OpenEncoding = Encoding.UTF8;
            PresentationOptions = new PresentationOptions();
        }

        public string Perform(ActionParameter parameter)
        {
            var result = ReadPortion(parameter);
            return result;
        }
        public string ReadPortion(ActionParameter parameter)
        {
            if (reader==null)
            {
            if (OpenEncoding==null)
               reader= new StreamReader(new FileStream(parameter.Parameter, FileMode.Open), true  );
            else
               reader = new StreamReader(new FileStream(parameter.Parameter, FileMode.Open), OpenEncoding);

            }
            if (!File.Exists(parameter.Parameter)) throw new FileNotFoundException(parameter.Parameter);

            var buffy = new char[SystemConstants.FileBufferSize];
            reader.ReadBlock(buffy);
            int idx=buffy.IndexOf('\0');
            var result = new String(buffy, 0, 
                idx); 
            return result;
        }

    }



}
