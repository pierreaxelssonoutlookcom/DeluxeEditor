using Model;
using System.Text;
using System.Windows.Forms;

namespace CustomFileApiFile
{
    public class DeluxeFileDialog
    {
        public EncodingPath? ShowFileOpenDialog(string? initDir = null)
        {
            EncodingPath? result = null;
            using var dialog = new FileDialogControlBase(initDir) { FileDlgType = FileDialogType.SaveFileDlg };
            var dummyForm = new Form();
            var dialogResult = dialog.ShowDialog(dummyForm);

            if (dialogResult == DialogResult.OK)
            {
                result = new EncodingPath { Path = dialog.MSDialog.FileName };
                result.Encoding = dialog.WantedEncoding != null ? (Encoding?)(Encoding.GetEncoding(dialog.WantedEncoding)) : null;
            }

            return result;

        }
        public EncodingPath? ShowFileSaveDialog(string? initDir = null)
        {
            EncodingPath? result = null;
            using var dialog = new FileDialogControlBase(initDir);
            var dummyForm = new Form();
            var dialogResult = dialog.ShowDialog(dummyForm);

            if (dialogResult == DialogResult.OK)
            {
                result = new EncodingPath { Path = dialog.MSDialog.FileName };
                result.Encoding = dialog.WantedEncoding != null ? (Encoding?)(Encoding.GetEncoding(dialog.WantedEncoding)) : null;
            }

            return result;
        }
        } 
    }