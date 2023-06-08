using DeluxeEdit.Model;
using DeluxeEdit.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace DeluxeEdit.DefaultPlugins
{
    public class UrlDecodePlugin : INamedActionPlugin
    {
        public object? Control { get; set; }
        public Type? ControlType { get; set; } = null;

        
        public string GuiAction(INamedActionPlugin instance) { return ""; }
        public bool Enabled { get; set; }
        public char[] MyKeyCommand { get; set; } = new char[0];
        public string Name { get; set; } ="UrlDeclode";
        public string Titel { get; set; } =  "Url Declode";
        public PresentationOptions PresentationOptions { get; set; }
        public ActionParameter Parameter { get; set; }

        public string Result { get; set; } = "";


        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";

        public UrlDecodePlugin()
        {
            Parameter = new ActionParameter();
            PresentationOptions= new PresentationOptions(); 
        }
        public string Perform(ActionParameter parameter)
        {   
         
            Result = WebUtility.UrlDecode(parameter.Parameter);
            return Result;
        }




    }


}
