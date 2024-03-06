using DefaultPlugins.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace DeluxeEdit.DefaultPlugins.Views
{
    /// <summary>
    /// Interaction logic for Plugins.xaml
    /// </summary>
    public partial class Plugins : UserControl
    {
        private PluginsViewModel viewModel;
        public static void CreateAndShow()
        {
            var view = new Plugins();
            var win = new Window();
            win.Content = view;
            win.ShowDialog();
        }

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
