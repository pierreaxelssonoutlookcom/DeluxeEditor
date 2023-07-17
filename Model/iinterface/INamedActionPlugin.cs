using System;
using System.Collections.Generic;

namespace Model.Interface
{

    
    
    
    public interface INamedActionPlugin
    {
        ConfigurationOptions Configuration { get; set; }


        bool Enabled { get; set; }

        string Id { get; set; }
        string Titel { get; set; }

        ActionParameter?  Parameter { get; set; }
        string Perform(ActionParameter parameter, string indata);

        object? Control { get; set; }

        Type?  ControlType { get; set; }

        EncodingPath?  GuiAction(INamedActionPlugin instance) ;
        string Path { get; set; } 
  
        
        }

   
}
