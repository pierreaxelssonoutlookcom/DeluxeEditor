using Model.Interface;
using Model;
using System;
using System.Net;
using System.Web;
using System.Text;

namespace DefaultPlugins
{
    public class UrlEncodePlugin :  INamedActionPlugin
    {
        public ActionParameter Parameter { get; set; }

        public object? Control { get; set; }
        public Type? ControlType { get; set; } = null;
        public EncodingPath? GuiAction(INamedActionPlugin instance) { return null; }

        public bool Enabled { get; set; }

        public char[] MyKeyCommand { get; set; } = new char[0];

        public string Id { get; set; } = "UrlEncode";
        public string Titel { get; set; } = "Url Eeclode";
     

        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";

        public UrlEncodePlugin()
        {
            Configuration = new ConfigurationOptions();
        }

        public string Perform(ActionParameter parameter, string indata)
        {
            var result = HttpUtility.UrlEncode( indata, Encoding.UTF8);
            return result;
        }
    }


}
