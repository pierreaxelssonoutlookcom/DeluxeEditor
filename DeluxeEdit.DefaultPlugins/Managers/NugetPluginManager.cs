using DeluxeEdit.Format;
using DeluxeEdit.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DeluxeEdit.DefaultPlugins.Managers
{
    public class NugetPluginManager
    {
        private string futurePluginPath;

        public NugetPluginManager() 
        {
            futurePluginPath = $"{Environment.SpecialFolder.ApplicationData}\\DeluxeEdit\\plugins";

        }




        public IEnumerable<PluginSourceItem> RemoteList()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<PluginSourceItem> LocalList(string search)
        {
            var parser = new PluginSourceItemParser();

            var result = Directory.GetFiles(futurePluginPath, "*.dll")
                .Select(p => parser.ParseFileName(p)).ToList();
            
 

            return result;

        }          



    }
}
