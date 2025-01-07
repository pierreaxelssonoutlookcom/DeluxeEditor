using Model;
using System;
using Extensions;
using System.Threading.Tasks;
using ViewModel;
using Extensions.Util;
using ICSharpCode.AvalonEdit;
using System.Formats.Tar;
using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace DefaultPlugins.ViewModel
{

    public  class LoadFile
    {
        private FileOpenPlugin openPlugin;
        private FileTypeLoader fileTypeLoader;
        private TabControl tabFiles;
        private MainEditViewModel model;
        private ProgressBar progressBar;

        public LoadFile(MainEditViewModel model, ProgressBar progressBar, TabControl tab)
        {
            this.tabFiles=tab;
            this.model=model;
            this.progressBar = progressBar;

            fileTypeLoader = new FileTypeLoader();
            openPlugin = AllPlugins.InvokePlugin<FileOpenPlugin>(PluginType.FileOpen);
        }
        public async Task<MyEditFile?> Load()
        {

            var action = openPlugin.GuiAction(openPlugin);
            //if user cancelled path is empty 
            if (action == null || !action.Path.HasContent()) throw new NullReferenceException();

            model.SetStatusText($" File: {action.Path}");        
            var parameter = new ActionParameter(action.Path, action.Encoding);

            var progress = new Progress<long>(value => progressBar.Value = value);

            var result = new MyEditFile();
            result.Path = action.Path;
            result.Content = await openPlugin.Perform(parameter, progress);
            
            var text = AddMyControls(action.Path);
            result.Area = fileTypeLoader.CurrentArea;
            text.Text = result.Content;



            // Application.DoEvents();
            MyEditFiles.Add(result);

            return result;
        }
        public TextEditor AddMyControls(string path, string? overrideTabNamePrefix = null)
        {
            bool isNewFle = File.Exists(path) == false;
            var name = isNewFle ? path : new FileInfo(path).Name;
            TextEditor text;
            if (isNewFle)
                text = new TextEditor();
            else
            {
                fileTypeLoader.LoadCurrent(path);
                text = fileTypeLoader.CurrentText;
                progressBar.ValueChanged += model.ProgressBar_ValueChanged;
            }
            text.IsReadOnly = false;
            text.Name = name.Replace(".", "");
            text.Visibility = Visibility.Visible;
            text.KeyDown += model.Text_KeyDown;
            text.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            text.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            name = $"{overrideTabNamePrefix}{name}";
            var tab = WPFUtil.AddOrUpdateTab(name, tabFiles, fileTypeLoader.CurrentArea);

            model.ChangeTab(tab);
            return text;

        }

    }
}
