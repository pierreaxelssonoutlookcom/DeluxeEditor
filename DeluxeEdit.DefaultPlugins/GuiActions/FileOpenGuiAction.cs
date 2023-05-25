using DeluxeEdit.Model;
using System;
using Microsoft.Win32;
namespace DeluxeEdit.DefaultPlugins.GuiActions
{
    public  class FileOpenGuiAction
    {
        
        public FileOpenGuiAction() 
        {
            // Configure open file dialog box
            OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
            }
        }
        public string GuiAction(ActionParameter parameter)
        {
            return "";
        }
    }

}   


    