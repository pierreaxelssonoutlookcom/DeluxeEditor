using CustomFileApi.Model;
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
        public EncodingPath? ShowFileOpenDialog()
        {
            EncodingPath? result;
            var dialog = new MyOpenFileDialogControl();
            var dummyForm = new Form();
            var dialogResult=dialog.ShowDialog(dummyForm);

            if (dialogResult == DialogResult.OK) 
            {
                result = new EncodingPath { Path = dialog.MSDialog.FileName };
                result.Encoding = dialog.WantedEncoding != null ? (Encoding?)(Encoding.GetEncoding(dialog.WantedEncoding)): null; 
            }
            
            return null;

        }
    }
}
