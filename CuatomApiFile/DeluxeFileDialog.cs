namespace CustomFileApiFile
{
    public class DeluxeFileDialog
    {
        public string? ShowFileOpenDialog()
        {
            var dialog = new MyOpenFileDialogControl();
            var result=dialog.ShowDialog();

            return null;
        }
    }
}
