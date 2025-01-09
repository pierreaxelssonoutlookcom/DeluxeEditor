using Model;
using System;
using Extensions;
using System.Threading.Tasks;
using Extensions.Util;
using ICSharpCode.AvalonEdit;
 using System.Formats.Tar;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DefaultPlugins;
using ICSharpCode.AvalonEdit.Editing;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace ViewModel
{

    public class LoadFile
    {
        public FileOpenPlugin openPlugin;
        public FileTypeLoader fileTypeLoader;
        public TabControl tabFiles;
        public MainEditViewModel model;
        public ProgressBar progressBar;
        public TextEditor? CurrentText { get; set; }
        public TextArea? CurrentArea { get; set; }

        public LoadFile(MainEditViewModel model, ProgressBar progressBar, TabControl tab)
        {
            tabFiles = tab;
            this.model = model;
            this.progressBar = progressBar;

            fileTypeLoader = new FileTypeLoader();
            openPlugin = AllPlugins.InvokePlugin<FileOpenPlugin>(PluginType.FileOpen);
        }
        public virtual async Task<MyEditFile?> Load()
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

            var items = AddMyControlsForExisting(action.Path);
            result.Area = fileTypeLoader.CurrentArea;

            result.Text = items.Item1;
            items.Item1.Text = result.Content;
            CurrentText = items.Item1;
            CurrentArea = fileTypeLoader.CurrentArea;
            result.Tab = items.Item2;
            // Application.DoEvents();
            MyEditFiles.Add(result);

            return result;
        }
        public Tuple<TextEditor, TabItem> AddMyControlsForExisting(string path, string? overrideTabNamePrefix = null)
        {
            var name = new FileInfo(path).Name;
            fileTypeLoader.LoadCurrent(path);
            var text = fileTypeLoader.CurrentText;
            text.IsReadOnly = false;
            text.Name = name.Replace(".", "");
            text.Visibility = Visibility.Visible;
            text.KeyDown += model.Text_KeyDown;
            text.TextChanged += model.Text_TextChanged;
            text.HorizontalAlignment = HorizontalAlignment.Stretch;
            text.VerticalAlignment = VerticalAlignment.Stretch;
            name = $"{overrideTabNamePrefix}{name}";
            var tab = WPFUtil.AddOrUpdateTab(name, tabFiles, fileTypeLoader.CurrentArea);
            model.ChangeTab(tab);
            var result = new Tuple<TextEditor, TabItem>(text, tab);
            return result;
        }
    }
} 