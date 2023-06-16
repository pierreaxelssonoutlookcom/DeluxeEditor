﻿using DeluxeEdit.DefaultPlugins.GuiActions;
using DeluxeEdit.DefaultPlugins.Managers;
using DeluxeEdit.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{
    public class PluginViewModel
    {
        private Views.Plugins pluginView;
        private PluginManager manager;

        public PluginViewModel()
        {
            pluginView = new Views.Plugins();
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
