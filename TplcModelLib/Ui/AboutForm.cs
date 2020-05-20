using Fqh.CommonLib.WebLicenseComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TplcModelLib.Ui
{
    public partial class AboutForm : Form
    {
        public string ApplicationName
        {
            set
            {
                lbAppicationName.Text = value;
            }
        }
        public string Version
        {
            set
            {
                lbVersion.Text = value;
            }
        }
        public string Copyright
        {
            set
            {
                lbCopyright.Text = value;
            }
        }
        public DateTime ExpiredDate
        {
            set
            {
                lbLicenseUntil.Text = value.ToString("yyyy-MM-dd");
            }
        }
        private void LoadComponentLicenses()
        {
            tcLicenses.TabPages.Clear();
            string dirName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "componentlicenses");
            if (!Directory.Exists(dirName))
                return;
            DirectoryInfo di = new DirectoryInfo(dirName);
            FileInfo[] fis = di.GetFiles();
            foreach(FileInfo fi in fis)
            {
                AddComponentLicense(fi.FullName);
            }
        }
        private void AddComponentLicense(string licFile)
        {
            if (!File.Exists(licFile))
                return;
            TabPage p = new TabPage(Path.GetFileNameWithoutExtension(licFile));
            TextBox t = new TextBox();
            t.Multiline = true;
            t.ReadOnly = true;
            t.ScrollBars = ScrollBars.Both;
            t.WordWrap = true;
            t.Dock = DockStyle.Fill;
            p.Controls.Add(t);
            try
            {
                StreamReader sr = new StreamReader(licFile);
                t.Text = sr.ReadToEnd();
                sr.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Read component license file error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tcLicenses.TabPages.Add(p);
        }
        

        public AboutForm(string applicationName, string version, string copyright, DateTime expiredDate)
        {
            InitializeComponent();
            ApplicationName = applicationName;
            Version = version;
            Copyright = copyright;
            ExpiredDate = expiredDate;
            LoadComponentLicenses();
        }

        private void buOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
