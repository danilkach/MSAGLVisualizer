/*
Microsoft Automatic Graph Layout,MSAGL 

Copyright (c) Microsoft Corporation

All rights reserved. 

MIT License 

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
namespace Microsoft.Msagl.GraphViewerGdi {
    partial class SaveViewAsImageForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.saveInTextBox = new System.Windows.Forms.TextBox();
            this.saveInLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.saveCurrentView = new System.Windows.Forms.RadioButton();
            this.saveTotalView = new System.Windows.Forms.RadioButton();
            this.imageScale = new System.Windows.Forms.TrackBar();
            this.imageSizeLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageScale)).BeginInit();
            this.SuspendLayout();
            // 
            // saveInTextBox
            // 
            this.saveInTextBox.Location = new System.Drawing.Point(91, 24);
            this.saveInTextBox.Name = "saveInTextBox";
            this.saveInTextBox.Size = new System.Drawing.Size(108, 20);
            this.saveInTextBox.TabIndex = 0;
            // 
            // saveInLabel
            // 
            this.saveInLabel.AutoSize = true;
            this.saveInLabel.Location = new System.Drawing.Point(13, 27);
            this.saveInLabel.Name = "saveInLabel";
            this.saveInLabel.Size = new System.Drawing.Size(72, 13);
            this.saveInLabel.TabIndex = 1;
            this.saveInLabel.Text = "Сохранить в:";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(205, 22);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Обзор";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // saveCurrentView
            // 
            this.saveCurrentView.AutoSize = true;
            this.saveCurrentView.Location = new System.Drawing.Point(25, 74);
            this.saveCurrentView.Name = "saveCurrentView";
            this.saveCurrentView.Size = new System.Drawing.Size(111, 17);
            this.saveCurrentView.TabIndex = 3;
            this.saveCurrentView.TabStop = true;
            this.saveCurrentView.Text = "Save current view";
            this.saveCurrentView.UseVisualStyleBackColor = true;
            this.saveCurrentView.Visible = false;
            // 
            // saveTotalView
            // 
            this.saveTotalView.AutoSize = true;
            this.saveTotalView.Location = new System.Drawing.Point(25, 98);
            this.saveTotalView.Name = "saveTotalView";
            this.saveTotalView.Size = new System.Drawing.Size(106, 17);
            this.saveTotalView.TabIndex = 4;
            this.saveTotalView.TabStop = true;
            this.saveTotalView.Text = "Save global view";
            this.saveTotalView.UseVisualStyleBackColor = true;
            this.saveTotalView.Visible = false;
            // 
            // imageScale
            // 
            this.imageScale.Location = new System.Drawing.Point(16, 58);
            this.imageScale.Maximum = 100;
            this.imageScale.Minimum = 1;
            this.imageScale.Name = "imageScale";
            this.imageScale.Size = new System.Drawing.Size(104, 45);
            this.imageScale.TabIndex = 5;
            this.imageScale.Value = 1;
            // 
            // imageSizeLabel
            // 
            this.imageSizeLabel.AutoSize = true;
            this.imageSizeLabel.Location = new System.Drawing.Point(126, 69);
            this.imageSizeLabel.Name = "imageSizeLabel";
            this.imageSizeLabel.Size = new System.Drawing.Size(70, 13);
            this.imageSizeLabel.TabIndex = 6;
            this.imageSizeLabel.Text = "Разрешение";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(16, 109);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(128, 23);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(149, 109);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(131, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SaveViewAsImageForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 139);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.imageSizeLabel);
            this.Controls.Add(this.imageScale);
            this.Controls.Add(this.saveTotalView);
            this.Controls.Add(this.saveCurrentView);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.saveInLabel);
            this.Controls.Add(this.saveInTextBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(308, 178);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(308, 178);
            this.Name = "SaveViewAsImageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Сохранить в .jpg";
            ((System.ComponentModel.ISupportInitialize)(this.imageScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox saveInTextBox;
        private System.Windows.Forms.Label saveInLabel;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.RadioButton saveCurrentView;
        private System.Windows.Forms.RadioButton saveTotalView;
        private System.Windows.Forms.TrackBar imageScale;
        private System.Windows.Forms.Label imageSizeLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ToolTip toolTip;

    }
}