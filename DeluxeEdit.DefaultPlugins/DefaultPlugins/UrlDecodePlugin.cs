using DeluxeEdit.Model.Interface;
using DeluxeEdit.Model;
using System;
using System.Net;
using System.Text;
using System.Web;

namespace DeluxeEdit.DefaultPlugins
{
    public class UrlDecodePlugin : INamedActionPlugin
    {
        public object? Control { get; set; }
        public Type? ControlType { get; set; } = null;

        
        public string? GuiAction(INamedActionPlugin instance) { return ""; }
        public bool Enabled { get; set; }
        public char[] MyKeyCommand { get; set; } = new char[0];
        public string Id { get; set; } ="UrlDeclode";
        public string Titel { get; set; } =  "Url Declode";
        public PresentationOptions PresentationOptions { get; set; }

        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";

        public UrlDecodePlugin()
        {
            PresentationOptions= new PresentationOptions(); 
        }
        public string Perform(ActionParameter parameter)
        {   
            var result = HttpUtility.UrlDecode(  parameter.Parameter,  Encoding.UTF8);
            return result;
        }




    }


}
