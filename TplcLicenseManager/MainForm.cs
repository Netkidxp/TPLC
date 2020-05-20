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

namespace TplcLicenseManager
{
    public partial class MainForm : Form
    {
        string licenseServer = @"http://www.netkidxp.cn:8000";
        List<Type> componentList = new List<Type>
        {
            typeof(TplcSolver.MainWindow),
            typeof(TplcPost.MainWindow)
        };
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void buCheck_Click(object sender, EventArgs e)
        {
            lvMain.Items.Clear();
            foreach (Type t in componentList)
            {
                var re = WebLisenceUtils.Check(licenseServer + "/check/", tbHardwareId.Text, t.GUID.ToString());
                if(re.Result.IsOK)
                {
                    var item = lvMain.Items.Add(t.AssemblyQualifiedName);
                    item.SubItems.Add(t.GUID.ToString());
                    item.SubItems.Add(re.Data.state.ToString());
                    item.SubItems.Add(re.Data.start_date.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(re.Data.day_count.ToString());
                    DateTime ed = re.Data.start_date.AddDays(re.Data.day_count);
                    item.SubItems.Add(ed.ToString("yyyy-MM-dd"));
                }
                else
                {
                    MessageBox.Show("Access license web serveice error: " + re.Result.Message);
                }
            }
        }

        private void OnContextMenuOpening(object sender, CancelEventArgs e)
        {
            //setAllComponentDayCountToolStripMenuItem.Visible = setComponentDayCountToolStripMenuItem.Visible = (lvMain.SelectedItems.Count > 0);
        }

        private void setAllComponentDayCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayCountForm f = new DayCountForm();
            f.ShowDialog();
            foreach (ListViewItem i in lvMain.Items)
            {
                string guid = i.SubItems[1].Text;
                string componentName = i.SubItems[0].Text;
                int dayCount = f.DayCount;
                var re = WebLisenceUtils.Regist(licenseServer + "/regist/", tbHardwareId.Text, componentName, guid, dayCount);
                if (re.Result.IsOK)
                {
                    MessageBox.Show("License updated");
                }
                else
                {
                    MessageBox.Show("Access license web serveice error: " + re.Result.Message);
                }
            }
            buCheck_Click(this, new EventArgs());
        }

        private void setComponentDayCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayCountForm f = new DayCountForm();
            f.ShowDialog();
            foreach (ListViewItem i in lvMain.SelectedItems)
            {
                string guid = i.SubItems["Guid"].Text;
                string componentName = i.Text;
                int dayCount = f.DayCount;
                var re = WebLisenceUtils.Regist(licenseServer + "/regist/", tbHardwareId.Text, componentName, guid, dayCount);
                if(re.Result.IsOK)
                {
                    MessageBox.Show("License updated");
                }
                else
                {
                    MessageBox.Show("Access license web serveice error: " + re.Result.Message);
                }
            }
            buCheck_Click(this, new EventArgs());
        }
    }
}
