using System;
using System.Collections.Generic;

namespace DeluxeEdit.Model.Interface
{

    
    
    
    public interface INamedActionPlugin
    {
        char[] MyKeyCommand { get; set; }
        PresentationOptions PresentationOptions { get; set; }


        bool Enabled { get; set; }

        string Id { get; set; }
        string Titel { get; set; }

        string Perform(ActionParameter parameter);

        object? Control { get; set; }

        Type?  ControlType { get; set; }

        string? GuiAction(INamedActionPlugin instance) ;
        string Path { get; set; } 
  
        
        }

   
}
