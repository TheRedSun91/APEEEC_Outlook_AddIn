using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APEEEC_Outlook_AddIn.src.Forms.KeyExchange
{
    public partial class ImportKeyFailedForm : Form
    {
        public ImportKeyFailedForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
