using Model.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
 
namespace Model
{
    public class PluginFile
    {
        public bool Loaded { get; set; }
        public string Name{ get; set; }
        public Version Version { get; set; }

        public string LocalPath { get; set; }
        public string? Url { get; set; }
        public Assembly Assembly  { get; set; }
        public List<PluginItem> Plugins { get; set; }
        public List<INamedActionPlugin> Instances { get; set; }
        public List<Type> MatchingTypes { get; set; }

        public PluginFile() 
        {
            Plugins = new List<PluginItem>();
            Instances = new List<INamedActionPlugin>();
        }

    }
}