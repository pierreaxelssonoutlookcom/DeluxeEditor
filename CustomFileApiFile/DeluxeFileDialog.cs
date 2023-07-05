using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFileApiFile
{
     public class DeluxeFileDialog
    {
        public string? ShowFileOpenDialog()
        {
            var dialog = new MyOpenFileDialogControl();
            dialog.ShowDialog();
            return null;

        }
    }
}
