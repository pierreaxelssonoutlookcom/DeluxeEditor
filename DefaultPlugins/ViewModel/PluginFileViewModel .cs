using Model;
using System;
using System.Collections.Generic;
using DefaultPlugins.Misc;
using Model.Interface;
using System.Linq;
using Shared;

namespace DefaultPlugins.ViewModel
{
    public class PluginFileViewModel
    {

        public IEnumerable<PluginItem> RemoteList()
        {
            throw new NotImplementedException();     
         }
        public List<PluginFile> LocalList()
        {
         return  PluginManager.LoadFiles();
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
