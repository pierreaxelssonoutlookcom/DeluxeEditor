using System.Collections.Generic;

namespace Model
{
    public class CustomMenu
    {
        public string Header { get; set; } ="";
        public List<CustomMenuItem> MenuItems { get; set; } = new List<CustomMenuItem>();
    }
}