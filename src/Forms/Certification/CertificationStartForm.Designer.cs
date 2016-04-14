namespace APEEEC_Outlook_AddIn.src.Forms.Certification
{
    partial class CertificationStartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificationStartForm));
            this.certificationNotificationTextBox = new System.Windows.Forms.TextBox();
            this.continueButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // certificationNotificationTextBox
            // 
            this.certificationNotificationTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.certificationNotificationTextBox.Location = new System.Drawing.Point(12, 12);
            this.certificationNotificationTextBox.Multiline = true;
            this.certificationNotificationTextBox.Name = "certificationNotificationTextBox";
            this.certificationNotificationTextBox.Size = new System.Drawing.Size(260, 199);
            this.certificationNotificationTextBox.TabIndex = 0;
            this.certificationNotificationTextBox.Text = resources.GetString("certificationNotificationTextBox.Text");
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(197, 227);
            this.continueButton.Name = "continueButton";
            this.continueButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 1;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // CertificationStartForm
            // 
            this.AcceptButton = this.continueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.certificationNotificationTextBox);
            this.Name = "CertificationStartForm";
            this.Text = "APEEEC Certification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox certificationNotificationTextBox;
        private System.Windows.Forms.Button continueButton;
    }
}