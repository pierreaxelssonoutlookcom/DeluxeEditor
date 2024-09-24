using CustomFileApiFile.FileDlgExtenders;
using System.ComponentModel;
namespace CustomFileApiFile
{
    public partial class MyOpenFileDialogControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblSizeValue = new System.Windows.Forms.Label();
            lblFormatValue = new System.Windows.Forms.Label();
            cmbEncoding = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // lblSizeValue
            // 
            lblSizeValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblSizeValue.Location = new System.Drawing.Point(53, 537);
            lblSizeValue.Name = "lblSizeValue";
            lblSizeValue.Size = new System.Drawing.Size(178, 13);
            lblSizeValue.TabIndex = 8;
            // 
            // lblFormatValue
            // 
            lblFormatValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblFormatValue.Location = new System.Drawing.Point(53, 519);
            lblFormatValue.Name = "lblFormatValue";
            lblFormatValue.Size = new System.Drawing.Size(178, 13);
            lblFormatValue.TabIndex = 7;
            // 
            // cmbEncoding
            // 
            cmbEncoding.FormattingEnabled = true;
            cmbEncoding.Location = new System.Drawing.Point(90, 537);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Size = new System.Drawing.Size(182, 33);
            cmbEncoding.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(5, 546);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(86, 25);
            label1.TabIndex = 10;
            label1.Text = "Encoding";
            // 
            // MyOpenFileDialogControl
            // 
            BackColor = System.Drawing.SystemColors.Control;
            BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            Controls.Add(label1);
            Controls.Add(cmbEncoding);
            Controls.Add(lblSizeValue);
            Controls.Add(lblFormatValue);
            FileDlgCaption = "Open text file";
            FileDlgDefaultViewMode = FolderViewMode.Thumbnails;
            FileDlgEnableOkBtn = false;
            FileDlgFileName = "Open text file";
            FileDlgFilterIndex = 2;
            FileDlgOkCaption = "Open";
            ImeMode = System.Windows.Forms.ImeMode.NoControl;
            Name = "MyOpenFileDialogControl";
            Size = new System.Drawing.Size(271, 588);
            EventClosingDialog += MyOpenFileDialogControl_ClosingDialog;
            Load += MyOpenFileDialogControl_Load;
            HelpRequested += MyOpenFileDialogControl_HelpRequested;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label lblSizeValue;
        private System.Windows.Forms.Label lblFormatValue;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Label label1;
    }
}