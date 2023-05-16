using DeluxeEdit.Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeluxeEdit.Model
{
    public class PluginSource
    {
        public string Path { get; set; } = "";
        public List<INamedActionPlugin> Items { get; set; }
        public PluginSource()
        {
            Items = new List<INamedActionPlugin>();
        }
    }
}
