using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.Model;
using Tplc.Model.Boundary;

namespace Tplc.UI
{
    public class TplcBoundaryEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            TplcBoundaryEditForm form = new TplcBoundaryEditForm();
            form.PathchBoundarys = (List<BoundaryBase>)value;
            form.PatchNames = GlobalModelObject.patchNames;
            form.ShowDialog();
            List<BoundaryBase> ret = new List<BoundaryBase>();
            ret.AddRange(form.PathchBoundarys);
            return ret;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
