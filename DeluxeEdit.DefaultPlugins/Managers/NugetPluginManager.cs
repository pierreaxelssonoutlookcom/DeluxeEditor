using DeluxeEdit.Extensions;
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
          pluginPath = $"{Environment.SpecialFolder.Programs}\\DeluxeEdit\\plugins";
          loadedAsms = new Dictionary<string, Assembly>();
        }

        public List<PluginSourceItem> RemoteList()
        {
            throw new NotImplementedException();
        }

        public List<PluginSourceItem> LocalList()
        {
            var parser = new PluginSourceParser();

            var result = Directory.GetFiles(pluginPath, "*.dll")
                 .Select(p => parser.ParseFileName(p)).ToList();
            return result;
           }

        private INamedActionPlugin CreateObjects(Type t)
        {
            var newItem = Activator.CreateInstance(t);
            var newItemCasted = newItem is INamedActionPlugin ? newItem as INamedActionPlugin : null; ;
            if (newItemCasted == null) throw new NullReferenceException();
            
                if (newItemCasted.ControlType != null)
                    newItemCasted.Control = Activator.CreateInstance(newItemCasted.ControlType);

            
            return newItemCasted;
        }
  
        public List<INamedActionPlugin> LoadPluginFile(string path)
        {
            var result = new List<INamedActionPlugin>();
            if (loadedAsms!=null && !loadedAsms.ContainsKey(path))
            {
                 loadedAsms[path]=Assembly.LoadFile(path);
            }
            if (loadedAsms==null) throw new NullReferenceException();
            //done:could be multiple plugisAssemblyn in the same, FILE
             
 
           foreach (var t in loadedAsms[path].GetTypes())
           {
                var newItemCasted = CreateObjects(t);
                result.Add(newItemCasted);
           }
            
            return result;
        }




    }
}
