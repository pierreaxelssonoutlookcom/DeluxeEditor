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
        private ContentPath currenContents;
        private List<ContentPath>  allContents;

        public MainEdit()
        {
            InitializeComponent();
            editViewModel = new MainEditViewModel();
            // temporary call
            currenContents =editViewModel.UpdateLoad();
            allContents = new List<ContentPath>();
        }
        public void addTab(string header)
        {
             var item=new TabItem { Header = header };
            TabFiles.Items .Add(item);
        }

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            currenContents = editViewModel.KeyDown();
            if (currenContents == null) return;

            addTab(currenContents.Header);
            MainEditBox.Text = currenContents.Content;
            allContents.Add(currenContents);

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string head = (TabFiles.SelectedItem as TabItem).Header as string;
            currenContents=  allContents.First(p => p.Header==head);
            editViewModel.ChangeTab(currenContents);

        }
    }
}
