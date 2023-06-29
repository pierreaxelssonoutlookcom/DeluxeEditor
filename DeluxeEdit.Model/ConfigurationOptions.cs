using System;
using System.Collections.Generic;
using System.Windows.Input;
namespace DeluxeEdit.Model
{
    public class ConfigurationOptions
    { 
        /// <summary>
        /// We only support a maximum of 3 keys
        /// </summary>
        public List<Key> KeyCommand { get; set; }

        public string? ShowInMenu { get; set; } = "";
        public int? SortOrder { get; set; }
        public ConfigurationOptions()
        {
            KeyCommand = new List<Key>();
        }
    }
}
