using System;
using System.Collections.Generic;

namespace DeluxeEdit.Model
{
    public class PresentationOptions
    {
        public string? ShowInMenu { get; set; } = "";
        public int? SortOrder { get; set; }
        public PresentationOptions()
        {
            ;
        }
    }
}
