using System;
using System.Collections.Generic;
using System.Text;

namespace DeluxeEdit.Model
{
    public class PluginSourceItem
    {
        public string? ID { get; set;  }
        public string? Name { get; set; }
        public Version? Version { get; set; }
        public string? LocalPath { get; set; }
        public string? Url { get; set; }

    }
}