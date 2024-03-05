using DefaultPlugins.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeluxeEdit.DefaultPlugins.Views
{
    /// <summary>
    /// Interaction logic for Plugins.xaml
    /// </summary>
    public partial class Plugins : UserControl
    {
        private PluginsViewModel viewModel;

        public Plugins()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel= new PluginsViewModel();
            if (ShowFiles.IsChecked.Value)
                data.ItemsSource = viewModel.LocalListFiles();
            else
                data.ItemsSource = viewModel.LocalListFiles() ;



        }
    }
}
