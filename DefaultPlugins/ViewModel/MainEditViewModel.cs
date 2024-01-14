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
using DefaultPlugins.Misc;
//using System.Windows.Input;

namespace DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {

        private FileOpenPlugin openPlugin;
        private FileSavePlugin savePlugin;
        public static  ContentPath CurrenContent=null;
        public static List<ContentPath> AllContents = new List<ContentPath>();

        public MainEditViewModel()
        {

            openPlugin = AllPlugins.InvokePlugin(PluginType.FileOpen) as FileOpenPlugin;
            savePlugin = AllPlugins.InvokePlugin(PluginType.FileSave) as FileSavePlugin;
        }
        //done :find way to renember old path before dialog 
        public ContentPath? UpdateLoad()
        {
            ContentPath? result = null;
            var action= openPlugin.GuiAction(openPlugin);
            //if user cancelled path is empty 
            if (action != null && action.Path.HasContent())
            {
                result = new ContentPath();
                result.Path = action.Path;
                result.Header = new FileInfo(result.Path).Name;
                openPlugin.OpenEncoding = action.Encoding;
                result.Content = openPlugin.Perform(new ActionParameter(result.Path));
                CurrenContent= result;
                                 
                AllContents.Add(result);
            }
            //done:fix so we can keep track of contents and paths
            return result;
            
        }
        public void ChangeTab(ContentPath item)
        {
            MainEditViewModel.CurrenContent= MainEditViewModel.AllContents.First(p => p.Path == item.Path && p.Header == item.Header);
        }
        public void UpdateSave(string data)
        {
            savePlugin.Perform(new ActionParameter(CurrenContent.Path, CurrenContent.Content));
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

