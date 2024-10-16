using DefaultPlugins.Model;
using DeluxeEdit.DefaultPlugins;
using DeluxeEdit.DefaultPlugins.ViewModel;
using Extensions;
using Model;
using Model.Interface;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DefaultPlugins.ViewModel
{
    public partial class MainEditViewModel
    {
        private ProgressBar progressBar;
        private TabControl tabFiles;
        private TextBlock progressText, statusText;
        private NewFileViewModel newFileViewModel;
        private FileOpenPlugin openPlugin;
        private INamedActionPlugin saveAsPlugin;
        private INamedActionPlugin savePlugin;

        private static List<CustomMenu> MainMenu = new MenuBuilder().BuildMenu();


        public object ShowM { get; internal set; }
        private long? lastFileLength;
        private List<INamedActionPlugin> relevantPlugins;

        public MainEditViewModel(TabControl tab, ProgressBar bar, TextBlock progressText, TextBlock statusText)
        {
            this.progressBar = bar;
            tabFiles = tab;
            this.progressText = progressText;
            this.statusText = statusText;
            tab.KeyDown += Tab_KeyDown1; ;
            newFileViewModel = new NewFileViewModel(tab);
            openPlugin = FileOpenPlugin.CastNative(AllPlugins.InvokePlugin(PluginType.FileOpen));
            saveAsPlugin = AllPlugins.InvokePlugin(PluginType.FileSaveAs);
            savePlugin = AllPlugins.InvokePlugin(PluginType.FileSave);

            var viewData = new EventData();

            viewData.subscrile(OnEvent);
            relevantPlugins = AllPlugins.InvokePlugins(PluginManager.GetPluginsLocal())
                .Where(p => p.Configuration.KeyCommand.Keys.Count > 0).ToList();

        }

        private void Tab_KeyDown1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyDown();
        }

        private void Tab_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void NewFile()
        {
            var file = newFileViewModel.GetNewFile();


            file.Text.Text = file.Content;



        }

        /*
        public List<CustomMenu> GetMenu()
        {
            return MainMenu;
            *
        }
        */

        public void SetViewData(CustomMenuItem item)
        {

        }




        public async Task<string> DoCommand(MenuItem item, string SelectedText)
        {
            string result = "";
            var publisher = new EventData();

            var myMenuItem = MainEditViewModel.MainMenu.SelectMany(p => p.MenuItems)
                .Single(p => item != null && p != null && p.Title == item.Header);
            if (myMenuItem.Plugin is FileNewPlugin)
                NewFile();
            else if (myMenuItem.Plugin is FileOpenPlugin)
            {
                var data = await LoadFile();
                publisher.PublishEditFile(data);
            }
            else if (myMenuItem.Plugin is FileSavePlugin)
                SaveFile();
            else if (myMenuItem.Plugin.ParameterIsSelectedText && SelectedText.HasContent())
                result = await myMenuItem.Plugin.Perform(new ActionParameter { Parameter = SelectedText },
                    null);
            else
                result = await myMenuItem.Plugin.Perform(myMenuItem.Plugin.Parameter, null);


            return result;

        }



        public void ScrollTo(double newValue)
        {
            //done :find way to renember old path before dialog 



        }

        public ComboControl AddMyContols(string path)
        {
            var result = new ComboControl();
            var name = new FileInfo(path).Name;

            result.Text = new TextBox();

            result.Text.Name = name.Replace(".", "");
            result.Text.AcceptsReturn = true;
            result.Text.KeyDown += Text_KeyDown;
            progressBar.ValueChanged += ProgressBar_ValueChanged;


            result.Panel = new StackPanel();
            result.Panel.Name = "panel" + name.Replace(".", "");
            result.Panel.Orientation = Orientation.Vertical;
            result.Panel.Children.Add(result.Text);
            WPFUtil.AddOrUpddateTab(name, tabFiles, result.Panel);
            // currentTab.Items.Add(resu

            return result;

        }


        public async Task<MyEditFile?> LoadFile()
        {

            MyEditFile? result = null;
            var action = openPlugin.GuiAction(openPlugin);
            //if user cancelled path is empty 
            if (action == null || !action.Path.HasContent()) throw new NullReferenceException();

            statusText.Text = $" File: {action.Path}";
            result = new MyEditFile();

            result.Path = action.Path;
            result.Header = new FileInfo(result.Path).Name;
            openPlugin.OpenEncoding = action.Encoding;
            var combo = AddMyContols(result.Header);
            var progress = new Progress<long>(value => progressBar.Value = value);
            var parameter = new ActionParameter(result.Path);


            ;





            lastFileLength = openPlugin.GetFileLeLength(parameter);
            result.Content = await openPlugin.Perform(parameter, progress);

            combo.Text.Text = result.Content;
            MyEditFiles.Add(result);

            return result;
        }

        public void ChangeTab(TabItem item)
        {
            if (item == null) throw new NullReferenceException();

            MyEditFiles.Current = MyEditFiles.Files.FirstOrDefault(p => p.Header == item.Header);

        }
        public async void SaveFile()
        {
            if (MyEditFiles.Current == null || MyEditFiles.Current.Text == null) throw new NullReferenceException();
         

            await savePlugin.Perform(new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Text.Text), null);

        }
        public async void SaveAsFile()
        {

            if (MyEditFiles.Current == null || MyEditFiles.Current.Text==null) throw new NullReferenceException();


            var action = openPlugin.GuiAction(openPlugin);
            //if user cancelled pat
            //h is empty 
            if (action == null || !action.Path.HasContent()) throw new NullReferenceException();

            await saveAsPlugin.Perform(new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Text.Text), null);


        } 
    }
}

