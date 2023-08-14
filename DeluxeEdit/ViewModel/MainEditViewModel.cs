using Model.Interface;
using Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Extensions;
using System.Security.Cryptography;
using System.Linq;
//using System.Windows.Input;

namespace DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {
        private FileOpenPlugin plugin;


        public MainEditViewModel()
        {
            plugin = AllPlugins.InvokePlugin(PluginId.FileOpen) as FileOpenPlugin;
        }
        /// <summary>
        /// Code should we run when loading a file
        /// </summary>
        /// <returns></returns>
        public string UpdateLoad()
        {
            var result=String.Empty;
            var actionResult = plugin.GuiAction(plugin);
            //if user cancelled path is empty 
            if (actionResult != null && actionResult.Path.HasContent())
            {

                plugin.OpenEncoding = actionResult.Encoding;
                result = plugin.Perform(new ActionParameter(actionResult.Path), String.Empty);
            }
            return result;
        }
        public void UpdateBeforeSave(INamedActionPlugin plugin, ActionParameter parameter)
        {


        }


        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.

        /// <summary>
        /// now counting number of matched key pressed    
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public string  KeyDown()
        {
            //done:cast enum from int
            string result = String.Empty;
            bool keysOkProceed = false;
            var matchCount = plugin.Configuration.KeyCommand
                .Cast<System.Windows.Input.Key>()
                .Count(p => System.Windows.Input.Keyboard.IsKeyDown(p));
            
            keysOkProceed=matchCount == plugin.Configuration.KeyCommand.Count && plugin.Configuration.KeyCommand.Count>0;
            if (keysOkProceed) result=UpdateLoad();

             
            return result;
        }

    }
}
