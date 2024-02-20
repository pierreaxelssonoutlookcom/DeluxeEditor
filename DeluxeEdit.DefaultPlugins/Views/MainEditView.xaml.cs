using DefaultPlugins.ViewModel;
using Model;
using System.Windows.Controls;
using DeluxeEdit.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Formats.Tar;

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
        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            MainEditViewModel.CurrenContent = editViewModel.KeyDown();
            if (MainEditViewModel.CurrenContent == null) return;



        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainEditViewModel.CurrenContent = MainEditViewModel.AllContents.First(p => p.Header == (e.Source as TabItem).Header);
            editViewModel.ChangeTab(MainEditViewModel.CurrenContent);

            MainEditViewModel.AllContents.Add(MainEditViewModel.CurrenContent);

            editViewModel.ChangeTab(MainEditViewModel.CurrenContent);

            // addTab(MainEditViewModel.CurrenContent.Header);
            ///       //     MainEditViewModel.AllContents.Add(MainEditViewModel.CurrenContent);


        }

        private void MainEditBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            editViewModel.ScrollTo(e.NewValue);
            MainEditBox.Text = MainEditViewModel.CurrenContent.Content;
        }


        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            editViewModel = new MainEditViewModel();
            var menu = editViewModel.LoadMenu();



            foreach (var item in menu)
            {
                int index = MainMenu.Items.Add(new MenuItem { Header = item.Header });

                foreach (var inner in item.MenuItems)
                {
                    MenuItem newExistMenuItem = (MenuItem)this.MainMenu.Items[index];
                    var newItem = new MenuItem { Header = inner.Title };
                    newItem.Click += MenuItem_Click;



                    newExistMenuItem.Items.Add(newItem);



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
                MainEditViewModel.AddOrUpddateTab(keyeddata.Header, TabFiles);
                e.Handled = true;
            }


        }

        private void TabFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainEditViewModel.CurrenContent = MainEditViewModel.AllContents.First(p => p.Header == (e.Source as TabItem).Header);

        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var clicked= e.Source as MenuItem;
            var mymenu=MainEditViewModel.MainMenu.SelectMany(p=>p.MenuItems).First(p=>p.Title==clicked.Header) ;
            editViewModel.DoCommand(mymenu);


        }
    }
}

