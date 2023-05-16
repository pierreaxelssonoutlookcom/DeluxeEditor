using System;
using System.Collections.Generic;

namespace DeluxeEdit.Model
{
    public class PresentationOptions
    {
        public List<Char> ShortCutCommand { get; set; }
        public string ShowInMenu { get; set; } = "";
        public int SortOrder { get; set; }
        public PresentationOptions()
        {
            ShortCutCommand = new List<Char>();
        }
    }
}
