using System;

using System.Collections.Generic;

namespace DeluxeEdit.Model.Interface
{

    public interface INamedAction
    {
        PresentationOptions PresentationOptions { get; set; }

        

        string Name  {  get; set; }
        string Titel { get; set; }

        ActionParameter Parameter { get; set         ; }
        string Result { get; set; }

        string Perform(ActionParameter parameter);
        
        }

    
}
