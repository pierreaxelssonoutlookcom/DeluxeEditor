using DeluxeEdit.DefaultPlugins.Managers;
using DeluxeEdit.DefaultPlugins.ViewModel;
using DeluxeEdit.Model;
using DeluxeEdit.Model.Interface;
using System.Windows.Controls;
using DeluxeEdit.Extensions;

namespace DeluxeEdit.DefaultPlugins.Views
{
    /// <summary>
    /// Interaction logic for MainEdit.xaml
    /// </summary>
    public partial class MainEdit : UserControl
    {
        private MainEditViewModel editViewModel;

        
        public MainEdit()
        {
            InitializeComponent();
            editViewModel = new MainEditViewModel();
            editViewModel.UpdateLoad();
        }


        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            MainEditBox.Text=editViewModel.KeyDown();
        }
    }
}
