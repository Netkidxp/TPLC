using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoamLib.Model.CfMesh;

namespace FoamLib.UI
{
    public partial class AnalyticGeometryControl : UserControl
    {
        private AnalyticGeometry geometry;
        public AnalyticGeometry Geometry
        {
            get
            {
                return geometry;
            }
            set
            {
                geometry = value;
                cb_main.SelectedValue = geometry.TypeName;
                pg_main.SelectedObject = geometry;
            }
        }
        public AnalyticGeometryControl()
        {
            InitializeComponent();
            Geometry = new Box();
            cb_main.Items.AddRange(AnalyticGeometry.TypeNames);
            cb_main.SelectedIndex = 0;
        }

        private void OnSelectedValueChanged(object sender, EventArgs e)
        {
            if(geometry.TypeName != (string)cb_main.SelectedItem)
            {
                switch(cb_main.SelectedItem)
                {
                    case "Box":
                        geometry = new Box();
                        pg_main.SelectedObject = geometry;
                        break;
                    case "Sphere":
                        geometry = new Sphere();
                        pg_main.SelectedObject = geometry;
                        break;
                    case "Cone":
                        geometry = new Cone();
                        pg_main.SelectedObject = geometry;
                        break;
                }
            }
        }
    }
}
