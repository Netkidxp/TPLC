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
    public partial class DayCountForm : Form
    {
        public int DayCount
        {
            get
            {
                return int.Parse(tbMain.Text);
            }
            set
            {
                tbMain.Text = value.ToString();
            }
        }
        public DayCountForm()
        {
            InitializeComponent();
            tbMain.Focus();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Close();
        }
    }
}
