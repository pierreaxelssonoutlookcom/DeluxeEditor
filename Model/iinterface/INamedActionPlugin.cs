using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model.Interface
{

    
    
    
    public interface INamedActionPlugin
    {
        ConfigurationOptions Configuration { get; set; }


        bool ParameterIsSelectedText        { get; set; }
        bool Enabled { get; set; }
        Version Version { get; set; }
        public string VersionString { get; set; }


        string Id { get; set; }
        string Titel { get; set; }

        ActionParameter?  Parameter { get; set; }


        Task<string> Perform(ActionParameter parameter);

       Task<IEnumerable<string>> Perform();



        EncodingPath? GuiAction(INamedActionPlugin instance);
        object CreateControl(bool showToo);
        string Path { get; set; } 
  
        
        }

   
}
