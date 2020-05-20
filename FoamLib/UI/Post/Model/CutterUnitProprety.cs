using FoamLib.Model;
using FoamLib.UI.Post.Display;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Model
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CutterUnitProprety : DatasetUnitProperty
    {
        private Vector origin = new Vector(0, 0, 0);
        private Vector normal = new Vector(1, 0, 0);
        private bool showPlane = true;
        private bool widgetUpdateLock = false;

        [field: NonSerialized]
        public event EventHandler OnPlaneWidgetParameterChanged;

        public void OnChildPropertyChanged(object sender,EventArgs e)
        {
            CurrentCutUnit.Origin = origin.ToBlock();
            CurrentCutUnit.Normal = normal.ToBlock();
            PropertyChanged();
        }
        public void OnUnitWidgetPlaneParametersChanged(vtkObject sender, vtkObjectEventArgs e)
        {
            widgetUpdateLock = true;
            Origin = new Vector(CurrentCutUnit.Origin);
            Normal = new Vector(CurrentCutUnit.Normal);
            OnPlaneWidgetParameterChanged?.Invoke(this, new EventArgs());
            widgetUpdateLock = false;
        }
        public CutterUnitProprety(CutUnit unit) : base(unit)
        {
            this.origin = new Vector(unit.Origin);
            this.normal = new Vector(unit.Normal);
            this.origin.OnPropertyChanged += OnChildPropertyChanged;
            this.normal.OnPropertyChanged += OnChildPropertyChanged;
            (unit as CutUnit).OnPlaneParameterChanged += OnUnitWidgetPlaneParametersChanged;
        }
        private CutUnit CurrentCutUnit
        {
            get => this.unit as CutUnit;
        }

        public Vector Origin
        {
            get => origin;
            set
            {
                origin = value;
                origin.OnPropertyChanged += OnChildPropertyChanged;
                if(!widgetUpdateLock)
                {
                    CurrentCutUnit.Origin = origin.ToBlock();
                }
                PropertyChanged();
            }
        }

        public Vector Normal
        {
            get => normal;
            set
            {
                normal = value;
                normal.OnPropertyChanged += OnChildPropertyChanged;
                if (!widgetUpdateLock)
                {
                    CurrentCutUnit.Normal = normal.ToBlock();
                }
                PropertyChanged();
            }
        }

        public bool ShowPlane
        {
            get => showPlane;
            set
            {
                showPlane = value;
                CurrentCutUnit.ShowPlaneWidget = showPlane && visible;
                PropertyChanged();
            }
        }

        public override DatasetUnit Unit
        {
            get => unit;
            set
            {
                base.Unit = value;
                //this.origin = new Vector(CurrentCutUnit.Origin);
                //this.normal = new Vector(CurrentCutUnit.Normal);
                this.origin.OnPropertyChanged += OnChildPropertyChanged;
                this.normal.OnPropertyChanged += OnChildPropertyChanged;
                CurrentCutUnit.Origin = origin.ToBlock();
                CurrentCutUnit.Normal = normal.ToBlock();
                CurrentCutUnit.ShowPlaneWidget = this.showPlane;
                CurrentCutUnit.OnPlaneParameterChanged += OnUnitWidgetPlaneParametersChanged;
            }
        }

        public override bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;
                CurrentCutUnit.ShowPlaneWidget = showPlane && visible;
            }
        }
    }
}
