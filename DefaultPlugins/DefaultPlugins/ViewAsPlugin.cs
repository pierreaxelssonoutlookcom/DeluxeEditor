using Model;
using System;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Generic;
using ViewModel;
using System.Linq;
using DefaultPlugins.DefaultPlugins.PluginHelpers;

namespace DefaultPlugins
{
    public class ViewAsPlugin : INamedActionPlugin
    {
        public const string VersionString = "0.1";
        private FileTypeLoader fileTypeLoader;

        public bool ParameterIsSelectedText { get; set; } = true;
        public Version Version { get; set; } = new Version(VersionString);
        public ActionParameter Parameter { get; set; } = new ActionParameter();
        public bool Enabled { get; set; }
        public Type Id { get; set; } =typeof(ViewAsPlugin);
        public ConfigurationOptions Configuration { get; set; } = new ConfigurationOptions();
        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";
        public ViewAsPlugin()
        {
            fileTypeLoader = new FileTypeLoader();
            SetConfig();


        }

        public void SetConfig()
        {
            Configuration.ShowInMenu = "View";
            Configuration.ShowInMenuItem = "View As";

        }

         public List<CustomMenuItem> GetSubMenuItemsForFileTypes()
        {
            var result = fileTypeLoader.GetFileTypes().Select(p =>
            new CustomMenuItem { Title = p.ToString(), FileType = p.FileType, IsCheckable = true, IsChecked = false, Plugin=this }).ToList();
            return result;
        }

        private async Task<string> InternalDoIt()
        {
            var result = String.Empty;
            SetSelectedPath(Parameter.Parameter);
            await Task.Delay(0);
        return result;

        }
        public void SetSelectedPath(string path)
        {
            var MenuItemsForFileTypes=GetSubMenuItemsForFileTypes();
            fileTypeLoader.LoadCurrent(path);
            var withTypes = MenuItemsForFileTypes.Where(p => p.FileType != null);
            if (fileTypeLoader.CurrentFileItem != null)
            {

                var match = withTypes.First(p => p.FileType == fileTypeLoader.CurrentFileItem.FileType);
                match.IsChecked = true;
            }




        }
        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progresss)
        {
            if (parameter == null) throw new ArgumentNullException();

            var dummy = await Task.FromResult(new List<string> { });
            Parameter = parameter;

            return await InternalDoIt();
             }
        public async Task<IEnumerable<string>> Perform(IProgress<long> progresss)
        {
            if (Parameter == null) throw new ArgumentNullException();

            return new List<string> { await InternalDoIt() };
 

        }

        public object CreateControl(bool showToo)
        {
            return new object();

        }
        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            return null;
        }


    }


}
