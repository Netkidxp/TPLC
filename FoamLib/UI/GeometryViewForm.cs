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
    public partial class GeometryViewForm : Form
    {
        string fileName;
        public GeometryViewForm(string fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
            geometryViewer1.SetStlFileName(fileName);
        }
    }
}
