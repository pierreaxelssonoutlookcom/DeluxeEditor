using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PluginItem
    {
        public Type MyType { get; set; }
        public string Id { get; set; }
        public Version? Version { get; set; }
        public bool Enabled { get; set; }
        public string DerivedSourcePath { get; set; }

        public override string ToString()
        {
            return "${Id} v.{Version} {MyType }";
        }
    };
}
