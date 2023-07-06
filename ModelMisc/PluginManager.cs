using Model;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ModelMisc
{
    public class PluginManager
    { 
        private static string pluginPath;
        private static Dictionary<string, Assembly>? loadedAsms;
        public static List<INamedActionPlugin> Instances= new List<INamedActionPlugin>();

        static PluginManager() 
        {
          pluginPath = $"{Environment.GetFolderPath( Environment.SpecialFolder.ProgramFiles)}\\DeluxeEdit\\plugins";
          Directory.GetFiles(pluginPath, "*.dll")
          .Select(p =>  LoadPluginFile(p));
        }

         public static 
            INamedActionPlugin InvokePlugin(Type pluginType)
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
            //now recording all plugin objects
            Instances.Add(newItemCasted);
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

        private static INamedActionPlugin CreateObjects(Type t)
        {
            var newItem = Activator.CreateInstance(t);
            var newItemCasted = newItem is INamedActionPlugin ? newItem as INamedActionPlugin : null; ;
            if (newItemCasted == null) throw new NullReferenceException();
            
                if (newItemCasted.ControlType != null)
                    newItemCasted.Control = Activator.CreateInstance(newItemCasted.ControlType);

            
            return newItemCasted;
        }
  
        public static List<INamedActionPlugin> LoadPluginFile(string path)
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
