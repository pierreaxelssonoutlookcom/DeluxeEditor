using System;
using System.Collections.Generic;

namespace DeluxeEdit.Shared.Interfaces
{
    public interface INamedAction
    {
        string Name  { get; set; }
        string Titel { get; set; }  
        List<Char> ShortCutCommand { get; set; }
        int SortOrdder { get; set; }
        string Parameter { get; set; }
        string Result { get; set; } 

        Func<string, string> Action { get; set; }
        string Perform(string parameter);
        
        }

    
}
