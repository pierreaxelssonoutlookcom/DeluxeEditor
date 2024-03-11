using DefaultPlugins.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace DeluxeEdit.DefaultPlugins.Views
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class MainEdit: UserControl
    {

        private MainEditViewModel editViewModel;

        public MainEdit()
        {
            InitializeComponent();
            
            // temporary call
            //currenContents =editViewModel.UpdateLoad();
        }
        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            MainEditViewModel.CurrenContent = editViewModel.KeyDown();
            if (MainEditViewModel.CurrenContent == null) return;



        }

 
        private void MainEditBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            editViewModel.ScrollTo(e.NewValue);
            MainEditBox.Text = MainEditViewModel.CurrenContent.Content;
        }


        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            editViewModel = new MainEditViewModel();
            editViewModel.ShowMenu(this.MainMenu);
            foreach (MenuItem item in MainMenu.Items)
                item.Click += MenuItem_Click;
           



        }

        private void MainEditBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var keyeddata = editViewModel.KeyDown();
            if (keyeddata == null) e.Handled = false;
            else
            {
                MainEditBox.Text = keyeddata.Content;

             WPFUtil.AddOrUpddateTab(keyeddata.Header, TabFiles);
                e.Handled = true;
            }


        }


        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            var clicked =   
                e.Source as MenuItem;
            editViewModel.DoCommand(clicked, MainEditBox.SelectedText);


        }

        private void ShowPlugins_Click(object sender, RoutedEventArgs e)
        {
            Plugins.CreateAndShow();

        }

        private void TabFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabFiles.Items.Count <= 1) return;
            MainEditViewModel.CurrenContent = MainEditViewModel.AllContents.First(p => p.Header == (e.Source as TabItem).Header);
            editViewModel.ChangeTab(MainEditViewModel.CurrenContent);

        }
    }

}
