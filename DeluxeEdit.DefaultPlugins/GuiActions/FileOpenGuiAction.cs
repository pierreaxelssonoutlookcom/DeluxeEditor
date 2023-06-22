using DeluxeEdit.Model;
using System;
//using Microsoft.Win32;
using DeluxeEdit.Model.Interface;

namespace DeluxeEdit.DefaultPlugins.GuiActions
{
    public  class FileOpenGuiAction
    {
        
        public FileOpenGuiAction() 
        {
        }
        public string? GuiAction(INamedActionPlugin parameter)
        {
//            (new MvvmDialogs.DialogService()).ShowDialog(this, this);
            /* Configure open file dialog box
            OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
             dlg.AddExtension = false;
             dlg.FileName = ""; // Default file name
             dlg.DefaultExt = ""; // Default file extension
             dlg.Filter = "All files |*.*"; // Filter files by extension

             // Show open file dialog box
             Nullable<bool> result = dlg.ShowDialog();

             // Process open file dialog box results
             if (result == true)
             {
                 // Open document
                 string filename = dlg.FileName;
             }


             string? retval= result==true ? dlg.FileName : null;
             return retval;
            */
            return null;
        }
    }

}   


    