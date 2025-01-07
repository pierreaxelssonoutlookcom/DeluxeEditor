using Model;
using System.Linq;
using System.Windows.Controls;
using ViewModel;

namespace DefaultPlugins.ViewModel
{
    public class SetupMenuActions
    {
        private MainEditViewModel model;
        private TabControl tabFiles;
        private ProgressBar progressBar;
        private LoadFile loadFile;

        public SetupMenuActions(MainEditViewModel model, TabControl tabControl, ProgressBar progress)
            {
                this.model=model;
            this.tabFiles=tabControl;
            this.progressBar=progress;
            this.loadFile=new LoadFile(this.model,  this.progressBar, this.tabFiles);
            }


        public void SetMenuAction(CustomMenuItem item)
            {
                if (item !=null && model!=null && item.Plugin is FileNewPlugin)
                    item.MenuActon = () => model.NewFile();
                else if (item != null && model != null && item.Plugin is FileOpenPlugin)
                    item.MenuActon = () => loadFile.Load();
                else if (item != null && model != null && item.Plugin is FileSavePlugin)
                    item.MenuActon = () => model.SaveFile();
                else if (item != null && model != null && item.Plugin  is FileSaveAsPlugin)
                    item.MenuActon = () => model.SaveAsFile();
                else if (item != null && model != null && item.Plugin is HexPlugin)
                    item.MenuActon = () => model.HexView();
                

            }

        }

        } 
    
