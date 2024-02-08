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
            var  menu = editViewModel.LoadMenu();



            foreach (var item in menu )
            {
                int index= MainMenu.Items.Add( new MenuItem {  Header= item.Header });

                foreach (var inner in item.MenuItems)
                {
                    MenuItem newExistMenuItem = (MenuItem)this.MainMenu.Items[index];
                    newExistMenuItem.Items.Add(new MenuItem { Header = inner.Title });



                }





            }

        }

        private void MainEditBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var keyeddata = editViewModel.KeyDown();
            if (keyeddata == null) e.Handled = false;
            else
            {
                MainEditBox.Text = keyeddata.Content;
                e.Handled = true;
            }

            
        }
    }
}
