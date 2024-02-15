using DeluxeEdit.Extensions;
using Model;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Shared
{
    public class PluginManager
    {
        private static string pluginPath;
        public static List<INamedActionPlugin> Instances = new List<INamedActionPlugin>();
        public static List<PluginFile> SourceFiles = new List<PluginFile>();

        static PluginManager()
        {
            pluginPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\DeluxeEdit\\plugins";
            LoadFiles();

        }
         
        public static List<PluginItem> GetPluginsLocal()
        {
            var files = PluginManager.LoadFiles();
            var result= new List<PluginItem>();
            foreach( var f in files)
             { 
                 var items= f.MatchingTypes.Select(p=> f.LocalPath.CreatePluginItem(p));
                result.AddRange(items);
            }
            return result;
        }
        public static List<PluginFile> LoadFiles()
        {
            var result = Directory.GetFiles(pluginPath, "*.dll")
                .Select(p => LoadPluginFile(p))
                .ToList();

            return result;
        }


 
        public  static INamedActionPlugin CreateObject(Type t)
        {
            object item = Activator.CreateInstance(t);
            var newItemCasted = item is INamedActionPlugin ? item as INamedActionPlugin : null; ;
            if (newItemCasted == null) throw new NullReferenceException();



            return newItemCasted;
        }

 

        public static PluginFile LoadPluginFile(string path)
        {
            //done:could be multiple plugis in the same, FILE

            var ourSource = SourceFiles.FirstOrDefault(p => String.Equals(p.LocalPath, path, StringComparison.InvariantCultureIgnoreCase));

            if (ourSource == null)
            {
                ourSource = new PluginFile { LocalPath = path };
                ourSource.Assembly = Assembly.LoadFrom(path);

                SourceFiles.Add(ourSource);
            }
            ourSource.MatchingTypes = ourSource.Assembly.GetTypes()
                .Where(p => p.ToString().EndsWith("Plugin"))
                .ToList();



            return ourSource;
        }
        public static void UnLoadPluginFile(string path)
        {
            throw new NotImplementedException();
            var match = SourceFiles.FirstOrDefault(p => String.Equals(path, p.LocalPath, StringComparison.CurrentCultureIgnoreCase));
            if (match != null)
            {
                var asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(p => path.SameFileName(p.GetName().CodeBase));
            }
        }
    }                                                               
}