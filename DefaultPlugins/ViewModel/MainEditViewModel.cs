using Model.Interface;
using Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Extensions;
using System.Security.Cryptography;
using System.Linq;
using System.Drawing;
//using System.Windows.Input;

namespace DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {
        private FileOpenPlugin openPlugin;
        private FileSavePlugin savePlugin;
        private ActionParameter openedParam;
        private string Content;

        public MainEditViewModel()
        {
            openPlugin = AllPlugins.InvokePlugin(PluginId.FileOpen) as FileOpenPlugin;
            savePlugin = AllPlugins.InvokePlugin(PluginId.FileSave) as FileSavePlugin;
        }
        //done :find way to renember old path before dialog 
        public string UpdateLoad()
        {
            var result=String.Empty;
            var action= openPlugin.GuiAction(openPlugin);
            //if user cancelled path is empty 
            if (action != null && action.Path.HasContent())
            { 
                openedParam = new ActionParameter(action.Path);
                openPlugin.OpenEncoding = action.Encoding;
                result = openPlugin.Perform(openedParam, String.Empty);
            }
            //todo:fix so we can keep track of contents and paths
            Content= result;
            return result;
            
        }
        public void UpdateSave(string data,  ActionParameter parameter)
        {

            savePlugin.Perform(parameter, data);
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
            var matchCount = openPlugin.Configuration.KeyCommand
                .Cast<System.Windows.Input.Key>()
                .Count(p => System.Windows.Input.Keyboard.IsKeyDown(p));
            
            keysOkProceed=matchCount == openPlugin.Configuration.KeyCommand.Count && openPlugin.Configuration.KeyCommand.Count>0;
            if (keysOkProceed) result=UpdateLoad();

             
            return result;
        }

    }
}

