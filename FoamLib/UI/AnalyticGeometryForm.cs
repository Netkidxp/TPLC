using FoamLib.Model.CfMesh;
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
    public partial class AnalyticGeometryForm : Form
    {
        public AnalyticGeometry Geometry
        {
            get
            {
                return agf_main.Geometry;
            }
            set
            {
                agf_main.Geometry = value;
            }
        }
        public AnalyticGeometryForm()
        {
            InitializeComponent();
        }
    }
}
