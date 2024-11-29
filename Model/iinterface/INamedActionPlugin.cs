
namespace Model
{

    
    
    
    public interface INamedActionPlugin
    {
        ConfigurationOptions Configuration { get; set; }


        bool ParameterIsSelectedText        { get; set; }
        bool Enabled { get; set; }
        Version Version { get; set; }

        const string VersionString = "";


        Type Id { get; set; }
        string Titel { get; set; }

        ActionParameter?  Parameter { get; set; }


        Task<string> Perform(ActionParameter parameter, IProgress<long> progresss);

       Task<IEnumerable<string>> Perform(IProgress<long> progresss);
         void SetConfig();

      

        EncodingPath? GuiAction(INamedActionPlugin instance);
        object CreateControl(bool showToo);
        string Path { get; set; } 
  
        
        }

   
}
