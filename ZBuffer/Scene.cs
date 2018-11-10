using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZBuffer.Tools;
using ZBuffer.Shapes;
using System.Windows.Media.Media3D;
using System.Runtime.Serialization;
using ZBuffer.Affine_Transformation;

namespace ZBuffer
{
    //TODO Think about changing DataSerializer to XmlSerializer
    [DataContract]
    [KnownType(typeof(MFacet))]
    public class Scene
    {
        public int Height { get; set; }
        public int Width { get; set; }

        //TODO Make public?
        [DataMember]
        private List<MCommonPrimitive> Shapes { get; set; }
        [DataMember]
        public List<MCommonPrimitive> SelectedShapes { get; set; }
        private Tools.ZBuffer Buffer { get; set; }
        public Tools.Camera CurrentCamera { get; set; }


        public Scene(int width, int heigth, int z)
        {
            Width = width;
            Height = heigth;

            Buffer = new Tools.ZBuffer(width * heigth);

            CurrentCamera = new Tools.OrthographicCamera(-160, 160, -90, 90, -50, 50);
            //CurrentCamera = new Tools.PerspectiveCamera((float)Math.PI / 4, 16 / 9, 10, -10);

            Shapes = new List<MCommonPrimitive>();

            //TODO Rework
            SelectedShapes = Shapes;
        }

        public WriteableBitmap Render()
        {
            new ShapeEditor().GetTransformedShapes(Shapes, CurrentCamera);

            List<MPoint> allPoints = GetAllPoints();

            return new Painter().DrawSceneByPoints(this.Width, this.Height, allPoints);
        }

        public void AddShape(MCommonPrimitive shape)
        {
            Shapes.Add(shape);
        }

        //TODO Rework
        public void RotateSelected(double angle)
        {
            //TODO remove this
            SelectedShapes = Shapes;

            var editor = new ShapeEditor();

            for (int i = 0; i < SelectedShapes.Count; ++i)
                editor.Rotate(SelectedShapes[i], 10, 0, 0);
        }

        private List<MPoint> GetAllPoints()
        {
            var allPoints = new List<MPoint>();

            foreach (MShape shape in Shapes)
            {
                allPoints.AddRange(shape.GetAllPoints());
            }
            //foreach(MShape shape in Shapes)
            //{
            //    foreach (MPoint point in shape.GetPoints())
            //        allPoints.Add(point);
            //}


            return allPoints;
        }

        public void Clear()
        {
            this.Shapes.Clear();
        }
    }
}
