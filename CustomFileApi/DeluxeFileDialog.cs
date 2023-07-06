using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomFileApiFile
{
     public class DeluxeFileDialog
    {
        public string? ShowFileOpenDialog()
        {
            var dialog = new MyOpenFileDialogControl();
            var dummy = new Form();
            dialog.ShowDialog(dummy);
            return null;

        }
    }
}
