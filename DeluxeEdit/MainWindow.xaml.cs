using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DefaultPlugins;
using System.IO;
using DefaultPlugins;
using System;

namespace DeluxeEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       public MainWindow()
        {
            InitializeComponent();
            //todo:add usercontols dynamically

            var plugin = AllPlugins.InvokePlugin(PluginType.FileOpen) as FileOpenPlugin;
            UserControl edit=plugin.CreateControl() as UserControl;


            Content = edit; ;
        }


        private void Plugins_Click(object sender, RoutedEventArgs e)
        {
       }

        private void PluginFiles_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
