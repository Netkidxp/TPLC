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
    public class PatchSelectEditor : UITypeEditor
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
                    if (value is string)
                    {
                        string patchName = (string)value;
                        ListBox lb = new ListBox();
                        foreach(string pn in GlobalModelObject.patchNames)
                        {
                            lb.Items.Add(pn);
                            if (pn == patchName)
                                lb.SelectedValue = pn;
                        }
                        svc.DropDownControl(lb);
                        if (lb.SelectedItem != null)
                            return lb.SelectedValue.ToString();
                        else
                            return value;
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
