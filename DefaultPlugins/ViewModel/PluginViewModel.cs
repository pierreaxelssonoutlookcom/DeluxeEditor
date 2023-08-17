using Model;
using System;
using System.Collections.Generic;
using DefaultPlugins.Misc;
using Model.Interface;
using System.Linq;

namespace DefaultPlugins.ViewModel
{
    public class PluginViewModel
    {
        private PluginManager manager;

        public PluginViewModel()
        {
            manager = new PluginManager();    
        }   
        public IEnumerable<PluginItem> RemoteList()
        {
            throw new NotImplementedException();     
         }
        public List<PluginItem> LocalList()
        {
            manager.LoadFiles();
            var result = new List<PluginItem>();
            foreach (var f in PluginManager.SourceFiles)
            {
                result.AddRange(f.Instances.Select(i => GetPluginItem(f.LocalPath, i)));

            }
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


    }
}
