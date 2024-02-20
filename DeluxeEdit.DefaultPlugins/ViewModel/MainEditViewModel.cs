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
using DefaultPlugins;
using Shared;
using System.Windows.Controls;
using DefaultPlugins.Views;
using MS.WindowsAPICodePack.Internal;
using System.Windows.Forms;
//using System.Windows.Input;

namespace DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {

        private FileOpenPlugin openPlugin;
        private FileSavePlugin savePlugin;
        public static ContentPath CurrenContent = null;
        public static List<ContentPath> AllContents = new List<ContentPath>();

        public static List<CustomMenu> MainMenu = new List<CustomMenu>();
        public void SetCommands(INamedActionPlugin instance, List<CustomMenu> menu)
        {
            foreach (var item in menu.SelectMany(p => p.MenuItems))
            {
                item.MenuAction = null; 
            }
        }

        public void SetCommand(CustomMenuItem item)
        {
            
        }
        public void DoCommand(CustomMenuItem item)
        {

 
        }

         public List<CustomMenu> GetMenuHeaders(IEnumerable<INamedActionPlugin> plugins)
        {
            var ps = plugins.Where(p => p.Configuration.ShowInMenu.HasContent() && p.Configuration.ShowInMenuItem.HasContent()).ToList();
            var result = ps.Select(p=> new CustomMenu { Header = p.Configuration.ShowInMenu }).ToList();


            var pluginsHeader = new CustomMenu { Header = "Plugins" };
            result.Add(pluginsHeader);
 
            return result;
        }



        public List<CustomMenu> LoadMenu()
        { 
            var plugins = AllPlugins.InvokePlugins(PluginManager.GetPluginsLocal());
            var result = GetMenuHeaders(plugins);
            foreach(var item in result)
            {
             item.MenuItems.AddRange(     GetMenuItems(item, plugins));
            }
            MainMenu.Clear();
            MainMenu.AddRange( result );

            return result;
        }
        public List<CustomMenuItem> GetMenuItems(CustomMenu item, IEnumerable<INamedActionPlugin> plugins)
        {
            var result = plugins.Where(p => p.Configuration.ShowInMenu.HasContent() && p.Configuration.ShowInMenuItem.HasContent() && item.Header == p.Configuration.ShowInMenuItem)
                .Select(p => new CustomMenuItem { Title = openPlugin.Configuration.ShowInMenuItem } )
                .ToList();
          return result;
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
        public static void AddOrUpddateTab(string header, TabControl control)
        {
            if (TabÉxist(header, control) == false)
            {
                var item = new TabItem { Header = header };
                control.Items.Add(item);
            }
        }
        public static bool TabÉxist(string header, TabControl control)
        {
            bool result = false;
            foreach (TabItem x in control.Items)
            {
                if (x.Header == header)
                {
                    result = true;
                    break;
                }

            }
            return result;
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
            var matchCount = openPlugin.Configuration.KeyCommand.Keys
                .Cast<System.Windows.Input.Key>()
                .Count(p => System.Windows.Input.Keyboard .IsKeyDown(p));
            
            keysOkProceed=matchCount == openPlugin.Configuration.KeyCommand.Keys.Count && openPlugin.Configuration.KeyCommand.Keys.Count>0;
            if (keysOkProceed) result=UpdateLoad();

             
            return result;
        }

    }
}

