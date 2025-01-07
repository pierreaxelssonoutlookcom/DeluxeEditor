using DefaultPlugins;
using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ViewModel
{
    public class SaveFile
    {
        private MainEditViewModel model;
        private ProgressBar progressBar;
        private FileSaveAsPlugin saveAs;
        private FileSavePlugin save;

        public SaveFile(MainEditViewModel model, ProgressBar progressBar)
        {
            saveAs = AllPlugins.InvokePlugin<FileSaveAsPlugin>(PluginType.FileSaveAs);
            save = AllPlugins.InvokePlugin<FileSavePlugin>(PluginType.FileSave);

            this.model = model;
            this.progressBar = progressBar;
            
        }
        public async Task<MyEditFile?> Save()
        {
            if (MyEditFiles.Current == null || MyEditFiles.Current.Text == null) throw new NullReferenceException();

            var progress = new Progress<long>(value => progressBar.Value = value);
            bool fileExist = File.Exists(MyEditFiles.Current.Path);
            if (fileExist)
                await save.Perform(new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Text.Text), progress);
            else
                await SaveAs();

            return null;

        }
        public async Task<MyEditFile?> SaveAs()
        {

            if (MyEditFiles.Current == null || MyEditFiles.Current.Text == null) throw new NullReferenceException();
            var progress = new Progress<long>(value => progressBar.Value = value);


            var action = saveAs.GuiAction(saveAs);
            //if user cancelled pat
            //h is empty 
            if (action == null || !action.Path.HasContent()) throw new NullReferenceException();

            await saveAs.Perform(new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Text.Text), progress);
            return null;

        }

    }
}
