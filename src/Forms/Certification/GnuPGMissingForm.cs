using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APEEEC_Outlook_AddIn.src.Forms.Certification
{
    public partial class GnuPGMissingForm : Form
    {
        public GnuPGMissingForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void getCurrentVersionButton_Click(object sender, EventArgs e)
        {
            string url = "https://www.gnupg.org/download/index.html";
            Process.Start(url);
        }
    }
}
