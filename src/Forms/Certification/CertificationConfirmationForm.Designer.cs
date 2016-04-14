﻿namespace APEEEC_Outlook_AddIn.src.Forms.Certification
{
    partial class CertificationConfirmationForm
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
            this.confirmationNotificationTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // confirmationNotificationTextBox
            // 
            this.confirmationNotificationTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.confirmationNotificationTextBox.Location = new System.Drawing.Point(12, 12);
            this.confirmationNotificationTextBox.Multiline = true;
            this.confirmationNotificationTextBox.Name = "confirmationNotificationTextBox";
            this.confirmationNotificationTextBox.Size = new System.Drawing.Size(260, 199);
            this.confirmationNotificationTextBox.TabIndex = 1;
            this.confirmationNotificationTextBox.Text = "Thank you for creating a new Key-Pair.\r\n\r\nThe process was successful. You can clo" +
    "se this windows and start with the encryption of your messages.";
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(197, 227);
            this.closeButton.Name = "closeButton";
            this.closeButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // CertificationConfirmationForm
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.confirmationNotificationTextBox);
            this.Name = "CertificationConfirmationForm";
            this.Text = "APEEEC Certification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox confirmationNotificationTextBox;
        private System.Windows.Forms.Button closeButton;
    }
}