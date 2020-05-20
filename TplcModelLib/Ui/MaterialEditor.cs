using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using Tplc.Model;
using FoamLib.UI;
using System.Windows.Forms;

namespace Tplc.UI
{
    class MaterialEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (svc != null)
                {
                    if (value is MaterialProperty)
                    {
                        MaterialProperty mp = (MaterialProperty)value;
                        MaterialEditControl c = new MaterialEditControl();
                        c.Material = new MaterialEditControl.ConstMaterial("case_material", mp.Density, mp.Viscosity);
                        c.BorderStyle = BorderStyle.None;
                        svc.DropDownControl(c);
                        return new MaterialProperty(c.Material.density, c.Material.viscosity);
                    }
                }
            }
            catch (Exception ex)
            {
                return value;
            }
            return value;
        }

    }
}
