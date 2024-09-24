//  Copyright (c) 2006, Gustavo Franco
//  Copyright © Decebal Mihailescu 2007-2010

//  Email:  gustavo_franco@hotmail.com
//  All rights reserved.

//  Redistribution and use in source and binary forms, with or without modification, 
//  are permitted provided that the following conditions are met:

//  Redistributions of source code must retain the above copyright notice, 
//  this list of conditions and the following disclaimer. 
//  Redistributions in binary form must reproduce the above copyright notice, 
//  this list of conditions and the following disclaimer in the documentation 
//  and/or other materials provided with the distribution. 

//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.

using DeluxeEdit.Extensions;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CustomFileApiFile.FileDlgExtenders;


namespace CustomFileApiFile
{
    public partial class MySaveDialogControl : FileDialogControlBase
    {
     
        public MySaveDialogControl(string? initialDirectory = null)
        {
            FileDlgInitialDirectory = initialDirectory.HasContent() ? initialDirectory : "";
            InitializeComponent();
            if (cmbEncoding != null) cmbEncoding.Items.AddRange(Encoding.GetEncodings().Select(p => p.Name).ToArray());
        }

        public string? WantedEncoding { get; set; }
        protected override void OnPrepareMSDialog()
        {

            if (Environment.OSVersion.Version.Major < 6)
                MSDialog.SetPlaces(new object[] { @"c:\", (int)Places.MyComputer, (int)Places.Favorites, (int)Places.Printers, (int)Places.Fonts, });
            base.OnPrepareMSDialog();

        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1807:AvoidUnnecessaryStringCreation", MessageId = "filePath")]



        private void MyOpenFileDialogControl_ClosingDialog(object sender, CancelEventArgs e)
        {
            if (cmbEncoding.SelectedIndex != -1)
                WantedEncoding = (string)cmbEncoding.SelectedItem;

            e.Cancel = false;
        }



        private void MyOpenFileDialogControl_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show("Please add some specific help here");
        }

        private void MyOpenFileDialogControl_Load(object sender, EventArgs e)
        {

        }
    }

}