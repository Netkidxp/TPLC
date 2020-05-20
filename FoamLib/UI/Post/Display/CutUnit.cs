using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.Model;
using Kitware.VTK;

namespace FoamLib.UI.Post.Display
{
    public class CutUnit : DatasetUnit
    {
        vtkPlane plane = vtkPlane.New();
        vtkCutter cutter = vtkCutter.New();
        DatasetUnit baseUnit = null;
        vtkRenderWindowInteractor interactor = null;
        bool showPlaneWidget = true;
        vtkImplicitPlaneWidget planeWidget = vtkImplicitPlaneWidget.New();
        public event vtkObject.vtkObjectEventHandler OnPlaneParameterChanged;
        public CutUnit(string name, DatasetUnit baseUnit, vtkRenderWindowInteractor interactor) : base()
        {
            this.Name = name;
            this.interactor = interactor;
            BaseUnit = baseUnit;
        }
        private void OnWidgetUpdate(vtkObject sender,vtkObjectEventArgs e)
        {
            double[] origin = planeWidget.GetOrigin();
            plane.SetOrigin(origin[0],origin[1],origin[2]);
            double[] normal = planeWidget.GetNormal();
            plane.SetNormal(normal[0], normal[1], normal[2]);
            Render();
            OnPlaneParameterChanged?.Invoke(sender, e);
        }

        public double[] Origin
        {
            get
            {
                return plane.GetOrigin();
            }
            set
            {
                plane.SetOrigin(value[0], value[1], value[2]);
                planeWidget.SetOrigin(value[0], value[1], value[2]);
                Render();
            }
        }

        public double[] Normal
        {
            get
            {
                return plane.GetNormal();
            }
            set
            {
                plane.SetNormal(value[0], value[1], value[2]);
                planeWidget.SetNormal(value[0], value[1], value[2]);
                Render();
            }
        }

        public DatasetUnit BaseUnit
        {
            get => baseUnit;
            set
            {
                baseUnit = value;
                if(baseUnit != null)
                {
                    double[] bounds = baseUnit.Dataset.GetBounds();
                    cutter.SetInputData(baseUnit.Dataset);
                    cutter.SetCutFunction(plane);
                    //Origin = new double[] { (bounds[0] + bounds[1]) / 2.0, (bounds[2] + bounds[3]) / 2.0, (bounds[4] + bounds[5]) / 2.0 };
                    Origin = baseUnit.Dataset.GetCenter();
                    Normal = new double[] { 1, 0, 0 };
                    UpdateCut();
                    //planeWidget.OriginTranslationOff();
                    planeWidget.OutlineTranslationOff();
                    planeWidget.SetInteractor(interactor);
                    planeWidget.SetPlaceFactor(1.0);
                    planeWidget.SetInputData(baseUnit.Dataset);
                    planeWidget.PlaceWidget();
                    //planeWidget.ScaleEnabledOn();
                    
                    //planeWidget.TubingOff();
                    planeWidget.EndInteractionEvt += OnWidgetUpdate;
                    if(showPlaneWidget)
                        planeWidget.On();
                    Dataset = cutter.GetOutput();
                }
            }
        }

        public bool ShowPlaneWidget
        {
            get => showPlaneWidget;
            set
            {
                showPlaneWidget = value;
                if (showPlaneWidget)
                    planeWidget.On();
                else
                    planeWidget.Off();
                Render();
            }
        }
        
        public void UpdateCut()
        {
            if(baseUnit!=null)
            {
                cutter.Update();
                Render();
            }
                
        }
        public override void Dispose()
        {
            base.Dispose();
            cutter.Dispose();
            plane.Dispose();
            planeWidget.Dispose();
        }

        public override void UnsetRender()
        {
            base.UnsetRender();
            planeWidget.Off();
        }
    }
}
