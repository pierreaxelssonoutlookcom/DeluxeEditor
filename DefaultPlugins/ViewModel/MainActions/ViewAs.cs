using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Xml.Serialization;
using ViewModel;

namespace DefaultPlugins.ViewModel.MainActions
{
    public class ViewAs
    {
        private MenuItem root;
        private List<CustomMenuItem> MenuItemsForFileTypes= new List<CustomMenuItem>();
        public ViewAs(MenuItem menu)
        {
            this.root = menu;
        }

        public List<CustomMenuItem> GetMenuItemsForFileTypes()
        {
            var result = FileTypeLoader.AllFileTypes.Select(p =>
            new CustomMenuItem { Title = p.ToString(), FileType = p.FileType, IsCheckable = true, IsChecked=false, CheckBox=new CheckBox() }).ToList();
            return result;
        }

        public CustomMenuItem Load()
        {
            MenuItemsForFileTypes = GetMenuItemsForFileTypes();

            MenuItemsForFileTypes.ForEach(p => root.Items.Add(p.Title));
            CustomMenuItem result= new CustomMenuItem(root);
           return result;
            /*
            foreach(var item in  menuItemTypes)
            {
                item.Click += FileTypeClick;
            }

            }
            */
        }

        private void FileTypeClick(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SetSelectedPath(string path)
        {
            var withTypes = MenuItemsForFileTypes.Where(p => p.FileType != null);
            var currentFileItem = FileTypeLoader.AllFileTypes.FirstOrDefault(p => path.EndsWith(p.FileExtension, StringComparison.OrdinalIgnoreCase));
            if (currentFileItem!=null)
            {

               var match=withTypes.First(p => p.FileType == currentFileItem.FileType);
                match.IsChecked = true;
            }
               
            


        }
    }
}
