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
    public class SurfacePartChooseEditor : UITypeEditor
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
                    if (value is List<string>)
                    {
                        CheckedListBox clb = new CheckedListBox();
                        foreach(var s in GlobalGeometryObject.SurfacePartList)
                        {
                            clb.Items.Add(s);
                        }
                        clb.BorderStyle = BorderStyle.None;
                        List<string>  partNames = value as List<string>;
                        for(int i=0;i<clb.Items.Count;i++)
                        {
                            clb.SetItemChecked(i, partNames.Contains(clb.GetItemText(clb.Items[i])));
                        }
                        svc.DropDownControl(clb);
                        partNames.Clear();
                        for (int i = 0; i < clb.Items.Count; i++)
                        {
                            if (clb.GetItemChecked(i))
                                partNames.Add(clb.GetItemText(clb.Items[i]));
                        }
                        return partNames;
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
