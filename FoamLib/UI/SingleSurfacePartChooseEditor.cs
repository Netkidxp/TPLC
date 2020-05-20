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
    public class SingleSurfacePartChooseEditor : UITypeEditor
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
                        CheckedListBox clb = new CheckedListBox();
                        clb.ItemCheck += (object sender, ItemCheckEventArgs e) =>
                        {
                            for (int i = 0; i < clb.Items.Count; i++)
                            {
                                if (i != e.Index)
                                {
                                    clb.SetItemCheckState(i, System.Windows.Forms.CheckState.Unchecked);
                                }

                            }
                        };
                        foreach(var s in GlobalGeometryObject.SurfacePartList)
                        {
                            clb.Items.Add(s);
                        }
                        clb.BorderStyle = BorderStyle.None;
                        string partName = value as string;
                        for(int i=0;i<clb.Items.Count;i++)
                        {
                            clb.SetItemChecked(i, clb.GetItemText(clb.Items[i]) == partName);
                        }
                        svc.DropDownControl(clb);
                        if(clb.SelectedItem!=null)
                        {
                            partName = clb.GetItemText(clb.SelectedItem);
                        }
                        return partName;
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
