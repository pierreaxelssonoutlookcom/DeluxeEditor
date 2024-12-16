/*using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Extensions;
using System.Reflection;
using System.IO.Packaging;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using System.Threading.Tasks;
using System.Threading;

namespace Shared
{
    public class NugetManager
    { 
            /*

        private static string pluginPath;
        public static List<INamedActionPlugin> Instances = new List<INamedActionPlugin>();
        public static List<PluginFile> SourceFiles = new List<PluginFile>();

        public static object NullPropertyProvider { get; private set; }
        static NugetManager()
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
        public static INamedActionPlugin InvokePlugin(PluginItem item)
        { 
            var result = CreateObjects(item.MyType);
            return result;
        }

        private static INamedActionPlugin CreateObjects(Type t)
        {

            object item = Activator.CreateInstance(t);
            var newItemCasted = item is INamedActionPlugin ? item as INamedActionPlugin : null; ;
            if (newItemCasted == null) throw new NullReferenceException();


            return newItemCasted;
        }

        //todo:move
        public static Package Create(string path)
        {
            var result = ZipPackage.Open(path);
            return result;
        }
        public static async Task<List<string>> ReadManifest(Package pack, string path)
        {
            var manifestRelationType = pack.GetRelationshipsByType("http://schemas.microsoft.com/packaging/2010/07/manifest").SingleOrDefault();
            var manifestPart = pack.GetPart(manifestRelationType.TargetUri);
            var reader = new PackageFolderReader(new FileInfo(path).Directory);
            //    .Select(p => PackageEntry.´öHashFilename(p.Uri.LocalPath)).ToArray();
            var result= await reader.GetPackageFilesAsync(PackageSaveMode.None, CancellationToken.None);
            return result.ToList();        }

        

        public static PluginFile LoadPluginFile(string path)
        {
            var pack =  Create(path);
            //var result = path.ParseNugetFileName();
            ReadManifest(pack, path);
            return null;
       }
            /*
            pkg.GetRelationships().Select(p => p.SourceUri);
            var parts = pkg.GetParts().ToList();
                   parts. cSelect(p => PackageEntry.HashFilename(p.Uri.LocalPath)).ToArray();
            var c  = parts.Select(p=>p.GetStream()).ToList();

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
/*
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
*/
