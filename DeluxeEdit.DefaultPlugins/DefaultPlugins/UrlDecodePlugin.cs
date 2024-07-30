using Model.Interface;
using Model;
using System;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace DefaultPlugins
{
    public class UrlDecodePlugin : INamedActionPlugin
    {

        public bool ParameterIsSelectedText { get; set; } = true;

        public object CreateControl(bool showToo)
        {
            return String.Empty;
        }


        public Version Version { get; set; }
        public string VersionString { get; set; } = "0.2";

        public ActionParameter Parameter { get; set; }


        
        public EncodingPath? GuiAction(INamedActionPlugin instance) { return null; }
        public bool Enabled { get; set; }
        public char[] MyKeyCommand { get; set; } = new char[0];
        public string Id { get; set; } = "UrlDecodePlugin";
        public string Titel { get; set; } =  "Url Declode";
        public ConfigurationOptions Configuration { get; set; }

        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";
        public UrlDecodePlugin()
        {
            Configuration = new ConfigurationOptions();
            Version = Version.Parse(VersionString);
            Configuration.ShowInMenu = "Plugins";
            Configuration.ShowInMenuItem = "UrlDecode";

        }
        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progresss)
        {
            var result = HttpUtility.UrlDecode( parameter.Parameter,  Encoding.UTF8);
            return result;
        }
        public async Task<IEnumerable<string>> Perform(IProgress<long> progresss)
        {
            var result = HttpUtility.UrlDecode(Parameter.Parameter, Encoding.UTF8);
            return new List<string> { result };


        }



    }


}
