using DefaultPlugins.ViewModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DefaultPlugins.Views
{
    /// <summary>
    /// Interaction logic for Plugins.xaml
    /// </summary>
    public partial class Plugins : UserControl
    {
        PluginViewModel pluginViewModel;
        public Plugins()
        {
            InitializeComponent();
            pluginViewModel = new PluginViewModel();
            LocalList();
        }
        private void LocalList()
        {
            pluginList.DataContext = pluginViewModel.LocalList();
        }
        private void RemoteList()
        {
            pluginList.DataContext = pluginViewModel.RemoteList();
        }
    }
}
