namespace APEEEC_Outlook_AddIn.src.Forms.KeyExchange
{
    partial class UserDeniedCommunicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserDeniedCommunicationForm));
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
            this.certificationNotificationTextBox.TabIndex = 4;
            this.certificationNotificationTextBox.Text = resources.GetString("certificationNotificationTextBox.Text");
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(197, 227);
            this.closeButton.Name = "closeButton";
            this.closeButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // UserDeniedCommunicationForm
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.certificationNotificationTextBox);
            this.Name = "UserDeniedCommunicationForm";
            this.Text = "Negative Key Exchange Response";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox certificationNotificationTextBox;
        private System.Windows.Forms.Button closeButton;
    }
}