﻿using System;

using System.Collections.Generic;

namespace DeluxeEdit.Interface
{
    public interface INamedActionPlugin:  INamedAction
    {
        
       string Path { get; set; }
        string Name { get; set; }
        string Titel { get; set; }  
        List<Char> ShortCutCommand { get; set; }
        string Parameter { get; set; }
        string Result { get; set; } 

        Func<string, string> Action { get; set; }
        string Perform(string parameter);
        
        }

    
}
