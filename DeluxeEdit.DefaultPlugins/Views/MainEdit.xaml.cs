using DefaultPlugins.ViewModel;
using DeluxeEdit.DefaultPlugins.ViewModel;
using Model;
using System;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography;
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
        private NewFileViewModel newViewModel;

        public MainEdit()
        {
            InitializeComponent();
            
            // temporary call
            //currenContents =editViewModel.UpdateLoad();
        }
        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {



        }

 
        private void MainEditBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            editViewModel.ScrollTo(e.NewValue); 
        }
        private void EditFile()
        {
            var text = MyEditFiles.Current.Text as TextBox;
            text.Text = CustomViewData.EditFile.Content;


        }
        private void NewFile()
        {
            var text = MyEditFiles.Current.Text as TextBox;
            text.Text = CustomViewData.NewFile.Content;


        }
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            editViewModel = new MainEditViewModel(TabFiles);

            newViewModel = new NewFileViewModel(TabFiles);
            newViewModel.AddNewTextControlAndListen("newfile.txt");
            var customMenu = editViewModel.GetMenu();

            var builder = new MenuBuilder();

            builder.ShowMenu(this.MainMenu, customMenu);
            foreach (MenuItem item in MainMenu.Items)
                item.Click += MenuItem_Click;

           var viewData = new CustomViewData();
            viewData.subscrile(OVEvent);
                
       }
       private  void OVEvent(EventType type)
        {
            if (type == EventType.EditFile)
                EditFile();
            else if (type==EventType.NewFile)
                NewFile();


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
            editViewModel.ChangeTab(e.Source as TabItem);

        }
    }

}
