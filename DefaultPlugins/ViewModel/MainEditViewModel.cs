using Model.Interface;
using Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Extensions;
using System.Security.Cryptography;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
//using System.Windows.Input;

namespace DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {
        private FileOpenPlugin openPlugin;
        private FileSavePlugin savePlugin;
        private ActionParameter LastParam;
        private static Dictionary<string, string>  Contents=new Dictionary<string, string>();

        public MainEditViewModel()
        {
            openPlugin = AllPlugins.InvokePlugin(PluginId.FileOpen) as FileOpenPlugin;
            savePlugin = AllPlugins.InvokePlugin(PluginId.FileSave) as FileSavePlugin;
        }
        //done :find way to renember old path before dialog 
        public ContentPath UpdateLoad()
        {
            var result = new ContentPath();
            var action= openPlugin.GuiAction(openPlugin);
            //if user cancelled path is empty 
            if (action != null && action.Path.HasContent())
            { 
                result.Path = action.Path;
                result.Header = new FileInfo(result.Path).Name;
                LastParam = new ActionParameter(action.Path);
                openPlugin.OpenEncoding = action.Encoding;
                result.Content = openPlugin.Perform(LastParam, String.Empty);
            }
            //todo:fix so we can keep track of contents and paths
            Contents[LastParam.Parameter]= result.Content;
            return result;
            
        }
        public void ChangeTab(ContentPath item)
        { }
        public void UpdateSave(string data)
        {
            if (LastParam == null) throw new NullReferenceException();
            savePlugin.Perform(null, data);
        }


        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.

        /// <summary>
        /// now counting number of matched key pressed    
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ContentPath?  KeyDown()
        {
            //done:cast enum from int
            ContentPath result = null;
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

