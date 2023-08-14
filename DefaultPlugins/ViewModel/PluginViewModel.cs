using ModelMisc;
using Model;
using System;
using System.Collections.Generic;

namespace DefaultPlugins.ViewModel
{
    public class PluginViewModel
    {
        private PluginManager manager;

        public PluginViewModel()
        {
            manager = new PluginManager();    
        }   
        public IEnumerable<PluginFileItem> RemoteList()
        {
            return manager.RemoteList();
        }
        public IEnumerable<PluginFileItem> LocalList()
        {
            var result=manager.LocalList();
            return result;
        }

    }
}
