namespace APEEEC_Outlook_AddIn.src.Forms.KeyExchange
{
    partial class KeyExchangeStartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyExchangeStartForm));
            this.keyExchangeNotificationTextBox = new System.Windows.Forms.TextBox();
            this.continueButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // keyExchangeNotificationTextBox
            // 
            this.keyExchangeNotificationTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.keyExchangeNotificationTextBox.Location = new System.Drawing.Point(12, 12);
            this.keyExchangeNotificationTextBox.Multiline = true;
            this.keyExchangeNotificationTextBox.Name = "keyExchangeNotificationTextBox";
            this.keyExchangeNotificationTextBox.Size = new System.Drawing.Size(260, 199);
            this.keyExchangeNotificationTextBox.TabIndex = 1;
            this.keyExchangeNotificationTextBox.Text = resources.GetString("keyExchangeNotificationTextBox.Text");
            // 
            // continueButton
            // 
            this.continueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continueButton.Location = new System.Drawing.Point(197, 227);
            this.continueButton.Name = "continueButton";
            this.continueButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 2;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(116, 227);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // KeyExchangeStartForm
            // 
            this.AcceptButton = this.continueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.keyExchangeNotificationTextBox);
            this.Name = "KeyExchangeStartForm";
            this.Text = "Key Exchange";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox keyExchangeNotificationTextBox;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button cancelButton;
    }
}