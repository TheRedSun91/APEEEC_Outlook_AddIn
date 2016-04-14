using APEEEC_Outlook_AddIn.src.Encryption;
using System;
using System.Drawing;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace APEEEC_Outlook_AddIn.src.Forms.Certification
{
    public partial class CertificationUserInputForm : Form
    {
        public CertificationUserInputForm()
        {
            InitializeComponent();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            //validate entries
            if (!EmailIsValid(emailAddressTextBox.Text))
            {
                emailAddressTextBox.BackColor = Color.Red;
            }
            else if (!Regex.IsMatch(firstNameTextBox.Text, @"^[a-zA-Z]+$"))
            {
                firstNameTextBox.BackColor = Color.Red;
            }
            else if (!Regex.IsMatch(lastNameTextBox.Text, @"^[a-zA-Z]+$"))
            {
                lastNameTextBox.BackColor = Color.Red;
            }
            else
            {
                //if entries valid, start "generate new key workflow"
                KeyManager keyManager = WorkflowHandler.APEEEC_Broker.GetSingletonBroker().GetKeyManager();
                String name = firstNameTextBox.Text + " " + lastNameTextBox.Text;
                keyManager.CreateNewKeyPair(name, new GpgApi.Email(emailAddressTextBox.Text));
                this.SetVisibleCore(false);
                CertificationConfirmationForm confirmationForm = new CertificationConfirmationForm();
                confirmationForm.Show();
                this.Dispose();
            }

        }

        private bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
