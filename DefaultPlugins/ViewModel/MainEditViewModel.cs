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
using DefaultPlugins.ViewModel.MainActions;
using MS.WindowsAPICodePack.Internal;
using System.Diagnostics;

namespace ViewModel
{
    public partial class MainEditViewModel
    {
        private ProgressBar progressBar;
        private TabControl tabFiles;
        private TextBlock statusText;
        private NewFile newFile;
        private DoWhenTextChange textChange;
        private LoadFile loadFile;
        private SaveFile saveFile;
        private HexView hex;
        private ViewAs viewAsModel;
        
        //      private EventData viewData;


        private List<INamedActionPlugin> relevantPlugins;

        public MainEditViewModel(TabControl tab, ProgressBar bar, MenuItem viewAs,TextBlock statusText)
        {
            this.progressBar = bar;

            this.viewAsModel = new ViewAs(this.progressBar);

            tabFiles = tab;
            tabFiles.SelectionChanged += TabFiles_SelectionChanged;
            this.statusText = statusText;
            newFile = new NewFile(this, tab);
            textChange=new DoWhenTextChange();
            this.loadFile = new LoadFile(this, bar, tab, viewAsModel);
            this.saveFile = new SaveFile(this, this.progressBar);
            this.hex = new HexView(this, this.progressBar, this.tabFiles, viewAsModel);



            new MenuBuilder(this.viewAsModel).BuildAndLoadMenu();
            
            relevantPlugins = AllPlugins.InvokePlugins(PluginManager.GetPluginsLocal())
                .Where(p => p.Configuration.KeyCommand.Keys.Count > 0).ToList();

        }

        public void SetStatusText(string statusText)
        {
            this.statusText.Text = statusText;
            
        }
     
        public async Task<string> DoCommand(MenuItem item)
        {
            string result = String.Empty;
            var header=item!=null && item.Header!=null ? item.Header.ToString() : String.Empty;
            
            var myMenuItem = MenuBuilder.MainMenu.SelectMany(p => p.MenuItems)
                 .Single(p => p != null && p.Title!=null && p.Title ==header);
           
            var actions = new SetupMenuActions(this, tabFiles, progressBar, viewAsModel);
            actions.SetMenuAction(myMenuItem);
            if (myMenuItem.MenuActon != null)
                await myMenuItem.MenuActon.Invoke();
           else
                result=await HandleOtherPlugins(myMenuItem);
            
            return result;
        }
        public async Task<string> HandleOtherPlugins(  CustomMenuItem? myMenuItem)
        {
            var progress = new Progress<long>(value => progressBar.Value = value);

            string selectedText = loadFile.CurrentText != null ? loadFile.CurrentText.SelectedText : String.Empty;
            string result=String.Empty;
            if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.ParameterIsSelectedText && selectedText.HasContent())
                result = await myMenuItem.Plugin.Perform(new ActionParameter(selectedText), progress);
            else if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.Parameter != null)
                result = await myMenuItem.Plugin.Perform(myMenuItem.Plugin.Parameter, progress);
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