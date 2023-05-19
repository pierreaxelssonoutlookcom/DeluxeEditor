using DeluxeEdit.Model;
using DeluxeEdit.Model.Interface;
using System;
using System.Collections.Generic;

namespace DeluxeEdit.Shared
{
    public class PluginFileReader 
    {


        private static List<PluginSource>? sourcesAndPlugins;
        private PluginManager manager;
        public PluginFileReader()
        {
            manager = new PluginManager();
            SetupCaches();

        }

        private  void SetupCaches()
        {
            sourcesAndPlugins=GetAllDefaultPlugins();

            ;
        }

            public  List<PluginSource> GetAllDefaultPlugins()
            {
                var sources= GetAllSources()
                ;
                foreach ( var source in sources ) 
                {
                    manager.LoadPlugins( source );  
                }


                return sources;

            }
        }
}

