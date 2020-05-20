using Fqh.CommonLib.LocalLicenseComonent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TplcLicenseManager
{
    public partial class LocalLicenseManager : Form
    {
        LocalLicenseData licData = new LocalLicenseData();
        public LocalLicenseManager()
        {
            InitializeComponent();
        }

        private void bu_new_Click(object sender, EventArgs e)
        {
            licData = new LocalLicenseData();
            licData.HardwareId = tbHardwareId.Text;
            pgMain.SelectedObject = licData;
        }

        private void bu_write_Click(object sender, EventArgs e)
        {
            if(licData!=null)
            {
                SaveFileDialog d = new SaveFileDialog();
                d.Filter = "*.lic|*.lic";
                if(d.ShowDialog() == DialogResult.OK)
                {
                    if(LocalLicenseUtil.WriteLicense(licData, d.FileName))
                    {
                        if(LocalLicenseUtil.SetUsedCount(licData.UsedCount))
                            MessageBox.Show("ok");
                        else
                            MessageBox.Show("set used count failed");
                    }
                    else
                    {
                        MessageBox.Show("fail");
                    }
                }
            }
        }

        private void bu_read_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.lic|*.lic";
            if (d.ShowDialog() == DialogResult.OK)
            {
                licData = LocalLicenseUtil.ReadLicense(d.FileName);
                if (licData != null)
                    pgMain.SelectedObject = licData;
                else
                    MessageBox.Show("fail");
            }
        }
    }
}
