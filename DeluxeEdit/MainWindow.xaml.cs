using Model;
using DefaultPlugins.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
          //  Content = new MainEdit();
        }


        private void Plugins_Click(object sender, RoutedEventArgs e)
        {
            var plugins = new Plugins( );
            Content = plugins;
      ;  }

        private void PluginFiles_Click(object sender, RoutedEventArgs e)
        {
            var plugins = new PluginsFiles();
            Content = plugins;

        }
    }
}
