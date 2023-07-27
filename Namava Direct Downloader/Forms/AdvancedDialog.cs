using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Namava_Direct_Downloader
{
    public partial class AdvancedDialog : Form
    {
        MainForm parent;
        ResourceManager rm;
        string lang;
        public AdvancedDialog(MainForm parent)
        {
            this.parent = parent;
            this.rm = parent.rm;
            this.lang = parent.Language;

            InitializeComponent();
        }

        private void LoadLanguage(ResourceManager rm)
        {
            lblSaveFormat.Text = rm.GetString("desAdvSaveFormat");
            lblFormatInfo.Text = rm.GetString("desAdvFormatInfo");
            if (lang == "en")
            {
                lblFormatInfo.RightToLeft = RightToLeft.No;
            }
            else
            {
                lblFormatInfo.RightToLeft = RightToLeft.Yes;
            }
            btnFormatReset.Text = rm.GetString("desAdvFormatReset");
            lblPreviewT.Text = rm.GetString("desAdvPreviewT");
            this.Text = rm.GetString("desAdvTitle");
        }

        private void cboxSaveFormat_TextUpdate(object sender, EventArgs e)
        {
            parent.mySettings.strSettings["SaveFormat"] = cboxSaveFormat.Text;
            lblPreview.Text = Utils.CorrectPath(parent.GetVideoFileName());
        }

        private void btnFormatReset_Click(object sender, EventArgs e)
        {
            cboxSaveFormat.Text = "$N-S$SE$E-($Qp)";
            cboxSaveFormat_TextUpdate(sender, e);
        }

        private void AdvancedDialog_Load(object sender, EventArgs e)
        {
            LoadLanguage(rm);
            cboxSaveFormat.Text = parent.mySettings.strSettings["SaveFormat"];
            cboxSaveFormat_TextUpdate(sender, e);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            parent.Text = $"NDD V{Application.ProductVersion} " + parent.GetVideoFileName();
        }
    }
}
