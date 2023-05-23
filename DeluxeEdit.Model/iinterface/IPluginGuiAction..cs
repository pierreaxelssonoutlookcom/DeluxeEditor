using System;
using System.Collections.Generic;

namespace DeluxeEdit.Model.Interface
{

    public interface IPluginGuiAction
    {

        string HasGuiPartClassName { get; set; }
        Func<string, string> GuiAction(string name);
       
        
        }

    
}
