using DeluxeEdit.Model.Interface;
using DeluxeEdit.Model;
using System;
using System.Net;

namespace DeluxeEdit.DefaultPlugins
{
    public class UrlEncodePlugin :  INamedActionPlugin
    {
        public object? Control { get; set; }
        public Type? ControlType { get; set; } = null;
        public string GuiAction(INamedActionPlugin instance) { return ""; }

        public bool Enabled { get; set; }

        public char[] MyKeyCommand { get; set; } = new char[0];

        public string Name { get; set; } = "UrlEncode";
        public string Titel { get; set; } = "Url Eeclode";
        public string Result { get; set; } = "";

        public ActionParameter Parameter { get; set; }

        public PresentationOptions PresentationOptions { get; set; }
        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";

        public UrlEncodePlugin()
        {
            PresentationOptions = new PresentationOptions();
            Parameter=new ActionParameter();
        }

        public string Perform(ActionParameter parameter)
        {
            Result = WebUtility.UrlEncode(parameter.Parameter);
            return Result;
        }
    }


}
