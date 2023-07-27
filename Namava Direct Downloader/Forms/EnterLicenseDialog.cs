using System;
using System.Windows.Forms;

namespace Namava_Direct_Downloader
{
    public partial class EnterLicenseDialog : Form
    {
        public string secret = "";
        public string license = "";

        public EnterLicenseDialog()
        {
            InitializeComponent();
        }

        private void EnterLicenseDialog_Load(object sender, EventArgs e)
        {

        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            license = tboxLicense.Text.ToUpper();
            this.DialogResult = DialogResult.OK;
        }

        private void EnterLicenseDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (license == "")
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
