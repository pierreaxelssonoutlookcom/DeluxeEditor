using DefaultPlugins.Model;
using DeluxeEdit.DefaultPlugins;
using DeluxeEdit.DefaultPlugins.ViewModel;
using Extensions;
using Model;
using Model.Interface;
using MS.WindowsAPICodePack.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using static System.Net.Mime.MediaTypeNames;



namespace DefaultPlugins.ViewModel
{
    public partial class MainEditViewModel
    {
        private ProgressBar progressBar;
        private TabControl tabFiles;
        private TextBlock progressText, statusText;
        private NewFileViewModel newFileViewModel;
        private FileOpenPlugin openPlugin;
        private INamedActionPlugin savePlugin;

        private static List<CustomMenu> MainMenu = new MenuBuilder().BuildMenu();

        public object ShowM { get; internal set; }
        private long? lastFileLength;

        public MainEditViewModel(TabControl tab, ProgressBar bar, TextBlock   progressText,TextBlock statusText)
        {
            this.progressBar = bar;
            tabFiles = tab;
            this.progressText = progressText;
            this.statusText = statusText;

            newFileViewModel = new NewFileViewModel(tab);
            openPlugin = FileOpenPlugin.CastNative(AllPlugins.InvokePlugin(PluginType.FileOpen));
            savePlugin = AllPlugins.InvokePlugin(PluginType.FileSaveAs);
        }

        public List<CustomMenu> GetMenu()
        {
            return MainMenu;
        }
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
                publisher.PublishNewFile(newFileViewModel.GetNewFile());
            else if (myMenuItem.Plugin is FileOpenPlugin)
            {
                var data = await LoadFile();
               publisher.PublishEditFile(data);
        }else if (myMenuItem.Plugin is FileSavePlugin)
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
            WPFUtil.AddOrUpddateTab(name, tabFiles, result.Panel) ;
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





             lastFileLength= openPlugin.GetFileLeLength(parameter);
            result.Content = await openPlugin.Perform(parameter, progress);

            combo.Text.Text = result.Content;
            MyEditFiles.Add(result);
            
            return result;
        }

        public void ChangeTab(TabItem item)
        {
            MyEditFiles.Current = MyEditFiles.Files.FirstOrDefault(p => p.Header == item.Header);

        }
        public async void SaveFile()
        {
            var text = MyEditFiles.Current.Text as TextBox;
            if (text != null)
            {

                var split = text.Text.Split(Environment.NewLine).ToList();
                await savePlugin.Perform(new ActionParameter(MyEditFiles.Current.Path, split), null);
            }

        }
    }
}

