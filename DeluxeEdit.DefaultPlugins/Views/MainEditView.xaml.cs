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
            editViewModel.ShowMenu(this.MainMenu);
            foreach(var item in MainMenu.Items)
                item.Click += MenuItem_Click;




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
            editViewModel.DoCommand(clicked);


        }
    }
}

