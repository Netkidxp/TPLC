using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoamLib.IO;

namespace FoamLib.UI
{
    public partial class CheckableParameterTextBox : UserControl
    {
        public String ShowName
        {
            get => cb_name.Text;
            set => cb_name.Text = value;
        }
        public bool Selected
        {
            get => cb_name.Checked;
            set
            {
                cb_name.Checked = value;
                tb_value.Enabled = value;
            }
        }
        public FoamDictionary BoundDictionary { get; set; }

        public string BoundName { get; set; }

        public CheckableParameterTextBox()
        {
            InitializeComponent();
        }

        public CheckableParameterTextBox(string showName, FoamDictionary boundDictionary, string boundName, string valueText)
        {
            ShowName = showName;
            BoundDictionary = boundDictionary;
            BoundName = boundName;
            tb_value.Text = valueText;
        }

        public void Bound(FoamDictionary d, string name)
        {
            BoundDictionary = d;
            BoundName = name;
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            if(Selected && BoundDictionary!=null && BoundName!=null && tb_value.Text != "")
            {
                    BoundDictionary.SetChild(BoundName, tb_value.Text);
            }
        }
    }
}
