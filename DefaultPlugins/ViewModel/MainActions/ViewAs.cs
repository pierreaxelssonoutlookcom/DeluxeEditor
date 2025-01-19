using DefaultPlugins.DefaultPlugins.PluginHelpers;
using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace DefaultPlugins.ViewModel.MainActions
{
    public class ViewAs
    {
        private ProgressBar progressBar;
        private ViewAsPlugin viewAsPlugin;
        public string SelectedPath { get; set; }=String.Empty;
        public ViewAs(ProgressBar progressBar)
        {
            this.progressBar= progressBar;
            //this.root = menu;
            //          a  this.fileTypeLoader=fileTypeLoader;
            this.viewAsPlugin=AllPlugins.InvokePlugin<ViewAsPlugin>(PluginType.ViewAs);
        }
        public async void SetSelectedPath(string path)
        {
            SelectedPath= path; 
            await Load();
        }
        public List<CustomMenuItem> GetSubMenuItemsForFileTypes()
        {
            return viewAsPlugin.GetSubMenuItemsForFileTypes();
         }

        public async Task<MyEditFile?> Load(  )
        {  
            MyEditFile? result = null;
       
            var progress = new Progress<long>(value => progressBar.Value = value);
            var parameter = new ActionParameter(SelectedPath);
            await viewAsPlugin.Perform(parameter, progress);
            return result;
        }

       

        
    }
}
