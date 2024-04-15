using DeluxeEdit.DefaultPlugins;
using DeluxeEdit.DefaultPlugins.ViewModel;
using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DefaultPlugins.ViewModel
{
    public class MainEditViewModel
    {
        TabControl currentTab;
        private NewFileViewModel newFileViewModel;
        private FileOpenPlugin openPlugin;
        private FileSavePlugin savePlugin;
        
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




        public async Task<string> DoCommand(MenuItem item, string SelectedText)
        {
            string result="" ;
            var publisher = new EventData();
            
            var myMenuItem = MainEditViewModel.MainMenu.SelectMany(p => p.MenuItems).First(p => p.Title == item.Header);
            if (myMenuItem.Plugin is FileNewPlugin)
                publisher.PublishNewFile(newFileViewModel.GetNewFile());
            else if (myMenuItem.Plugin is FileOpenPlugin)
                publisher.PublishLoadFile(await LoadFile());
            else if (myMenuItem.Plugin is FileSavePlugin)
                SaveFile();
            else if (myMenuItem.Plugin.ParameterIsSelectedText && SelectedText.HasContent())
                result = await myMenuItem.Plugin.Perform(new ActionParameter { Parameter = SelectedText });
            else
                result = await myMenuItem.Plugin.Perform(myMenuItem.Plugin.Parameter);


            return result;

        }



   public void ScrollTo(double newValue)
        {
              //done :find way to renember old path before dialog 
 
        }
        public TextBox AddNewTextControlAndListen(string path)
        {
            var name = new FileInfo(path).Name;
            WPFUtil.AddOrUpddateTab(name, currentTab);

            var text = new TextBox();

            text.Name = name;
            text.KeyDown += Text_KeyDown;
            currentTab.Items.Add(text);

            return text;
        }

        public async Task<ContentPath?> LoadFile()
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
                result.Content  = await openPlugin.Perform(new ActionParameter { Parameter = result.Path });
                var text = AddNewTextControlAndListen(result.Header);
                text.Text = result.Content;
                MyEditFiles.Add(
                    new MyEditFile
                    {
                        Path = result.Path,
                        Content = result.Content,
                        Header = result.Header,
                        Text = text,
                        Tab = currentTab.Items.CurrentItem
                    });
            }
            return result;
        }
        public void ChangeTab(TabItem  item)
        {
            MyEditFiles.Current = MyEditFiles.Files.FirstOrDefault(p => p.Header==item.Header);
                
        }
        public ContentPath SaveFile()
        {
            var text = MyEditFiles.Current.Text as TextBox;
            var result = new ContentPath { Path = MyEditFiles.Current.Path, Content = text.Text };
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

        public async Task<ContentPath?> KeyDown()
        {
            //done:cast enum from int
            ContentPath result = null;
            bool keysOkProceed = false;
            var matchCount = openPlugin.Configuration.KeyCommand.Keys
                .Cast<System.Windows.Input.Key>()
                .Count(p => System.Windows.Input.Keyboard.IsKeyDown(p));

            keysOkProceed = matchCount == openPlugin.Configuration.KeyCommand.Keys.Count && openPlugin.Configuration.KeyCommand.Keys.Count > 0;
            if (keysOkProceed) result = await LoadFile();


            return result;
        }
    }
}

