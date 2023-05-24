using System;
using System.Collections.Generic;

namespace DeluxeEdit.Model.Interface
{

    public interface INamedActionPlugin : INamedAction
    {

        Func<ActionParameter, string>? GuiAction();
        string Path { get; set; } 
        string ClassName { get; set; }

        
        }

   
}
