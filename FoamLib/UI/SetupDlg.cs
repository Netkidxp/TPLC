using FoamLib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoamLib.UI
{
    public partial class SetupDlg : Form
    {
        public SetupDlg(ref GlobalConfig cfg)
        {
            InitializeComponent();
            this.pg_main.SelectedObject = cfg;
        }
    }
}
