using FoamLib.IO;
using FoamLib.UI.Post.Display;
using FoamLib.Util;
using Fqh.CommonLib;
using Fqh.CommonLib.LocalLicenseComonent;
using Fqh.CommonLib.WebLicenseComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TplcModelLib.Ui;

namespace Test
{
    [LicenseProvider(typeof(LocalLicenseProvider))]
    public partial class Form1 : Form
    {
        private LocalLicense license = null;
        public Form1()
        {
            //this.mLicense = LicenseManager.Validate(typeof(Form1), this);

            license = (LocalLicense)LicenseManager.Validate(typeof(Form1), this);
            if (license.LicenseData == null)
            {
                //MessageBox.Show("license data is null");
                new RegistForm("No license file found").ShowDialog();
                return;
            }

            /*
            if (license.LicenseData.UsedCount >= license.LicenseData.MaxCount)
            {
                MessageBox.Show("use count error " + license.LicenseData.MaxCount + " " + license.LicenseData.UsedCount);

                new RegistForm().ShowDialog();
                return;
            }
            */

            if (license.LicenseData.HardwareId != LocalInformation.HardwareId)
            {
                //MessageBox.Show("hardwareid is error");
                new RegistForm("The license file is not for this device").ShowDialog();
                return;
            }
            DateTime now = LocalLicenseUtil.GetNetDateTime();
            if (now == new DateTime(0))
                now = DateTime.Now;
            if (now > license.LicenseData.ExpiredDate)
            {
                //MessageBox.Show("expired date error " + now + " " + license.LicenseData.ExpiredDate);
                new RegistForm("The license expires, and the final use period is " + license.LicenseData.ExpiredDate).ShowDialog();
                return;
            }

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = TplcWebLisenceUtils.Check("sss").ToString();
            WebLisenceUtils.RegistLocal(@"http://127.0.0.1:8000/regist/", typeof(Form1), 12);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //viewer3.RemoveAllUnits();
            WebLisenceUtils.CheckLocal(@"http://127.0.0.1:8000/check/", typeof(Form1));
        }

        private void OnUnitPicked(object sender, FoamLib.UI.Post.Controls.PickEventArge e)
        {
            MessageBox.Show(e.PickedUnit.Name);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebLisenceUtils.CheckLocal(@"http://127.0.0.1:8000/check/", typeof(Form2));

        }

        private void button4_Click(object sender, EventArgs e)
        {
            BoxUnit box = (BoxUnit)viewer3.FindUnit("box");
            box.Height = box.Length = box.Width = 0.3;
            box.Opacity = 0.1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LocalLicenseData lld = new LocalLicenseData();
            lld.ExpiredDate = DateTime.Now.AddDays(30);
            lld.HardwareId = LocalInformation.HardwareId;
            lld.MaxCount = 200;
            lld.StartDate = DateTime.Now;
            lld.UsedCount = 0;
            LocalLicenseUtil.WriteLicense(lld, @"f:\haha.lic");
            LocalLicenseData ldd_ = LocalLicenseUtil.ReadLicense(@"f:\haha.lic");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FoamDictionaryListFile f = new FoamDictionaryListFile(@"F:\lflow\testdata\r400_1.run\constant\polyMesh\boundary");
            f.Read();
        }
    }
}
