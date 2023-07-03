using DeluxeEdit.CustomFileApiFile.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
namespace Deluxe.CustomFileApiFile
{
    public class FileDialog
   {
       public string? ShowFileOpenDialog()
        {
            var diag = new OpenFileDialogEncoding();
            diag.ShowDialog();
            return  null;
        }

    }
}