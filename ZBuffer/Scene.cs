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

namespace ZBuffer
{
    public class Scene
    {
        //TODO Refactor
        public WriteableBitmap Bitmap { get; set; }

        private List<MShape> Shapes { get; set; }
        private Tools.ZBuffer Buffer { get; set; }
        private Tools.Camera CurrentCamera { get; set; }
        

        public Scene(int width, int heigth, int z)
        {
            Buffer = new Tools.ZBuffer(width * heigth);

            CurrentCamera = new Tools.Camera(width, heigth, z);

            Bitmap = new WriteableBitmap(width, heigth, 96, 96, PixelFormats.Bgra32, null);

            Shapes = new List<MShape>();
        }

        public WriteableBitmap Render()
        {
            List<Point3D> allPoints = GetAllPoints();

            var painter = new Painter();

            painter.DrawSceneByPoints(this, allPoints);

            return Bitmap;
        }

        public void AddShape(MShape shape)
        {
            Shapes.Add(shape);
        }

        private List<Point3D> GetAllPoints()
        {
            var allPoints = new List<Point3D>();

            foreach (MShape shape in Shapes)
            {
                allPoints.AddRange(shape.GetPoints());
            }
            //foreach(MShape shape in Shapes)
            //{
            //    foreach (Point3D point in shape.GetPoints())
            //        allPoints.Add(point);
            //}


            return allPoints;
        }
    }
}
