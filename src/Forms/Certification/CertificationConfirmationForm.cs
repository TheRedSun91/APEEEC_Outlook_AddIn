using System;
using System.Windows.Forms;

namespace APEEEC_Outlook_AddIn.src.Forms.Certification
{
    public partial class CertificationConfirmationForm : Form
    {
        public CertificationConfirmationForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.SetVisibleCore(false);
            this.Dispose();
        }
    }
}
