using Model.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
 
namespace Model
{
    public class PluginFile
    {
        public string LocalPath { get; set; }
        public string? Url { get; set; }
        public Assembly Assembly  { get; set; }
        public List<PluginItem> Plugins { get; set; }
        public List<INamedActionPlugin> Instances { get; set; }
        public PluginFile() 
        {
            Plugins = new List<PluginItem>();
            Instances = new List<INamedActionPlugin>();
        }

    }
}