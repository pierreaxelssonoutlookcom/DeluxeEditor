using DeluxeEdit.Model.Interface;
using DeluxeEdit.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DeluxeEdit.Extensions;
using System.Security.Cryptography;
using System.Linq;
using System.Windows.Input;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {
        private INamedActionPlugin plugin;


        public MainEditViewModel()
        {
            plugin = AllPlugins.InvokePlugin(PluginId.FileOpen);
        }

        public string UpdateLoad()
        {
            var result=String.Empty;
            string? path = plugin.GuiAction(plugin);
            //if user cancelled path is empty 
            if (path.HasContent() && !String.IsNullOrEmpty(path))
            {
                result = plugin.Perform(new ActionParameter(path));
            }
            return result;
        }
        public void UpdateBeforeSave(INamedActionPlugin plugin, ActionParameter parameter)
        {


        }


        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
 

        public string  KeyDown(KeyEventArgs e)
        {
            string result=String.Empty;
            bool keysOkProceed = false;
            var matchCount = plugin.Configuration.KeyCommand
                .Count(p => Keyboard.IsKeyDown(Enum.Parse<System.Windows.Input.Key>(p.ToString())));
            
            keysOkProceed=matchCount == plugin.Configuration.KeyCommand.Count;
            if (keysOkProceed) result=UpdateLoad();


            return result;
            throw new NotImplementedException();
        }

    }
}
