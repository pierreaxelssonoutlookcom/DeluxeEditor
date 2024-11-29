using Model;
using System;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DefaultPlugins
{
    public class UrlEncodePlugin :  INamedActionPlugin
    {
        public const string VersionString = "0.2";

        public bool ParameterIsSelectedText { get; set; } = true;


        public Version Version { get; set; }= new Version(VersionString);



        public ActionParameter? Parameter { get; set; } = new ActionParameter();

        public EncodingPath? GuiAction(INamedActionPlugin instance) { return null; }

        public bool Enabled { get; set; }

        public char[] MyKeyCommand { get; set; } = new char[0];

        public string Titel { get; set; } = "Url encode";

        public Type Id { get; set; } = typeof(UrlEncodePlugin);


        public ConfigurationOptions Configuration { get; set; }= new ConfigurationOptions();
        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";

        public UrlEncodePlugin()
        {
            SetConfig();


        }
        public void SetConfig()
        {
            Configuration.ShowInMenu = "Plugins";
            Configuration.ShowInMenuItem = "UrlEncode";
            Version = new Version(VersionString);

        }

        public object CreateControl(bool showToo)
        {
            return "";
        }






        public async Task<IEnumerable<string>> Perform(IProgress<long> progresss)
        {
            if (Parameter == null) throw new ArgumentNullException();
            var dummy = await Task.FromResult(new List<string> { });

            var result = HttpUtility.UrlEncode(Parameter.Parameter, Encoding.UTF8);
            return new List<string> { result };


        }





        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progresss)
        {
            if (parameter == null) throw new ArgumentNullException();
            var dummy = await Task.FromResult(new List<string> { });



            var result = HttpUtility.UrlEncode(parameter.Parameter, Encoding.UTF8);
            return result;
        }

    }


}
