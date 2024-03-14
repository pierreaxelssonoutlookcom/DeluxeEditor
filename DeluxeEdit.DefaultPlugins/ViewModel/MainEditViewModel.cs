using Model.Interface;
using Model;
using System;

using Extensions;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Shared;
using System.Windows.Controls;
using DeluxeEdit.DefaultPlugins;
using FolderBrowser;
using System.Formats.Tar;
using System.Windows.Controls.Primitives;
using DeluxeEdit.DefaultPlugins.ViewModel;

namespace DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {

        private FileOpenPlugin openPlugin;
        private FileSavePlugin savePlugin;
        public static ContentPath CurrenContent = null;
        public static List<ContentPath> AllContents = new List<ContentPath>();

        private static List<CustomMenu> MainMenu = new MenuBuilder().BuildMenu();

        public object ShowM { get; internal set; }

        public MainEditViewModel()
        {

            openPlugin = AllPlugins.InvokePlugin(PluginType.FileOpen) as FileOpenPlugin;
            savePlugin = AllPlugins.InvokePlugin(PluginType.FileSave) as FileSavePlugin;
        }
        public List<CustomMenu> GetMenu()
        {
            return MainMenu;
        }



        public string DoCommand(MenuItem item, string SelectedText)
        {
            var myMenuItem = MainEditViewModel.MainMenu.SelectMany(p => p.MenuItems).First(p => p.Title == item.Header);

            ActionParameter parameter;
            if (myMenuItem.Plugin.ParameterIsSelectedText && SelectedText.HasContent())
                parameter = new ActionParameter(SelectedText);
            else
                parameter = myMenuItem.Plugin.Parameter;


            var result = myMenuItem.Plugin.Perform(parameter);
            return result;

        }



        public void ShowNewFile()
        {
            throw new NotImplementedException();
        }
        public void AddNewTextControl(TabControl control, string name)
        {
            WPFUtil.AddOrUpddateTab(name, control);

            var text = new TextBox();
            text.Name = name;
           text.KeyDown += Text_KeyDown;
            control.Items.Add(text);
        }
        private void Text_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var keyeddata = KeyDown();
            if (keyeddata == null) e.Handled = false;
            else
            {


                e.Handled = true;
            }
        }





        //done :find way to renember old path before dialog 
        public void ScrollTo(double newValue)
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

