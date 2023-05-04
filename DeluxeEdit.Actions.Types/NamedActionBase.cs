using DeluxeEdit.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeluxeEdit.Actions
{
    public class NamedActionBase : INamedAction
    {
        public bool AsReadonly { get; set; }
        public Encoding OpenEncoding { get; set; }
        public string Name { get; set; }
        public string Titel { get; set; }
        public List<char> ShortCutCommand { get; set; }
        public int SortOrdder { get; set; }
        public string Parameter { get; set; }
        public string Result { get; set; }
        public Func<string, string> Action { get; set; }

        public string Perform(string parameter)
        {
            var result = Action.Invoke(parameter);
            return result;
        }
    }


}
