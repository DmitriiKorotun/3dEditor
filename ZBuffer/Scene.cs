using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EmuEngine.Tools;
using EmuEngine.Shapes;
using System.Windows.Media.Media3D;
using System.Runtime.Serialization;
using EmuEngine.Affine_Transformation;
using System.Drawing;


namespace EmuEngine
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
        private List<MShape> Shapes { get; set; }
        [DataMember]
        public List<MShape> SelectedShapes { get; set; }
        private Tools.ZBuffer Buffer { get; set; }
        public StageManager StageManager { get; set; }


        public Scene(int width, int heigth, int z)
        {
            Width = width;
            Height = heigth;

            StageManager = new StageManager();

            //CurrentCamera = new Tools.OrthographicCamera(-160, 160, -90, 90, -50, 50);

            Shapes = new List<MShape>();

            //TODO Rework
            SelectedShapes = Shapes;
        }

        public void SwitchCameraType()
        {
            //CurrentCamera
        }

        public WriteableBitmap Render()
        {
            new ShapeEditor().TransformShapes(Shapes, StageManager.CurrentCamera);

            List<MPoint> allPoints = GetAllPoints();

            return new Painter().DrawSceneByPoints(this.Width, this.Height, allPoints);
        }

        public Bitmap RenderBitmap()
        {
            new ShapeEditor().TransformShapes(Shapes, StageManager.CurrentCamera);

            List<MPoint> allPoints = GetAllPoints();

            return new Painter().DrawSceneByPointsBitmap(this.Width, this.Height, allPoints);
        }

        public void AddShape(MShape shape)
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
