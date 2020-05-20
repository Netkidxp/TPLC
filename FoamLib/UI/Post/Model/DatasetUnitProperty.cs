using FoamLib.UI.Post.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.UI;
namespace FoamLib.UI.Post.Model
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DatasetUnitProperty
    {
        protected string name = "";
        protected string fieldName = "p";
        protected bool visible = true;
        protected double min = 0;
        protected double max = 1;
        protected double opacity = 1.0;
        protected DisplayMode mode = DisplayMode.Surface;

        [field: NonSerialized]
        public event EventHandler OnPropertyChanged;

        
        [NonSerialized]
        protected DatasetUnit unit = null;

        public DatasetUnitProperty() { }
        public DatasetUnitProperty(DatasetUnit unit)
        {
            this.unit = unit;
            this.name = unit.Name;
            this.fieldName = unit.FieldName;
            this.visible = unit.Visible;
            this.min = unit.ScalarRange[0];
            this.max = unit.ScalarRange[1];
            this.opacity = unit.Opacity;
            this.mode = unit.Mode;
        }
        [Browsable(false)]
        public string Name
        {
            get => name;
            set
            {
                name = value;
                if(unit!=null)
                    unit.Name = name;
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        [Browsable(false)]
        [TypeConverter(typeof(FieldNameStringConvertor))]
        public string FieldName
        {
            get => fieldName;
            set
            {
                fieldName = value;
                if (unit != null)
                    unit.FieldName = fieldName;
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        public virtual bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                if (unit != null)
                    unit.Visible = visible;
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        [ReadOnly(true)]
        public double Min
        {
            get => min; set
            {
                min = value;
                if (unit != null)
                    unit.ScalarRange = new double[2] {min, max};
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        [ReadOnly(true)]
        public double Max
        {
            get => max;
            set
            {
                max = value;
                if (unit != null)
                    unit.ScalarRange = new double[2] { min, max };
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        public double Opacity
        {
            get => opacity;
            set
            {
                opacity = value;
                if (unit != null)
                    unit.Opacity = opacity;
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        public DisplayMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                if (unit != null)
                    unit.Mode = mode;
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        protected void PropertyChanged()
        {
            OnPropertyChanged?.Invoke(this, new EventArgs());
        }
        [Browsable(false)]
        public virtual DatasetUnit Unit {
            get => unit;
            set
            {
                unit = value;
                unit.FieldName = this.fieldName;
                unit.Visible = this.visible;
                unit.ScalarRange = new double[] { this.min, this.max };
                unit.Opacity = this.opacity;
                unit.Mode = this.mode;
            }
        }
        
    }
}
