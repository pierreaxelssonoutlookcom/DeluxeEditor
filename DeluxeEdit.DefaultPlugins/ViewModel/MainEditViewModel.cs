using Model.Interface;
using Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DeluxeEdit.Extensions;
using System.Security.Cryptography;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using DefaultPlugins.Misc;
using Shared;
//using System.Windows.Input;

namespace DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {

        private FileOpenPlugin openPlugin;
        private FileSavePlugin savePlugin;
        public static  ContentPath CurrenContent=null;
        public static List<ContentPath> AllContents = new List<ContentPath>();

        public  static List< CustomMenu> MainMenu= new List<CustomMenu>();

        public List<CustomMenu> LoadMenu()
        {
            var plugins=PluginManager.InvokePlugins(PluginManager.GetPluginsLocal());
            var showInMenuConfs = plugins.Where(p => p.Configuration.ShowInMenu.HasContent() && p.Configuration.ShowInMenuItem.HasContent())
                 .Select(p => p.Configuration);
            foreach (var conf in showInMenuConfs)
            {
                var header = MainMenu.FirstOrDefault(p => p.Header == conf.ShowInMenu);
                if (header == null)
                {
                    header = new CustomMenu { Header = conf.ShowInMenu };
                    MainMenu.Add(header);
                }

                var item = new CustomMenuItem { Title = $"{conf.ShowInMenuItem} ({conf.KeyCommand })" };
                header.MenuItems.Add(item);
            }
            var pluginsHeader = MainMenu.FirstOrDefault(p => p.Header == "Plugins");
            if (pluginsHeader == null)
            {
                pluginsHeader = new CustomMenu { Header = "Plugins" };
                
                MainMenu.Add(pluginsHeader);
            }




            var pitems = plugins.Select(p => new CustomMenuItem { Title = p.ToString() } ).ToList();

            pluginsHeader.MenuItems.AddRange(pitems);

            return MainMenu;
        } 
                                    
        public MainEditViewModel()
        {

            openPlugin = AllPlugins.InvokePlugin(PluginType.FileOpen) as FileOpenPlugin;
            savePlugin = AllPlugins.InvokePlugin(PluginType.FileSave) as FileSavePlugin;
        }
        //done :find way to renember old path before dialog 
        public  void ScrollTo(double newValue)
        {
            var seeked= openPlugin.SeekData(newValue);
            CurrenContent.Content = String.Join("\r\n", seeked);

    }

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
            CurrenContent= MainEditViewModel.AllContents.First(p => p.Path == item.Path && p.Header == item.Header);
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
            var matchCount = openPlugin.Configuration.KeyCommand.KeyCommand
                .Cast<System.Windows.Input.Key>()
                .Count(p => System.Windows.Input.Keyboard .IsKeyDown(p));
            
            keysOkProceed=matchCount == openPlugin.Configuration.KeyCommand.KeyCommand.Count && openPlugin.Configuration.KeyCommand.KeyCommand.Count>0;
            if (keysOkProceed) result=UpdateLoad();

             
            return result;
        }

    }
}

