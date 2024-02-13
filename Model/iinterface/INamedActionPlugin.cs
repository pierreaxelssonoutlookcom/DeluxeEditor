using System;
using System.Collections.Generic;

namespace Model.Interface
{

    
    
    
    public interface INamedActionPlugin
    {
        ConfigurationOptions Configuration { get; set; }


        bool Enabled { get; set; }
        Version  Version { get; set; }

        string Id { get; set; }
        string Titel { get; set; }

        ActionParameter?  Parameter { get; set; }
        string Perform(ActionParameter parameter);

        object? Control { get; set; }

        Type?  ControlType { get; set; }

        EncodingPath? GuiAction(INamedActionPlugin instance);
        object CreateControl();
        string Path { get; set; } 
  
        
        }

   
}
