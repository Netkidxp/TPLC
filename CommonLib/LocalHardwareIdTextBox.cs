using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fqh.CommonLib
{
    public partial class LocalHardwareIdTextBox : UserControl
    {
        public LocalHardwareIdTextBox()
        {
            InitializeComponent();
            tbMain.Text = LocalInformation.HardwareId;
        }
    }
}
