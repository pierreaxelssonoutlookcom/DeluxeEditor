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
            editViewModel = new MainEditViewModel();
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
    }
}
