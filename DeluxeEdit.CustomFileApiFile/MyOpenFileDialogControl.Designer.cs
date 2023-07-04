using System.ComponentModel;
namespace CustomControls
{
    partial class MyOpenFileDialogControl
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MyOpenFileDialogControl));
            lblColors = new System.Windows.Forms.Label();
            lblFormat = new System.Windows.Forms.Label();
            lblSize = new System.Windows.Forms.Label();
            lblSizeValue = new System.Windows.Forms.Label();
            lblFormatValue = new System.Windows.Forms.Label();
            lblColorsValue = new System.Windows.Forms.Label();
            cmbEncoding = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // lblColors
            // 
            lblColors.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblColors.Location = new System.Drawing.Point(5, 33);
            lblColors.Name = "lblColors";
            lblColors.Size = new System.Drawing.Size(42, 13);
            lblColors.TabIndex = 3;
            lblColors.Text = "Colors:";
            // 
            // lblFormat
            // 
            lblFormat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblFormat.Location = new System.Drawing.Point(5, -3);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new System.Drawing.Size(42, 13);
            lblFormat.TabIndex = 4;
            lblFormat.Text = "Format:";
            // 
            // lblSize
            // 
            lblSize.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblSize.Location = new System.Drawing.Point(5, 15);
            lblSize.Name = "lblSize";
            lblSize.Size = new System.Drawing.Size(42, 13);
            lblSize.TabIndex = 5;
            lblSize.Text = "Size:";
            // 
            // lblSizeValue
            // 
            lblSizeValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblSizeValue.Location = new System.Drawing.Point(53, 15);
            lblSizeValue.Name = "lblSizeValue";
            lblSizeValue.Size = new System.Drawing.Size(178, 13);
            lblSizeValue.TabIndex = 8;
            // 
            // lblFormatValue
            // 
            lblFormatValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblFormatValue.Location = new System.Drawing.Point(53, -3);
            lblFormatValue.Name = "lblFormatValue";
            lblFormatValue.Size = new System.Drawing.Size(178, 13);
            lblFormatValue.TabIndex = 7;
            // 
            // lblColorsValue
            // 
            lblColorsValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblColorsValue.Location = new System.Drawing.Point(53, 33);
            lblColorsValue.Name = "lblColorsValue";
            lblColorsValue.Size = new System.Drawing.Size(178, 13);
            lblColorsValue.TabIndex = 6;
            // 
            // cmbEncoding
            // 
            cmbEncoding.FormattingEnabled = true;
            cmbEncoding.Location = new System.Drawing.Point(88, 24);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Size = new System.Drawing.Size(182, 33);
            cmbEncoding.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(7, 31);
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
            Controls.Add(lblColorsValue);
            Controls.Add(lblSize);
            Controls.Add(lblFormat);
            Controls.Add(lblColors);
            FileDlgCaption = "Select an Image";
            FileDlgDefaultViewMode = Win32Types.FolderViewMode.Thumbnails;
            FileDlgEnableOkBtn = false;
            FileDlgFileName = "Select Picture";
            FileDlgFilter = resources.GetString("$this.FileDlgFilter");
            FileDlgFilterIndex = 2;
            FileDlgOkCaption = "Select";
            ImeMode = System.Windows.Forms.ImeMode.NoControl;
            Name = "MyOpenFileDialogControl";
            Size = new System.Drawing.Size(270, 66);
            EventClosingDialog += MyOpenFileDialogControl_ClosingDialog;
            HelpRequested += MyOpenFileDialogControl_HelpRequested;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label lblColors;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblSizeValue;
        private System.Windows.Forms.Label lblFormatValue;
        private System.Windows.Forms.Label lblColorsValue;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Label label1;
    }
}