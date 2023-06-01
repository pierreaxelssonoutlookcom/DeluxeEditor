using DeluxeEdit.Format;
using DeluxeEdit.Model;
using DeluxeEdit.Model.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

namespace DeluxeEdit.DefaultPlugins.Managers
{
    public class NugetPluginManager
    { 
        private string pluginPath;
        private static Dictionary<string, Assembly>? loadedAsms;

        public NugetPluginManager() 
        {
          pluginPath = $"{Environment.SpecialFolder.ApplicationData}\\DeluxeEdit\\plugins";
            loadedAsms = new Dictionary<string, Assembly>();
        }




        public IEnumerable<PluginSourceItem> RemoteList()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<PluginSourceItem> LocalList()
        {
            var parser = new PluginSourceParser();

            var result = Directory.GetFiles(pluginPath, "*.dll")
                .Select(p => parser.ParseFileName(p)).ToList();
            
 

            return result;

        }
        public List<INamedActionPlugin> LoadPluginFile(string path)
        {
            if (loadedAsms!=null && !loadedAsms.ContainsKey(path))
            {
               var asm= Assembly.LoadFrom(path);
                
                loadedAsms[path]=Assembly.LoadFile(path);
            }

            //done:could be multiple plugisAssemblyn in the same, FILE
             
            var result = new List<INamedActionPlugin>();
            if (loadedAsms != null)
            {
                foreach (var t in loadedAsms[path].GetTypes())
                {

                    var newItem = Activator.CreateInstance(t);
                    var newItemCasted = newItem is INamedActionPlugin ? newItem as INamedActionPlugin : null; ;

                    if (newItemCasted != null)
                        result.Add(newItemCasted);
                }
            }
            return result;
        }




    }
}
