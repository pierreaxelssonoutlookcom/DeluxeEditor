using DefaultPlugins;
using Extensions.Util;
using Extensions;
using Model;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using DefaultPlugins.ViewModel;
namespace ViewModel
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
        private HexPlugin hexPlugin;
        private EventData viewData;
        private static List<CustomMenu> MainMenu = new MenuBuilder().BuildMenu();


        private List<INamedActionPlugin> relevantPlugins;
        private FileTypeLoader fileTypesLoader;

        public MainEditViewModel(TabControl tab, ProgressBar bar, TextBlock progressText, TextBlock statusText)
        {
            this.progressBar = bar;
            tabFiles = tab;
            this.progressText = progressText;
            this.statusText = statusText;
            newFileViewModel = new NewFileViewModel(tab);
            openPlugin = AllPlugins.InvokePlugin<FileOpenPlugin>(PluginType.FileOpen);
            saveAsPlugin = AllPlugins.InvokePlugin<FileSaveAsPlugin>(PluginType.FileSaveAs);
            savePlugin = AllPlugins.InvokePlugin<FileSavePlugin>(PluginType.FileSave);
            hexPlugin  = AllPlugins.InvokePlugin<HexPlugin>(PluginType.Hex);
            viewData = new EventData();

            viewData.Subscibe(OnEvent);
            relevantPlugins = AllPlugins.InvokePlugins(PluginManager.GetPluginsLocal())
                .Where(p => p.Configuration.KeyCommand.Keys.Count > 0).ToList();

            fileTypesLoader = new FileTypeLoader();
        }
       

        public async Task<MyEditFile?> NewFile()
        {
            var file = newFileViewModel.GetNewFile();
            MyEditFiles.Add(file);
            var text=AddMyControl(file.Path);
            var myTab=WPFUtil.AddOrUpdateTab(file.Header, tabFiles,text);
            if (myTab!=null) ChangeTab(myTab);

            await Task.Delay(0);
            return file;


        }
        public FileTypeItem? ExecuteViewAs(string menuTitle)
        {
            var result = FileTypeLoader.ParseFileItem(menuTitle);
            return result;
        }

        public async Task<string> DoCommand(MenuItem item, string SelectedText)
        {
            string result = "";
            var header=item!=null && item.Header!=null ? item.Header.ToString() : String.Empty;
            var progress = new Progress<long>(value => progressBar.Value = value);
            
            var myMenuItem = MainEditViewModel.MainMenu.SelectMany(p => p.MenuItems)
                .Single(p => p != null && p.Title!=null && p.Title ==header);
            //CheckForViewAs(myMenuItem.Title)
            var actions = new SetupMenuActions(this);
            actions.SetMenuAction(myMenuItem);
            if (myMenuItem.MenuActon != null)
                await myMenuItem.MenuActon.Invoke();
            else
                ExecuteViewAs(myMenuItem.Title);

            if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.ParameterIsSelectedText && SelectedText.HasContent())
                result = await myMenuItem.Plugin.Perform(new ActionParameter(SelectedText), progress);
            else if (myMenuItem!=null && myMenuItem.Plugin!=null && myMenuItem.Plugin.Parameter != null)
                result = await myMenuItem.Plugin.Perform(myMenuItem.Plugin.Parameter, progress);


            return result;


        }
        public async Task<MyEditFile?>  HexView()
        {
           var result= new MyEditFile();
            if (MyEditFiles.Current == null || MyEditFiles.Current.Text == null) throw new NullReferenceException();
 
            statusText.Text = $"Hex View:{MyEditFiles.Current.Path}";
            
            var progress = new Progress<long>(value => progressBar.Value = value);
            var parameter = new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Encoding);
            var hexOutput = await hexPlugin.Perform(parameter, progress);

            result.Path = hexOutput;
            result.Content = hexOutput;
            AddMyControl(result.Path);

            return result ;
        }

        public void ScrollTo(double newValue)
        {
            //do,ne :find way to renember old path before dialog 



        }

        public TextEditor AddMyControl(string path)
        {
            bool isNewFle=!File.Exists(path);
            var name = isNewFle ? path :  new FileInfo(path).Name ;

    
            fileTypesLoader.LoadCurrent(path);

            fileTypesLoader.CurrentText.Name = name.Replace(".", "");
            //            text.AcceptsReturn = true;
            fileTypesLoader.CurrentText.KeyDown += Text_KeyDown;

            progressBar.ValueChanged += ProgressBar_ValueChanged;

            WPFUtil.AddOrUpdateTab(name, tabFiles, fileTypesLoader.CurrentText);

            return fileTypesLoader.CurrentText;

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
            var parameter = new ActionParameter(result.Path);
            parameter.Encoding=action.Encoding;
            result.Encoding = action.Encoding;

            var text=AddMyControl(result.Path);
            var progress =          new Progress<long>(value => progressBar.Value = value);
            

//            lastFileLength = openPlugin.GetFileLeLength(parameter);
            result.Content = await openPlugin.Perform(parameter, progress);
            
            viewData.PublishEditFile(result);

            text.AppendText(result.Content);
            MyEditFiles.Add(result);

            return result;
        }

        public void ChangeTab(TabItem item)
        {
            if (item == null) throw new NullReferenceException();
            var header = item != null && item.Header != null ? item.Header.ToString() : String.Empty;

            MyEditFiles.Current = MyEditFiles.Files.FirstOrDefault(p => p.Header == header                       );

        }
        public async Task<MyEditFile?> SaveFile()
        {
            if (MyEditFiles.Current == null || MyEditFiles.Current.Text == null) throw new NullReferenceException();

                    var progress = new Progress<long>(value => progressBar.Value = value);
            bool fileExist = File.Exists(MyEditFiles.Current.Path);
            if (fileExist)
                await savePlugin.Perform(new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Text.Text), progress);
            else
                   await SaveAsFile();

            return null;

        }
        public async Task<MyEditFile?> SaveAsFile()
        {

            if (MyEditFiles.Current == null || MyEditFiles.Current.Text==null) throw new NullReferenceException();
            var progress = new Progress<long>(value => progressBar.Value = value);


            var action = openPlugin.GuiAction(openPlugin);
            //if user cancelled pat
            //h is empty 
            if (action == null || !action.Path.HasContent()) throw new NullReferenceException();

            await saveAsPlugin.Perform(new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Text.Text), progress);
            return null;
   
        }
    }
}

