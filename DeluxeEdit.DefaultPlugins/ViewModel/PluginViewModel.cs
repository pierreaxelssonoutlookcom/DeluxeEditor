using Model;
using System;
using System.Collections.Generic;
using DefaultPlugins.Misc;
using Model.Interface;
using System.Linq;
using Shared;

namespace DefaultPlugins.ViewModel
{
    public class PluginViewModel
    {

        public IEnumerable<PluginItem> RemoteList()
        {
            throw new NotImplementedException();     
         }
        public List<PluginItem  > LocalList()
        {
            var result = PluginManager.LocalListPllugins();
            return result;
        }



    }
}
