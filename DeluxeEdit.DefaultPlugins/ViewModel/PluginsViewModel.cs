using Model;
using System;
using System.Collections.Generic;
using DefaultPlugins;
using Model.Interface;
using System.Linq;
using Shared;

namespace DefaultPlugins.ViewModel
{
    public class PluginsViewModel
    {

        public IEnumerable<PluginItem> RemoteList()
        {
            throw new NotImplementedException();     
         }
        public List<PluginItem  > LocalList()
        {
            var result = PluginManager.GetPluginsLocal();
            return result;
        }

        public IEnumerable<PluginFile> RemoteListFiles()
        {
            throw new NotImplementedException();
        }
        public List<PluginFile> LocalListFiles()
        {
            return PluginManager.LoadFiles();
        }


    }
}
