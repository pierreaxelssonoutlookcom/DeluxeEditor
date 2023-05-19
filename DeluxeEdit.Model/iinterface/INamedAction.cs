using System;

using System.Collections;

namespace DeluxeEdit.Model.Interface
{

    public interface INamedAction
    {
       char[] MyKeyCommand { get; set; }
        PresentationOptions PresentationOptions { get; set; }


        bool    Enabled  { get; set; }

        string Name  {  get; set; }
        string Titel { get; set; }

        ActionParameter Parameter { get; set         ; }
        string Result { get; set; }

        string Perform(ActionParameter parameter);
        
        }

    
}
