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
using DefaultPlugins.Model;

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

        public ComboControl AddMyContols(string filename)
        {
            var result = new ComboControl();

            result.Text = new TextBox();

            result.Text.Name = filename.Replace(".", "");
            result.Text.AcceptsReturn = true;
            result.Text.KeyDown += Text_KeyDown;

            result.ProgressBar = new ProgressBar();
            result.ProgressBar.Name = "progress" + filename.Replace(".", "");
            result.Panel = new StackPanel();
            result.Panel.Name = "panel" + filename.Replace(".", "");
            result.Panel.Orientation = Orientation.Vertical; ;
            result.Panel.Children.Add(result.ProgressBar);
            result.Panel.Children.Add(result.Text);
            WPFUtil.AddOrUpddateTab(filename, currentTab, result.Panel);
            // currentTab.Items.Add(resul

            return result;

        }





        public MyEditFile GetNewFile()
        {
               var result = new MyEditFile { Header = "newfile.txt", Content = "", IsNewFile=true };
            var combos= AddMyContols("newfile.txt");
            MyEditFiles.Files.Add(new MyEditFile { Header = result.Header });
            result.Text = combos.Text;

            MyEditFiles.Add(result);
               


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
        public MyEditFile? KeyDown()
        {
            //done:cast enum from int
            MyEditFile result = null;
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