using FoamLib.Model;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Display
{
    public class BoxUnit : GeometryUnit
    {
        vtkPolyData polyData = vtkPolyData.New();
        vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
        Vector center = new Vector(0, 0, 0);
        double length = 1;
        double width = 1;
        double height = 1;
        public BoxUnit(string name) : base(name)
        {
            UpdateBox();
            mapper.SetInputData(polyData);
            actor.SetMapper(mapper);
            Center.OnPropertyChanged += OnCenterChanged;
        }

        public Vector Center
        {
            get => center;
            set
            {
                center = value;
                center.OnPropertyChanged += OnCenterChanged;
                UpdateBox();
            }
        }
        public double Length
        {
            get => length;
            set
            {
                length = value;
                UpdateBox();
            }
        }
        public double Width
        {
            get => width;
            set
            {
                width = value;
                UpdateBox();
            }
        }
        public double Height
        {
            get => height;
            set
            {
                height = value;
                UpdateBox();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            polyData.Dispose();
            mapper.Dispose();
        }

        private void OnCenterChanged(object sender, EventArgs e)
        {
            UpdateBox();
        }
        
        private void UpdateBox()
        {
            double[,] points = new double[8, 3]
            {
                {Center.V1 - Length/2, Center.V2 - Width/2, Center.V3  - Height/2 },
                {Center.V1 + Length/2, Center.V2 - Width/2, Center.V3  - Height/2 },
                {Center.V1 + Length/2, Center.V2 + Width/2, Center.V3  - Height/2 },
                {Center.V1 - Length/2, Center.V2 + Width/2, Center.V3  - Height/2 },
                {Center.V1 - Length/2, Center.V2 - Width/2, Center.V3  + Height/2 },
                {Center.V1 + Length/2, Center.V2 - Width/2, Center.V3  + Height/2 },
                {Center.V1 + Length/2, Center.V2 + Width/2, Center.V3  + Height/2 },
                {Center.V1 - Length/2, Center.V2 + Width/2, Center.V3  + Height/2 }
            };
            int[,] cells = new int[6, 4]
            {
                { 0, 1, 2, 3 },
                { 4, 5, 6, 7 },
                { 0, 1, 5, 4 },
                { 1, 2, 6, 5 },
                { 2, 3, 7, 6 },
                { 3, 0, 4, 7 }
            };
            vtkPoints pointArray = vtkPoints.New();
            for(int i = 0; i<8; i++)
            {
                pointArray.InsertNextPoint(points[i, 0], points[i, 1], points[i, 2]);
            }
            vtkCellArray cellArray = vtkCellArray.New();
            for(int i = 0; i<6;i++)
            {
                vtkIdList il = vtkIdList.New();
                for(int j=0;j<4;j++)
                {
                    il.InsertNextId(cells[i, j]);
                }
                cellArray.InsertNextCell(il);
            }
            polyData.SetPoints(pointArray);
            polyData.SetPolys(cellArray);
            Render();
        }
    }
}
