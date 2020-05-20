using FoamLib.Model;
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
    public class FieldChooseEditor : UITypeEditor
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
                        clb.Items.AddRange(Case.fieldNames);
                        clb.BorderStyle = BorderStyle.None;
                        List<string>  fields = value as List<string>;
                        for(int i=0;i<clb.Items.Count;i++)
                        {
                            clb.SetItemChecked(i, fields.Contains(clb.GetItemText(clb.Items[i])));
                        }
                        svc.DropDownControl(clb);
                        fields.Clear();
                        for (int i = 0; i < clb.Items.Count; i++)
                        {
                            if (clb.GetItemChecked(i))
                                fields.Add(clb.GetItemText(clb.Items[i]));
                        }
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
