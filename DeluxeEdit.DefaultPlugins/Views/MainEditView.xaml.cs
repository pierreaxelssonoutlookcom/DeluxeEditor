using DeluxeEdit.DefaultPlugins.Managers;
using DeluxeEdit.DefaultPlugins.ViewModel;
using DeluxeEdit.Model;
using DeluxeEdit.Model.Interface;
using System.Windows.Controls;


namespace DeluxeEdit.DefaultPlugins.Views
{
    /// <summary>
    /// Interaction logic for MainEdit.xaml
    /// </summary>
    public partial class MainEdit : UserControl
    {
        private MainEditViewModel editViewModel;
        private PluginManager manager;

        public MainEdit()
        {
            InitializeComponent();
            editViewModel = new MainEditViewModel();

            manager = new PluginManager();

        }

        public void UpdateBeforeLoad(ActionParameter parameter)
        {
            var plugin= manager.InvokePlugin<FileOpenPlugin>();

            editViewModel.Text=plugin.Perform(parameter);
            MainEditBox.Text = editViewModel.Text;
        }
        public void UpdateBeforeSave(INamedActionPlugin plugin, ActionParameter parameter)
        {


        }

    }
}
