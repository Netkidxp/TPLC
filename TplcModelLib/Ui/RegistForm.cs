using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TplcModelLib.Ui
{
    public partial class RegistForm : Form
    {
        public RegistForm(string msg)
        {
            InitializeComponent();
            label2.Text = msg;
        }
    }
}
