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
            var viewData = new EventData();

            viewData.Subscibe(OnEvent);
            relevantPlugins = AllPlugins.InvokePlugins(PluginManager.GetPluginsLocal())
                .Where(p => p.Configuration.KeyCommand.Keys.Count > 0).ToList();

            fileTypesLoader = new FileTypeLoader();
        }
       

        public void NewFile()
        {
            var file = newFileViewModel.GetNewFile();
            MyEditFiles.Add(file);
            var text=AddMyControls(file.Path);
            var myTab=WPFUtil.AddOrUpdateTab(file.Header, tabFiles,text);
            if (myTab!=null) ChangeTab(myTab);



          if (file!=null && file.Text!=null) file.Text.Text = file.Content;



        }
        public async Task<string> DoCommand(MenuItem item, string SelectedText)
        {
            string result = "";
            var header=item!=null && item.Header!=null ? item.Header.ToString() : String.Empty;
            var publisher = new EventData();
            var progress = new Progress<long>(value => progressBar.Value = value);

            var myMenuItem = MainEditViewModel.MainMenu.SelectMany(p => p.MenuItems)
                .Single(p => p != null && p.Title!=null && p.Title ==header);
            if (myMenuItem.Plugin is FileNewPlugin)
                NewFile();
            else if (myMenuItem.Plugin is FileOpenPlugin)
            {
                var data = await LoadFile();
                if (data != null)
                publisher.PublishEditFile(data);
            }
            else if (myMenuItem.Plugin is FileSavePlugin)
                SaveFile();
            else if (myMenuItem.Plugin is FileSaveAsPlugin)
                SaveAsFile();
            else if (myMenuItem.Plugin is HexPlugin)
                HexView();
            else if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.ParameterIsSelectedText && SelectedText.HasContent())
                result = await myMenuItem.Plugin.Perform(new ActionParameter(SelectedText), progress);
            else if (myMenuItem!=null && myMenuItem.Plugin!=null && myMenuItem.Plugin.Parameter != null)
                result = await myMenuItem.Plugin.Perform(myMenuItem.Plugin.Parameter, progress);


            return result;


        }
        public async void HexView()
        {
            if (MyEditFiles.Current == null || MyEditFiles.Current.Text == null) throw new NullReferenceException();
 
            statusText.Text = $"Hex View:{MyEditFiles.Current.Path}";
            
            var text = AddMyControls(MyEditFiles.Current.Header);
            var progress = new Progress<long>(value => progressBar.Value = value);
            var parameter = new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Encoding);


            //            lastFileLength = openPlugin.GetFileLeLength(parameter);
            var hexOutput = await hexPlugin.Perform(parameter, progress);
//            text.IsReadOnly = true;

  //          text.AppendText(hexOutput.ToString());
        }

        public void ScrollTo(double newValue)
        {
            //do,ne :find way to renember old path before dialog 



        }

        public TextEditor AddMyControls(string path)
        {
            bool isNewFle=!File.Exists(path);
            var name = isNewFle ? path :  new FileInfo(path).Name ;
            var text = new TextEditor();

    
            fileTypesLoader.LoadFileTypes(path);


            text.Name = name.Replace(".", "");
//            text.AcceptsReturn = true;
            text.KeyDown += Text_KeyDown;

            progressBar.ValueChanged += ProgressBar_ValueChanged;

            WPFUtil.AddOrUpdateTab(name, tabFiles, text);

            return text;

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

            var text=AddMyControls(result.Path);
            var progress =          new Progress<long>(value => progressBar.Value = value);
            

//            lastFileLength = openPlugin.GetFileLeLength(parameter);
            result.Content = await openPlugin.Perform(parameter, progress);

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
        public async void SaveFile()
        {
            if (MyEditFiles.Current == null || MyEditFiles.Current.Text == null) throw new NullReferenceException();
            
            var progress = new Progress<long>(value => progressBar.Value = value);
            bool fileExist = File.Exists(MyEditFiles.Current.Path);
            if (fileExist)
                await savePlugin.Perform(new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Text.Text), progress);
            else
                 SaveAsFile();

        }
        public async void SaveAsFile()
        {

            if (MyEditFiles.Current == null || MyEditFiles.Current.Text==null) throw new NullReferenceException();
            var progress = new Progress<long>(value => progressBar.Value = value);


            var action = openPlugin.GuiAction(openPlugin);
            //if user cancelled pat
            //h is empty 
            if (action == null || !action.Path.HasContent()) throw new NullReferenceException();

            await saveAsPlugin.Perform(new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Text.Text), progress);


        } 
    }
}

