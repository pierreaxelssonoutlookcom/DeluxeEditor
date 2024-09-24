using CustomFileApiFile.FileDlgExtenders;
using System.ComponentModel;
namespace CustomFileApiFile
{
    public partial class MySaveDialogControl
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
            label1 = new System.Windows.Forms.Label();
            cmbEncoding = new System.Windows.Forms.ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(44, 420);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(86, 25);
            label1.TabIndex = 12;
            label1.Text = "Encoding";
            // 
            // cmbEncoding
            // 
            cmbEncoding.FormattingEnabled = true;
            cmbEncoding.Location = new System.Drawing.Point(129, 411);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Size = new System.Drawing.Size(182, 33);
            cmbEncoding.TabIndex = 11;
            // 
            // MySaveDialogControl
            // 
            BackColor = System.Drawing.SystemColors.Control;
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEncoding;
    }
}
