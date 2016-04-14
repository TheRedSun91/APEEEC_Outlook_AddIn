namespace APEEEC_Outlook_AddIn.src.Forms.Certification
{
    partial class GnuPGMissingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GnuPGMissingForm));
            this.confirmationNotificationTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.getCurrentVersionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // confirmationNotificationTextBox
            // 
            this.confirmationNotificationTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.confirmationNotificationTextBox.Location = new System.Drawing.Point(12, 12);
            this.confirmationNotificationTextBox.Multiline = true;
            this.confirmationNotificationTextBox.Name = "confirmationNotificationTextBox";
            this.confirmationNotificationTextBox.Size = new System.Drawing.Size(260, 199);
            this.confirmationNotificationTextBox.TabIndex = 2;
            this.confirmationNotificationTextBox.Text = resources.GetString("confirmationNotificationTextBox.Text");
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(197, 227);
            this.closeButton.Name = "closeButton";
            this.closeButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // getCurrentVersionButton
            // 
            this.getCurrentVersionButton.Location = new System.Drawing.Point(12, 227);
            this.getCurrentVersionButton.Name = "getCurrentVersionButton";
            this.getCurrentVersionButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.getCurrentVersionButton.Size = new System.Drawing.Size(179, 23);
            this.getCurrentVersionButton.TabIndex = 4;
            this.getCurrentVersionButton.Text = "Get Current GnuPG Version";
            this.getCurrentVersionButton.UseVisualStyleBackColor = true;
            this.getCurrentVersionButton.Click += new System.EventHandler(this.getCurrentVersionButton_Click);
            // 
            // GnuPGMissingForm
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.getCurrentVersionButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.confirmationNotificationTextBox);
            this.Name = "GnuPGMissingForm";
            this.Text = "GnuPGMissingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox confirmationNotificationTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button getCurrentVersionButton;
    }
}