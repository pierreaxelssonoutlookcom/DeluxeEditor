using Model;
using System.Windows;
using DefaultPlugins;

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

 
            var plugin = AllPlugins.InvokePlugin(PluginType.FileOpen);
            Content= plugin.CreateControl(false);


        }


        private void Plugins_Click(object sender, RoutedEventArgs e)
        {
       }

        private void PluginFiles_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
