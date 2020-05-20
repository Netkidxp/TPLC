using FoamLib.Model.CfMesh;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using static FoamLib.Model.CfMesh.CfMeshDict;

namespace FoamLib.UI
{
    public class RefineObjectListEdit : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (svc != null)
                {
                    if (value is List<ObjectRefinementSubDict>)
                    {
                        RefineObjectListEditControl f = new RefineObjectListEditControl(value as List<ObjectRefinementSubDict>);
                        f.BorderStyle = System.Windows.Forms.BorderStyle.None;
                        svc.DropDownControl(f);
                        List<ObjectRefinementSubDict> nl = new List<ObjectRefinementSubDict>();
                        nl.AddRange(value as List<ObjectRefinementSubDict>);
                        return nl;
                    }
                }
            }
            catch (Exception ex)
            {
                return value;
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }
}
