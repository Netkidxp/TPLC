using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FoamLib.UI
{
    public partial class MaterialEditControl : UserControl
    {
        public class ConstMaterial
        {
            public string name = "new_material";
            public float density = 1.293f;
            public float viscosity = 1.79e-5f;

            public ConstMaterial()
            {
            }

            public ConstMaterial(string name, float density, float viscosity)
            {
                this.name = name;
                this.density = density;
                this.viscosity = viscosity;
            }
        }

        List<ConstMaterial> constMaterialList = new List<ConstMaterial>();
        const string szTpConst = "tp_const";
        const string szTpIdealGas = "tp_idealgas";
        string szConstPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "materials", "const");
        string szIdealgasPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "materials", "idealgas");
        public MaterialEditControl()
        {
            InitializeComponent();
            InitlizeData();
        }

        public ConstMaterial Material
        {
            get
            {
                ConstMaterial m = new MaterialEditControl.ConstMaterial();
                m.name = cb_const_name.Text;
                m.density = float.Parse(tb_const_density.Text);
                m.viscosity = float.Parse(tb_const_viscosity.Text);
                return m;
            }
            set
            {
                cb_const_name.Text = value.name;
                tb_const_density.Text = value.density.ToString();
                tb_const_viscosity.Text = value.viscosity.ToString();
            }
        }

        public void InitlizeData()
        {
            constMaterialList.Clear();
            cb_const_name.Items.Clear();
            if (!Directory.Exists(szConstPath))
                return;
            DirectoryInfo di = new DirectoryInfo(szConstPath);
            FileInfo[] fis = di.GetFiles();
            foreach (FileInfo fi in fis)
            {
                if (fi.Name == "." || fi.Name == ".." || fi.Extension !=".mat")
                    continue;
                ConstMaterial m = ReadConst(fi.FullName);
                constMaterialList.Add(m);
                cb_const_name.Items.Add(m.name);
            }
            
        }
        private ConstMaterial ReadConst(string fileName)
        {
            try
            {
                ConstMaterial m = new ConstMaterial();
                StreamReader sr = new StreamReader(fileName, new UTF8Encoding(false));
                m.name = sr.ReadLine();
                m.density = float.Parse(sr.ReadLine());
                m.viscosity = float.Parse(sr.ReadLine());
                sr.Close();
                return m;
            }
            catch(Exception e)
            {
                MessageBox.Show("Error to read material: " + fileName + "\r\nMessage: "+e.Message);
                return null;
            }

        }
        private bool WriteConst(ConstMaterial mat)
        {
            try
            {
                string fileName = Path.Combine(szConstPath, mat.name + ".mat");
                StreamWriter sw = new StreamWriter(fileName, false, new UTF8Encoding(false));
                sw.WriteLine(mat.name);
                sw.WriteLine(mat.density.ToString());
                sw.WriteLine(mat.viscosity.ToString());
                sw.Close();
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show("Error to write material: " + mat.name + "\r\nMessage: " + e.Message);
                return false;
            }
        }
        private void bu_add_Click(object sender, EventArgs e)
        {
            ConstMaterial m = new ConstMaterial();
            m.name = "material" + constMaterialList.Count + 1;
            Material = m;
        }

        private void bu_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = Path.Combine(szConstPath, cb_const_name.Text + ".mat");
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error to delete material\r\nMessage: " + ex.Message);
            }
            InitlizeData();
        }

        private void bu_const_copy_Click(object sender, EventArgs e)
        {
            ConstMaterial m = Material;
            m.name += "_copy";
            Material = m;
        }

        private void bu_const_save_Click(object sender, EventArgs e)
        {
            WriteConst(Material);
            InitlizeData();
        }

        private void OnCb_ConstName_SelectIndexChanged(object sender, EventArgs e)
        {
            if (constMaterialList.Count > cb_const_name.SelectedIndex && cb_const_name.SelectedIndex>=0)
                Material = constMaterialList[cb_const_name.SelectedIndex];
        }
    }
}
