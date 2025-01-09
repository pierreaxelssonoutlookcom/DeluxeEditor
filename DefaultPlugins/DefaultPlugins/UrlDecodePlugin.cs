using Model;
using System;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DefaultPlugins
{
    public class UrlDecodePlugin : INamedActionPlugin
    {
        public const string VersionString = "0.2";
              public bool ParameterIsSelectedText { get; set; } = true;
        public Version Version { get; set; } = new Version(VersionString);
        public ActionParameter Parameter { get; set; } = new ActionParameter();
        public bool Enabled { get; set; }
        public Type Id { get; set; } =typeof(UrlDecodePlugin);
        public ConfigurationOptions Configuration { get; set; } = new ConfigurationOptions();
        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";
        public UrlDecodePlugin()
        {
            SetConfig();


        }
        public void SetConfig()
        {
            Configuration.ShowInMenu = "Plugins";
            Configuration.ShowInMenuItem = "UrlDecode";

        }

        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progresss)
        {
            if (parameter == null) throw new ArgumentNullException();

            var dummy = await Task.FromResult(new List<string> { });


            var result = HttpUtility.UrlDecode( parameter.Parameter,  Encoding.UTF8);
            return result;
        }
        public async Task<IEnumerable<string>> Perform(IProgress<long> progresss)
        {
            if (Parameter == null) throw new ArgumentNullException();

            var dummy = await Task.FromResult(new List<string> { });

            var result = HttpUtility.UrlDecode(Parameter.Parameter, Encoding.UTF8);
            return new List<string> { result };


        }

        public object CreateControl(bool showToo)
        {
            return String.Empty;
        }
        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            return null;
        }


    }


}
