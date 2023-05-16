using System;
using System.Collections.Generic;

namespace DeluxeEdit.Model.Interface
{

    public interface INamedActionPlugin:  INamedAction
    {
        
       string Path { get; set; }
        string ClassName { get; set; }

        
        }

    
}
