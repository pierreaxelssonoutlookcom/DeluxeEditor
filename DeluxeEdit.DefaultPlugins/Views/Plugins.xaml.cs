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
        private void ShowDisplay()
        {
            if (ShowFiles.IsChecked.GetValueOrDefault())
                data.ItemsSource = viewModel.LocalListFiles();
            else
                data.ItemsSource = viewModel.LocalList();

        } 
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel= new PluginsViewModel();
            ShowDisplay();


        }

        private void ShowFiles_Click(object sender, RoutedEventArgs e)
        {
            ShowDisplay();
        }
    }
}
