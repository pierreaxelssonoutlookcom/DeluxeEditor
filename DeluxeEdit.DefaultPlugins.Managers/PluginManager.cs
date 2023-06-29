using DeluxeEdit.DefaultPlugins.Managers.Other;
using DeluxeEdit.Model;
using DeluxeEdit.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DeluxeEdit.DefaultPlugins.Managers
{
    public class PluginManager
    { 
        private string pluginPath;
        private static Dictionary<string, Assembly>? loadedAsms;
        private List<string> pluginFiles;

        public PluginManager() 
        {
          pluginPath = $"{Environment.GetFolderPath( Environment.SpecialFolder.ProgramFiles)}\\DeluxeEdit\\plugins";
          pluginFiles=Directory.GetFiles(pluginPath, "*.dll").ToList();
          pluginFiles.Select(p =>  LoadPluginFile(p));
        }

         public static INamedActionPlugin InvokePlugin(Type pluginType)
        {
            object? newItem = Activator.CreateInstance(pluginType);


            /*
            if (loadedAsms == null) throw new NullReferenceException();
            foreach (var asm in loadedAsms)
            {
                  var type = asm.Value.GetTypes().SingleOrDefault(p => p == pluginType);
                if (type != null)
                {
                       newItem=  .CreateInstance(type);
                    break;
                }
            }
            */
          //  if (newItem == null) throw new NullReferenceException();
          var newItemCasted = newItem is INamedActionPlugin ? newItem as INamedActionPlugin : null; ;
            if (newItemCasted == null) throw new InvalidCastException();

            return newItemCasted;
        }
        public List<PluginFileItem> RemoteList()
        {
            throw new NotImplementedException();
        }   

        public List<PluginFileItem> LocalList()
        {
            var parser = new PluginFileItemParser();

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
            if (loadedAsms == null) loadedAsms = new Dictionary<string, Assembly>();


            var result = new List<INamedActionPlugin>();
            if (loadedAsms!=null && !loadedAsms.ContainsKey(path))
            {
                 loadedAsms[path]=Assembly.LoadFile(path);
            }

            if (loadedAsms == null) throw new NullReferenceException();
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
