﻿using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ViewModel;

namespace DefaultPlugins.ViewModel.MainActions
{
    public class ViewAs
    {
        private MenuItem root;

        public ViewAs(MenuItem menu)
        {
            this.root = menu;
        }

        public List<CustomMenuItem> GetMenuItemsForFileTypes()
        {
            var result = FileTypeLoader.AllFileTypes.Select(p =>
            new CustomMenuItem { Title = p.ToString(), FileType = p.FileType, IsCheckable = true, IsChecked=true }).ToList();
            return result;
        }

        public void Load()
        {
            var menuItemTypes = GetMenuItemsForFileTypes();
            menuItemTypes.ForEach(p => root.Items.Add(p.Title) );









        }

        public void SetSelectedPath(string path)
        {
            /*
            FileTypeItem? currentFileItem;
            if (path.HasContent())
            {
                currentFileItem = FileTypeLoader.AllFileTypes.FirstOrDefault(p => path.EndsWith(p.FileExtension, StringComparison.OrdinalIgnoreCase));

                 viewAs.SelectedItem = currentFileItem;
            }
                
            (*/


        }
    }
}
