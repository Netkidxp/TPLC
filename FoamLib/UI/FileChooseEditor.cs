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
    public class FileChooseEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            List<string> fileNames = new List<string>();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "*.stl|*.stl";
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                foreach(var fn in dlg.FileNames)
                {
                    fileNames.Add(fn);
                }
            }
            return fileNames;
        }
    }

}
