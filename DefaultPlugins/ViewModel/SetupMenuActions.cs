using Model;
using System.Linq;
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


        public void SetMenuAction(CustomMenuItem item)
            {
                if (item !=null && model!=null && item.Plugin is FileNewPlugin)
                    item.MenuActon = () => model.NewFile();
                else if (item != null && model != null && item.Plugin is FileOpenPlugin)
                    item.MenuActon = () => model.LoadFile();
                else if (item != null && model != null && item.Plugin is FileSavePlugin)
                    item.MenuActon = () => model.SaveFile();
                else if (item != null && model != null && item.Plugin  is FileSaveAsPlugin)
                    item.MenuActon = () => model.SaveAsFile();
                else if (item != null && model != null && item.Plugin is HexPlugin)
                    item.MenuActon = () => model.HexView();
                

            }

        }

        } 
    
