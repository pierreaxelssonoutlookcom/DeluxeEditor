using Model;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Extensions;
using System.Reflection;
using NuGet;
using System.IO.Packaging;
using NuGet.Packaging;

namespace Shared
{
    public class NugetManager
    {
        private static string pluginPath;
        public static List<INamedActionPlugin> Instances = new List<INamedActionPlugin>();
        public static List<PluginFile> SourceFiles = new List<PluginFile>();

        public NugetManager()
        {
            pluginPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\DeluxeEdit\\plugins";

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
            var result = path.ParseNugetFileName();
            var pkg = ZipPackage.Open(path);
            pkg.GetRelationships().Select(p => p.SourceUri);
            var parts = pkg.GetParts().ToList();
            var contentTypes = parts.Select(p=>p.ContentType).ToList();

            var packageParts = parts
                .Where(p => p.ContentType == "aplication/octet").ToList();
            return result; 
            /*
            var myStream=packagePart.GetStream(FileMode.Open);
            packagePart.ContentType=
            var reader = new StreamReader(myStream);
            myStream
            var ourSource = SourceFiles.FirstOrDefault(p => String.Equals(p.LocalPath, path, StringComparison.InvariantCultureIgnoreCase));

            if (ourSource == null)
            
                ourSource = new PluginFile { LocalPath = path };
                ourSource.Assembly = Assembly.LoadFrom(path);

                SourceFiles.Add(ourSource);
            }
            var matchingTypes = ourSource.Assembly.GetTypes()
                .Where(p => p.ToString().EndsWith("Plugin"))
                .ToList();

            ourSource.Plugins = matchingTypes.Select(p =>
            new PluginItem { Id = p.ToString(), MyType = p, DerivedSourcePath = path, Version = p.Assembly.GetName().Version })
                .ToList();

            return ourSource;
            */
        }
        public static void UnLoadPluginFile(string path)
        {
            var match = SourceFiles.FirstOrDefault(p => String.Equals(path, p.LocalPath, StringComparison.CurrentCultureIgnoreCase));
            if (match != null)
            {
                var asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(p => path.SameFileName(p.GetName().CodeBase));
            }
        }
    }                                                               
}