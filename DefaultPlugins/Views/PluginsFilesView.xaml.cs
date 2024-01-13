using DefaultPlugins.ViewModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DefaultPlugins.Views
{
    /// <summary>
    /// Interaction logic for Plugins.xaml
    /// </summary>
    public partial class PluginsFiles : UserControl
    {
        PluginFileViewModel viewModel;
        public PluginsFiles()
        {
            
            InitializeComponent();
            viewModel = new PluginFileViewModel();

            LocalList();
        }
        private void LocalList()
        {
            pluginsGrid.ItemsSource = viewModel.LocalList();
        }
    }
}
