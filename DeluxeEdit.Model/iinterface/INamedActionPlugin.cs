using System;
using System.Collections.Generic;

namespace DeluxeEdit.Model.Interface
{

    public interface INamedActionPlugin : INamedAction
    {

        //string HasGuiPartClassName { get; set; }
        Func<string, string> GuiAction(string name);
        string Path { get; set; } 
        string ClassName { get; set; }

        
        }

    
}
