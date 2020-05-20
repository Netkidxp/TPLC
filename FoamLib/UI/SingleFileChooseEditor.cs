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
    public class SingleFileChooseEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.stl|*.stl|*.ftr|*.ftr";
            if (dlg.ShowDialog() == DialogResult.OK)
                return dlg.FileName;
            else
                return value;
        }
    }

}
