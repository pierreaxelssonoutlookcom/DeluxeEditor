using Model.Interface;
using Model;
using System;

using Extensions;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Shared;
using System.Windows.Controls;
using DeluxeEdit.DefaultPlugins;
using MS.WindowsAPICodePack.Internal;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace DefaultPlugins.ViewModel
{
    public class NewFileViewModel
    {
        private INamedActionPlugin plugin;
        private TabControl currentTab;

        public NewFileViewModel(TabControl tab)
        {
            plugin = AllPlugins.InvokePlugin(PluginType.FileNew);
            currentTab = tab;
        }
        public TextBox AddNewTextControlAndListen(string name)
        {

            WPFUtil.AddOrUpddateTab(name, currentTab);

            var text = new TextBox();
            text.Name = name;
            text.KeyDown += Text_KeyDown;
            currentTab.Items.Add(text);

            return text;
        }

        public ContentPath GetNewFile()
        {
               var result = new ContentPath { Header = "newfile.txt", Content = "" };
            MyEditFiles.Files.Add(new MyEditFile { Header = result.Header });
            var text=AddNewTextControlAndListen(result.Header);
            MyEditFiles.Files.Add(
               new MyEditFile
               {
                   Path = result.Path,
                   Content = result.Content,
                   Header = result.Header,
                   Text = text,
                   Tab = currentTab.Items.CurrentItem,
                   IsNewFile = true
               });



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
        public ContentPath? KeyDown()
        {
            //done:cast enum from int
            ContentPath result = null;
            bool keysOkProceed = false;
            var matchCount = plugin.Configuration.KeyCommand.Keys
                .Cast<System.Windows.Input.Key>()
                .Count(p => System.Windows.Input.Keyboard.IsKeyDown(p));

            keysOkProceed = matchCount == plugin.Configuration.KeyCommand.Keys.Count && plugin.Configuration.KeyCommand.Keys.Count > 0;
            if (keysOkProceed) result = GetNewFile();


            return result;


        }
    }
}