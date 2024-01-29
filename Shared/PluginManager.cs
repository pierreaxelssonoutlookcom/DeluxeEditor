using Model;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Extensions;
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

        }
         
        public static List<PluginItem> LocalListPllugins()
        {
            var files = PluginManager.LoadFiles();
            var result = files.SelectMany(p => p.Plugins).ToList();
            return result;
        }
        public PluginItem GetPluginItem(string path, INamedActionPlugin item)
        {
            var result = new PluginItem();
            result.DerivedSourcePath = path;
            result.Id = item.Id;
            result.Version = item.Version;
            return result;
        }

        public static List<PluginFile> LoadFiles()
        {
            var result = Directory.GetFiles(pluginPath, "*.dll")
                .Select(p => LoadPluginFile(p))
                .ToList();

            return result;
        }


        public static INamedActionPlugin InvokePlugin(Type type)
        {
            var result = CreateObjects(type);
            return result;
        }
        public INamedActionPlugin InvokePlugin(PluginItem item)
        {
            var result = CreateObjects(item.MyType);
            return result;
        }

        private static INamedActionPlugin CreateObjects(Type t)
        {
            object item = Activator.CreateInstance(t);
            var newItemCasted = item is INamedActionPlugin ? item as INamedActionPlugin : null; ;
            if (newItemCasted == null) throw new NullReferenceException();

            if (newItemCasted.ControlType != null)
                newItemCasted.Control = Activator.CreateInstance(newItemCasted.ControlType);


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
            var matchingTypes = ourSource.Assembly.GetTypes()
                .Where(p => p.ToString().EndsWith("Plugin"))
                .ToList();

            ourSource.Plugins = matchingTypes.Select(p =>  path.CreatePluginItem(p)).ToList();




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