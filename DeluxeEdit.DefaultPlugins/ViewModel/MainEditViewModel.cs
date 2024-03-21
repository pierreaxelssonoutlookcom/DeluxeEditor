using DeluxeEdit.DefaultPlugins;
using DeluxeEdit.DefaultPlugins.ViewModel;
using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {
        TabControl currentTab;
        private NewFileViewModel newFileViewModel;
        private FileOpenPlugin openPlugin;
        private FileSavePlugin savePlugin;
        public static ContentPath CurrenContent = null;
        public static List<ContentPath> AllContents = new List<ContentPath>();

        private static List<CustomMenu> MainMenu = new MenuBuilder().BuildMenu();

        public object ShowM { get; internal set; }

        public MainEditViewModel(TabControl tab)
        {
            currentTab = tab;
            newFileViewModel= new NewFileViewModel(tab);
            openPlugin = AllPlugins.InvokePlugin(PluginType.FileOpen) as FileOpenPlugin;
            savePlugin = AllPlugins.InvokePlugin(PluginType.FileSave) as FileSavePlugin;
        }
        public List<CustomMenu> GetMenu()
        {
            return MainMenu;
        }
        private void SetMenuActions(List<CustomMenu> menu)
        {
            var allItems = menu.SelectMany(p => p.MenuItems);
            foreach (var item in allItems)
            {

            }

        }
        public void SetViewData(CustomMenuItem item)
        {

        }




        public string DoCommand(MenuItem item, string SelectedText)
        {
            string result="" ;
            var publisher = new CustomViewData();
            
            var myMenuItem = MainEditViewModel.MainMenu.SelectMany(p => p.MenuItems).First(p => p.Title == item.Header);
            if (myMenuItem.Plugin is FileNewPlugin)
                publisher.PublishNewFile(newFileViewModel.GetNewFile());
            else if (myMenuItem.Plugin is FileOpenPlugin)
                publisher.PublishLoadFile(LoadFile());
            else if (myMenuItem.Plugin is FileSavePlugin)
                SaveFile();
            else if (myMenuItem.Plugin.ParameterIsSelectedText && SelectedText.HasContent())
                result = myMenuItem.Plugin.Perform(new ActionParameter { Parameter = SelectedText });
            else
                result = myMenuItem.Plugin.Perform(myMenuItem.Plugin.Parameter);


            return result;

        }



   public void ScrollTo(double newValue)
        {
              //done :find way to renember old path before dialog 
           var seeked = openPlugin.SeekData(newValue);
            CurrenContent.Content = String.Join("\r\n", seeked);

        }
        public TextBox AddNewTextControlAndListen(string path)
        {
            var name = new FileInfo(path).Name;
            WPFUtil.AddOrUpddateTab(name, currentTab);

            var text = new TextBox();

            text.Name = name;
            text.KeyDown += Text_KeyDown;
            currentTab.Items.Add(text);
            MyFiles.Files.Add(new MyFile { Path = path, Text = text });

            return text;
        }

        public ContentPath? LoadFile()
        {
            ContentPath? result = null;
            var action = openPlugin.GuiAction(openPlugin);
            //if user cancelled path is empty 
            if (action != null && action.Path.HasContent())
            {
                result = new ContentPath();
                result.Path = action.Path;
                result.Header = new FileInfo(result.Path).Name;
                openPlugin.OpenEncoding = action.Encoding;
                result.Content = openPlugin.Perform(new ActionParameter { Parameter = result.Path }); 
                var text = AddNewTextControlAndListen(result.Header);
                text.Text = result.Content;
                CurrenContent = result;

                AllContents.Add(result);
            }
            //done:fix so we can  keep track of contents and paths
            return result;

        }
        public void ChangeTab(ContentPath item)
        {
            MyFiles.Current = MyFiles.Files.FirstOrDefault(p => p.Path==item.Path);
            if (MyFiles.Current == null) 
            CurrenContent = MainEditViewModel.AllContents.First(p => p.Path == item.Path && p.Header == item.Header);
        }
        public ContentPath SaveFile()
        {
            var text = MyFiles.Current.Text as TextBox;
            var result = new ContentPath { Path = MyFiles.Current.Path, Content = text.Text };
            savePlugin.Perform(new ActionParameter { Parameter = result.Path, InData = result.Content });
            return result;
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

        public ContentPath? KeyDown()
        {
            //done:cast enum from int
            ContentPath result = null;
            bool keysOkProceed = false;
            var matchCount = openPlugin.Configuration.KeyCommand.Keys
                .Cast<System.Windows.Input.Key>()
                .Count(p => System.Windows.Input.Keyboard.IsKeyDown(p));

            keysOkProceed = matchCount == openPlugin.Configuration.KeyCommand.Keys.Count && openPlugin.Configuration.KeyCommand.Keys.Count > 0;
            if (keysOkProceed) result = LoadFile();


            return result;
        }
    }
}

