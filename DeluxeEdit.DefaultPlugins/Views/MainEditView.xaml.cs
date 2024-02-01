using DefaultPlugins.ViewModel;
using Model;
using System.Windows.Controls;
using Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DefaultPlugins.Views
{
    /// <summary>
    /// Interaction logic for MainEdit.xaml
    /// </summary>
    public partial class MainEdit : UserControl
    {
        private MainEditViewModel editViewModel;
        private Dictionary<string, ConfigurationOptions> menuConfig;

        public MainEdit()
        {
            InitializeComponent();

            // temporary call
            //currenContents =editViewModel.UpdateLoad();
        }
        public void addTab(string header)
        {
             var item=new TabItem { Header = header };
            TabFiles.Items .Add(item);
        }

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            MainEditViewModel.CurrenContent = editViewModel.KeyDown();
            if (MainEditViewModel.CurrenContent == null) return;


            addTab(MainEditViewModel.CurrenContent.Header);
             MainEditViewModel.AllContents.Add(MainEditViewModel.CurrenContent);

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string head = (TabFiles.SelectedItem as TabItem).Header as string;
             MainEditViewModel.CurrenContent= MainEditViewModel.AllContents.First(p => p.Header==head);
            editViewModel.ChangeTab( MainEditViewModel.CurrenContent);

        }

        private void MainEditBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            editViewModel.ScrollTo(e.NewValue);
            MainEditBox.Text = MainEditViewModel.CurrenContent.Content;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            editViewModel = new MainEditViewModel();
            menuConfig = editViewModel.LoadMenu();
            foreach (var key in menuConfig.Keys)
            {
                var myMenu = MainMenu.Items.Add(key);
                var menuItem = new System.Windows.Controls.MenuItem();
                menuItem.Header= $" {menuConfig[key].ShowInMenuItem}  ( {menuConfig[key].KeyCommand} )";
                MainMenu.Items.Add(menuItem);

            }

        }
    }
}
