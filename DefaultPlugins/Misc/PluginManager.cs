using DefaultPlugins.Misc;
using Model;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DefaultPlugins.Misc
 {
    public class PluginManager
    { 
        private static string pluginPath;
        public static List<INamedActionPlugin> Instances= new List<INamedActionPlugin>();
        public static List<PluginFile> SourceFiles = new List<PluginFile>();

        static PluginManager()
        {
            pluginPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\DeluxeEdit\\plugins";
            
        }
        public void LoadFiles()
        {
            SourceFiles =
                Directory.GetFiles(pluginPath, "*.dll").ToList()
                .Select(p => new PluginFile { LocalPath = p }).ToList();
            SourceFiles.ForEach(p=> 
            p.Instances = LoadPluginFile(p.LocalPath));
           
        }
     

        public static INamedActionPlugin InvokePlugin(Type pluginType)
        {
            object? newItem = Activator.CreateInstance(pluginType);




          var newItemCasted = newItem is INamedActionPlugin ? newItem as INamedActionPlugin : null; ;
            if (newItemCasted == null) throw new InvalidCastException();
            //now recording all plugin objects
            Instances.Add(newItemCasted);
            return newItemCasted;

        }

        private static INamedActionPlugin CreateObjects(Type t)
        {
            object item= Activator.CreateInstance(t);

            var newItemCasted = item is INamedActionPlugin ? item as INamedActionPlugin : null; ;
            if (newItemCasted == null) throw new NullReferenceException();
             
            if (newItemCasted.ControlType != null)
                newItemCasted.Control = Activator.CreateInstance(newItemCasted.ControlType);
            

            return newItemCasted;
        }
  
        public static List<INamedActionPlugin> LoadPluginFile(string path)
        {
            //done:could be multiple plugis in the same, FILE

            var result = new List<INamedActionPlugin>();
            var ourSource= SourceFiles.First(p => String.Equals(p.LocalPath, path, StringComparison.InvariantCultureIgnoreCase));
            
            
            if (ourSource.Assembly == null) ourSource.Assembly = Assembly.LoadFile(path);

            var matchingTypes = ourSource.Assembly.GetTypes()
                .Where(p=>p.ToString().EndsWith("Plugin") )
                .ToList();
            matchingTypes.ForEach(t => result.Add(CreateObjects(t)));
            
            return result;
        }




    }
}
