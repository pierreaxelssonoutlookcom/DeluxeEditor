using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PluginItem
    {
        public string Id { get; set; }
        public Version? Version { get; set; }
        public bool Enabled { get; set; }
        public string DerivedSourcePath { get; set; }


    }
}
