using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace FoamLib.UI.Post.Display
{

    public class DatasetUnit : Unit
    {
        
        protected vtkDataSetMapper mapper = vtkDataSetMapper.New();
        protected vtkDataSet dataset = null;
        protected string fieldName = "p";
        protected double[] scalarRange = new double[] { 0,1};
        
        protected vtkLookupTable lookupTable = vtkLookupTable.New();
        private vtkScalarBarActor scalarBarActor = vtkScalarBarActor.New();
        
        protected DatasetUnit()
        {
            lookupTable = vtkLookupTable.New();
            lookupTable.SetNumberOfColors(100);
            lookupTable.SetHueRange(0.67, 0.0);
            lookupTable.Build();
            scalarBarActor.SetOrientationToHorizontal();
            scalarBarActor.SetLookupTable(lookupTable);
            scalarBarActor.SetTitle(fieldName); 
            scalarBarActor.GetAnnotationTextProperty().SetColor(0, 0, 0);
            scalarBarActor.GetTitleTextProperty().SetColor(0, 0, 0);
            scalarBarActor.GetLabelTextProperty().SetColor(0, 0, 0);
            scalarBarActor.GetAnnotationTextProperty().SetFontSize(20);
            scalarBarActor.GetTitleTextProperty().SetFontSize(20);
            scalarBarActor.GetLabelTextProperty().SetFontSize(20);
            scalarBarActor.GetAnnotationTextProperty().SetFontFamilyToArial();
            scalarBarActor.GetTitleTextProperty().SetFontFamilyToArial();
            scalarBarActor.GetLabelTextProperty().SetFontFamilyToArial();
            scalarBarActor.GetAnnotationTextProperty().ItalicOff();
            scalarBarActor.GetTitleTextProperty().ItalicOff();
            scalarBarActor.GetLabelTextProperty().ItalicOff();
            scalarBarActor.GetAnnotationTextProperty().BoldOff();
            scalarBarActor.GetTitleTextProperty().BoldOff();
            scalarBarActor.GetLabelTextProperty().BoldOff();
            scalarBarActor.SetUnconstrainedFontSize(true);
            scalarBarActor.SetBarRatio(0.15);
            scalarBarActor.SetNumberOfLabels(11);
            
        }
        public DatasetUnit(string name, vtkDataSet dataset) : this()
        {
            this.Name = name;
            this.Dataset = dataset;
        }
        
        public vtkMapper Mapper { get => mapper; }
        
        public virtual vtkDataSet Dataset
        {
            get => dataset;
            set
            {
                dataset = value;
                dataset.GetPointData().SetActiveScalars(fieldName);
                scalarRange = dataset.GetPointData().GetScalars().GetRange();
                mapper.SetInputData(dataset);
                mapper.ScalarVisibilityOn();
                mapper.SetScalarModeToUsePointFieldData();
                mapper.ColorByArrayComponent(fieldName, 0);
                mapper.SetScalarRange(scalarRange[0], scalarRange[1]);
                mapper.SetLookupTable(lookupTable);
                actor.SetMapper(mapper);
                Visible = true;
                if (mode == DisplayMode.Point)
                {
                    actor.GetProperty().SetEdgeVisibility(0);
                    actor.GetProperty().SetRepresentationToPoints();
                }
                else if (mode == DisplayMode.Wireframe)
                {
                    actor.GetProperty().SetEdgeVisibility(0);
                    actor.GetProperty().SetRepresentationToWireframe();
                }
                else if (mode == DisplayMode.Surface)
                {
                    actor.GetProperty().SetEdgeVisibility(0);
                    actor.GetProperty().SetRepresentationToSurface();
                }
                else if (mode == DisplayMode.SurfaceWithEdge)
                {
                    actor.GetProperty().SetEdgeVisibility(1);
                    actor.GetProperty().SetRepresentationToSurface();
                }
                Render();
            }
        }
        public string[] GetArrayNames()
        {
            int n = dataset.GetPointData().GetNumberOfArrays();
            string[] names = new string[n];
            for(int i=0;i<n;i++)
            {
                names[i] = dataset.GetPointData().GetArrayName(i);
            }
            return names;
        }
        public double[] GetInputScalarRange()
        {
            return dataset.GetPointData().GetScalars().GetRange();
        }
        public double[] GetInputScalarRange(string name)
        {
            return dataset.GetPointData().GetArray(name).GetRange();
        }
        public double GetAveragePointValue(string name, int compondIndex)
        {
            vtkDataArray array = dataset.GetPointData().GetArray(name);
            double v = 0;
            for(int i = 0; i < array.GetNumberOfTuples(); i++)
            {
                v += array.GetComponent(i, compondIndex);
            }
            return v / (double)array.GetNumberOfTuples();
        }
        public override bool Visible
        {
            get => base.Visible;
            set
            {
                ScalarBarActor.SetVisibility(value ? 1 : 0);
                base.Visible = value;
            }
            
        }
        public double[] ScalarRange
        {
            get
            {
                return scalarRange;
            }
            set
            {
                scalarRange = value;
                mapper.SetScalarRange(value[0], value[1]);
                Render();
            }
        }
        public string FieldName
        {
            get => fieldName;
            set
            {
                fieldName = value;
                dataset.GetPointData().SetActiveScalars(fieldName);
                scalarRange = GetInputScalarRange();
                mapper.ColorByArrayComponent(fieldName, 0);
                mapper.SetScalarRange(scalarRange[0], scalarRange[1]);
                scalarBarActor.SetTitle(fieldName);
                Render();
            }
        }
        public vtkScalarBarActor ScalarBarActor { get => scalarBarActor; }
        

        public override void Dispose()
        {
            base.Dispose();
            if (mapper != null)
                mapper.Dispose();
            if (lookupTable != null)
                lookupTable.Dispose();
        } 
        public void ResetScalarRange()
        {
            ScalarRange = dataset.GetPointData().GetScalars().GetRange();
        }
    }
}
