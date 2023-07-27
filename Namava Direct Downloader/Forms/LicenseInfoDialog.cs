using System;
using System.Globalization;
using System.Windows.Forms;

namespace Namava_Direct_Downloader
{
    public partial class LicenseInfoDialog : Form
    {
        private static readonly DateTime JanFirst1970 = new DateTime(1970, 1, 1);
        public double created, expire, days;
        public string license;
        public LicenseInfoDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblLicense.Text);
        }

        private void LicenseInfoDialog_Load(object sender, EventArgs e)
        {
            days = (expire - created) / 86400.0;
            DateTime Created = new DateTime((long)(created * 10000000) + JanFirst1970.Ticks);
            PersianCalendar Created_pc = new PersianCalendar();
            string Created_pc_string = String.Format("({0}/{1}/{2})", Created_pc.GetYear(Created), Created_pc.GetMonth(Created), Created_pc.GetDayOfMonth(Created));
            lblCreatedAtValue.Text = Created.ToString("MM/dd/yyyy") + " " + Created_pc_string;
            DateTime Expire = new DateTime((long)(expire * 10000000) + JanFirst1970.Ticks);
            PersianCalendar Expire_pc = new PersianCalendar();
            string Expire_pc_string = String.Format("({0}/{1}/{2})", Expire_pc.GetYear(Expire), Expire_pc.GetMonth(Expire), Expire_pc.GetDayOfMonth(Expire));
            lblExpirationDateValue.Text = Expire.ToString("MM/dd/yyyy") + " " + Expire_pc_string;
            lblDaysOfLicenseValue.Text = days.ToString() ;
            double timeleft = (Expire.Ticks - DateTime.Now.Ticks) / 10000000;
            lblDaysLeftValue.Text = ((int)(timeleft / 86400.0)).ToString();
            lblLicense.Text = license.ToUpper();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string linf = "";
            linf += lblLicenseInfo.Text + "\n";
            linf += "    " + lblLicense.Text + "    \n";
            linf += lblCreatedAt.Text + "  " + lblCreatedAtValue.Text + "\n";
            linf += lblExpirationDate.Text + "  " + lblExpirationDateValue.Text + "\n";
            linf += lblDaysOfLicense.Text + "  " + lblDaysOfLicenseValue.Text + "\n";
            linf += "    " + lblDaysLeft.Text + "  " + lblDaysLeftValue.Text + "\n";
            Clipboard.SetText(linf);
        }
    }
}
