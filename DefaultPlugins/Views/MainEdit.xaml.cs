using DefaultPlugins.ViewModel.MainActions;
using Model;
using System.Windows;
using System.Windows.Controls;
using ViewModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Views
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
            editViewModel = new MainEditViewModel(TabFiles, Progress, viewAs , StatusText);
                

        }


        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            
            var builder = new MenuBuilder(new ViewAs(viewAs ));
            
            builder.ShowMenu(this.MainMenu, MenuBuilder.MainMenu);

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

        private void viewAs_Click(object sender, RoutedEventArgs e)
        {
           

        }
    }

}