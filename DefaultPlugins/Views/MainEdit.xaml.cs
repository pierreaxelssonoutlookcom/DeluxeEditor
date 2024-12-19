using Model;
using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace Views
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
            editViewModel = new MainEditViewModel(TabFiles, Progress, ProgressText, StatusText);
                
            newViewModel = new NewFileViewModel(TabFiles);

        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var menu = editViewModel.GetMenu();
            var builder = new MenuBuilder();

            builder.ShowMenu(this.MainMenu, menu);

            foreach (MenuItem item in MainMenu.Items)
                item.Click += MenuItem_Click;

                
       }

        

        private async void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (e.Source is MenuItem)
            {
                var clicked = e.Source is MenuItem ? e.Source as MenuItem : null;

                 if (clicked!=null) await editViewModel.DoCommand(clicked);

            }
        }

        private void ShowPlugins_Click(object sender, RoutedEventArgs e)
        {
            Plugins.CreateAndShow();

        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
             var data = await editViewModel.LoadFile();
            
            
            
            
            
            
            
            
            
    //        MainEditBox.Text = data.Content;
//

        }
    }

}