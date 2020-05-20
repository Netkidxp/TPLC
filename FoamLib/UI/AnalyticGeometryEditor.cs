using FoamLib.Model;
using FoamLib.Model.CfMesh;
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FoamLib.UI
{
    public class AnalyticGeometryEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            AnalyticGeometryForm agf = new AnalyticGeometryForm();
            agf.Geometry = (AnalyticGeometry)value;
            agf.ShowDialog();
            return agf.Geometry;
        }
    }

}
