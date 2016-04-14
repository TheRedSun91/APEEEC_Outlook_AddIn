using System;
using System.Windows.Forms;

namespace APEEEC_Outlook_AddIn.src.Forms.Certification
{
    public partial class CertificationStartForm : Form
    {
        public CertificationStartForm()
        {
            InitializeComponent();
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            this.SetVisibleCore(false);
            CertificationUserInputForm userInputForm = new CertificationUserInputForm();
            userInputForm.Show();
            this.Dispose();
        }
    }
}
