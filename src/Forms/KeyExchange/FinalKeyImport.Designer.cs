﻿namespace APEEEC_Outlook_AddIn.src.Forms.KeyExchange
{
    partial class FinalKeyImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.certificationNotificationTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // certificationNotificationTextBox
            // 
            this.certificationNotificationTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.certificationNotificationTextBox.Location = new System.Drawing.Point(12, 12);
            this.certificationNotificationTextBox.Multiline = true;
            this.certificationNotificationTextBox.Name = "certificationNotificationTextBox";
            this.certificationNotificationTextBox.Size = new System.Drawing.Size(260, 199);
            this.certificationNotificationTextBox.TabIndex = 3;
            this.certificationNotificationTextBox.Text = "Congratulations!\r\n\r\nThe key was successfully imported from the message.\r\n\r\nYou ca" +
    "n now send encrypted e-mails to the correspondent.";
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(197, 227);
            this.closeButton.Name = "closeButton";
            this.closeButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // FinalKeyImport
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.certificationNotificationTextBox);
            this.Name = "FinalKeyImport";
            this.Text = "FinalKeyImport";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox certificationNotificationTextBox;
        private System.Windows.Forms.Button closeButton;
    }
}