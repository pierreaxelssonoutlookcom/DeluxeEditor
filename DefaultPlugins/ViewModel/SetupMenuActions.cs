using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ViewModel;

namespace DefaultPlugins.ViewModel
{
    public class SetupMenuActions
    {
        private MainEditViewModel model;

            public SetupMenuActions(MainEditViewModel model)
            {
                this.model=model;
            }
            public void SetMenuActions(List<CustomMenu> menu)
            {
                var allItems = menu.SelectMany(p => p.MenuItems);
                foreach (var item in allItems)
                {
                    item.MenuActon = (dummy) => model.SaveFile();

                    if (item.Plugin is FileNewPlugin)
                        item.MenuActon = (dummy) => model.NewFile();
                    else if (item.Plugin is FileOpenPlugin)
                        item.MenuActon = (dummy) => model.LoadFile();
                    else if (item.Plugin is FileSavePlugin)
                        item.MenuActon = (dummy) => model.SaveFile();
                    else if (item.Plugin  is FileSaveAsPlugin)
                        item.MenuActon = (dummy) => model.SaveAsFile();
                    else if (item.Plugin is HexPlugin)
                        item.MenuActon = (dummy) => model.HexView();
                }

            }

        }

        } 
    
