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
using System.Formats.Tar;
using System.Windows;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Document;
using System.Xml.Linq;
using System.Windows.Shapes;

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
      

        private List<INamedActionPlugin> relevantPlugins;
        private FileTypeLoader fileTypesLoader;

        public MainEditViewModel(TabControl tab, ProgressBar bar, TextBlock progressText, TextBlock statusText)
        {
            this.progressBar = bar;
            tabFiles = tab;
            tabFiles.SelectionChanged += TabFiles_SelectionChanged;
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
            var text=AddMyControls(file.Path);
            var myTab=WPFUtil.AddOrUpdateTab(file.Header, tabFiles,text);
            if (myTab!=null) ChangeTab(myTab);

            await Task.Delay(0);
            return file;



        }
        public FileTypeItem? ExecuteViewAs(string menuTitle)
        {
            var result = FileTypeLoader.GetFileTypeItemByMenu(menuTitle);
            return result;
        }
     
        public async Task<string> DoCommand(MenuItem item)
        {
            string result = "";
            var header=item!=null && item.Header!=null ? item.Header.ToString() : String.Empty;
            var progress = new Progress<long>(value => progressBar.Value = value);
            
            var myMenuItem = MenuBuilder.MainMenu.SelectMany(p => p.MenuItems)
                .Single(p => p != null && p.Title!=null && p.Title ==header);
            //CheckForViewAs(myMenuItem.Title)
            var actions = new SetupMenuActions(this);
            actions.SetMenuAction(myMenuItem);
            if (myMenuItem.MenuActon != null)
                await myMenuItem.MenuActon.Invoke();
            else
            {
                string selectedText = fileTypesLoader.CurrentText != null ? fileTypesLoader.CurrentText.SelectedText : String.Empty;

                var viewasResult = ExecuteViewAs(myMenuItem.Title);

                if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.ParameterIsSelectedText && selectedText.HasContent())
                    result = await myMenuItem.Plugin.Perform(new ActionParameter(selectedText), progress);
                else if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.Parameter != null)
                    result = await myMenuItem.Plugin.Perform(myMenuItem.Plugin.Parameter, progress);

            }
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

            result.Path = MyEditFiles.Current.Path;
            result.Content = hexOutput;
            var text=     AddMyControls(result.Path, "hex:");
            text.Text = hexOutput;
            

            return result ;
        }

        public void ScrollTo(double newValue)
        {
            //do,ne :find way to renember old path before dialog 



        }

        public TextEditor AddMyControls(string path, string? overrideTabNamePrefix=null)
        {
            bool isNewFle=!File.Exists(path);
            var name = isNewFle ? path :  new FileInfo(path).Name ;

    
            fileTypesLoader.LoadCurrent(path);

            fileTypesLoader.CurrentText.IsReadOnly = false;
            fileTypesLoader.CurrentText.Name = name.Replace(".", "");
            fileTypesLoader.CurrentText.Visibility = Visibility.Visible;
            fileTypesLoader.CurrentText.KeyDown += Text_KeyDown;
            fileTypesLoader.CurrentText.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            fileTypesLoader.CurrentText.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            progressBar.ValueChanged += ProgressBar_ValueChanged;
            name=$"{overrideTabNamePrefix}{name}";
                    var tab=WPFUtil.AddOrUpdateTab(name, tabFiles, fileTypesLoader.CurrentTextArea);
            ChangeTab(tab);

            return fileTypesLoader.CurrentText;

        }

 
            

        public async Task<MyEditFile?> LoadFile()
        {

            var action = openPlugin.GuiAction(openPlugin);
            //if user cancelled path is empty 
            if (action == null || !action.Path.HasContent()) throw new NullReferenceException();

            statusText.Text = $" File: {action.Path}";
            var parameter = new ActionParameter(action.Path, action.Encoding);
  
             var progress =          new Progress<long>(value => progressBar.Value = value);

            var result = new MyEditFile();
            result.Path = action.Path;
            result.Content = await openPlugin.Perform(parameter, progress);

            var text = AddMyControls(action.Path);
            
            text.Text=result.Content;
            
            
            //            viewData.PublishEditFile(result);

          //  WPFUtil.RefreshUI(tabFiles);

            // Application.DoEvents();
            MyEditFiles.Add(result);
            
            return result;
        }


        public void ChangeTab(TabItem item)
        {
            if (item == null) throw new NullReferenceException();
            tabFiles.SelectedItem=item;
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

