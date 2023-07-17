using Model.Interface;
using Model;
using System;
using System.Net;
using System.Text;
using System.Web;

namespace DefaultPlugins
{
    public class UrlDecodePlugin : INamedActionPlugin
    {
        public ActionParameter Parameter { get; set; }

        public object? Control { get; set; }

        public Type? ControlType { get; set; } = null;

        
        public EncodingPath? GuiAction(INamedActionPlugin instance) { return null; }
        public bool Enabled { get; set; }
        public char[] MyKeyCommand { get; set; } = new char[0];
        public string Id { get; set; } ="UrlDeclode";
        public string Titel { get; set; } =  "Url Declode";
        public ConfigurationOptions Configuration { get; set; }

        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";

        public UrlDecodePlugin()
        {
            Configuration= new ConfigurationOptions(); 
        }
        public string Perform(ActionParameter parameter, string indata)
        {   
            var result = HttpUtility.UrlDecode(  indata ,  Encoding.UTF8);
            return result;
        }

        


    }


}
