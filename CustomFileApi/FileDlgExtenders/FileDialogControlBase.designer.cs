namespace CustomFileApiFile
{
    public partial class FileDialogControlBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Encoding = new System.Windows.Forms.Label();
            colorDialog1 = new System.Windows.Forms.ColorDialog();
            cmbEncoding = new System.Windows.Forms.ComboBox();
            SuspendLayout();
            // 
            // Encoding
            // 
            Encoding.AutoSize = true;
            Encoding.Location = new System.Drawing.Point(0, 0);
            Encoding.Name = "Encoding";
            Encoding.Size = new System.Drawing.Size(86, 25);
            Encoding.TabIndex = 0;
            Encoding.Text = "Encoding";
            // 
            // cmbEncoding
            // 
            cmbEncoding.FormattingEnabled = true;
            cmbEncoding.Location = new System.Drawing.Point(91, 8);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Size = new System.Drawing.Size(182, 33);
            cmbEncoding.TabIndex = 1;
            // 
            // FileDialogControlBase
            // 
            Controls.Add(cmbEncoding);
            Controls.Add(Encoding);
            Name = "FileDialogControlBase";
            Size = new System.Drawing.Size(555, 385);
            Load += FileDialogControlBase_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label Encoding;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox cmbEncoding;

        #endregion


        //protected System.Windows.Forms.OpenFileDialog _dlgOpen;

        

    }
}