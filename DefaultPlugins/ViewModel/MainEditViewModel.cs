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
using System.Windows;

namespace ViewModel
{
    public partial class MainEditViewModel
    {
        private ProgressBar progressBar;
        private TabControl tabFiles;
        private TextBlock progressText, statusText;
        private NewFile newFile;
        private DoWhenTextChange textChange;
        private LoadFile loadFile;
        private SaveFile saveFile;
        private HexView hex;
        private EventData viewData;
      

        private List<INamedActionPlugin> relevantPlugins;

        public MainEditViewModel(TabControl tab, ProgressBar bar, TextBlock progressText, TextBlock statusText)
        {
            this.progressBar = bar;
            tabFiles = tab;
            tabFiles.SelectionChanged += TabFiles_SelectionChanged;
            this.progressText = progressText;
            this.statusText = statusText;
            newFile = new NewFile(this, tab);
            textChange=new DoWhenTextChange();
            this.loadFile = new LoadFile(this, bar, tab);
            this.saveFile = new SaveFile(this, this.progressBar);
            this.hex = new HexView(this, this.progressBar, this.tabFiles);
            viewData = new EventData();

            viewData.Subscibe(OnEvent);
            relevantPlugins = AllPlugins.InvokePlugins(PluginManager.GetPluginsLocal())
                .Where(p => p.Configuration.KeyCommand.Keys.Count > 0).ToList();

        }

        public void SetStatusText(string statusText)
        {
            this.statusText.Text = statusText;
            
        }
        public FileTypeItem? ExecuteViewAs(CustomMenuItem menuItem)
        {
            var type = menuItem.FileType;
            throw new NotImplementedException();            
        }
     
        public async Task<string> DoCommand(MenuItem item)
        {
            string result = "";
            var header=item!=null && item.Header!=null ? item.Header.ToString() : String.Empty;
            var progress = new Progress<long>(value => progressBar.Value = value);
            
            var myMenuItem = MenuBuilder.MainMenu.SelectMany(p => p.MenuItems)
                .Single(p => p != null && p.Title!=null && p.Title ==header);
            //CheckForViewAs(myMenuItem.Title)
            var actions = new SetupMenuActions(this, tabFiles, progressBar);
            actions.SetMenuAction(myMenuItem);
            if (myMenuItem.MenuActon != null)
                await myMenuItem.MenuActon.Invoke();
           else
            {
                string selectedText = loadFile.CurrentText != null ? loadFile.CurrentText.SelectedText : String.Empty;

                var viewasResult = ExecuteViewAs(myMenuItem);

                if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.ParameterIsSelectedText && selectedText.HasContent())
                    result = await myMenuItem.Plugin.Perform(new ActionParameter(selectedText), progress);
                else if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.Parameter != null)
                    result = await myMenuItem.Plugin.Perform(myMenuItem.Plugin.Parameter, progress);

            }
            return result;
        }
        public void ChangeTab(TabItem item)
        {
            if (item == null) throw new NullReferenceException();
            tabFiles.SelectedItem=item;
            var header = item != null && item.Header != null ? item.Header.ToString() : String.Empty;

            MyEditFiles.Current = MyEditFiles.Files.FirstOrDefault(p => p.Header == header );

        }
    }
}