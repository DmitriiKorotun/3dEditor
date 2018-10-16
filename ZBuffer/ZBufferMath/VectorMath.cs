using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ZBuffer.ZBufferMath
{
    public class VectorMath
    {
        public List<Point3D> GetAllVectorPoints(Point3D point1, Point3D point2)
        {
            List<Point3D> points = new List<Point3D>() { point1, point2 };

            CalculateLinePoints(points, point1, point2);

            return points;
        }

        private void CalculateLinePoints(List<Point3D> points, Point3D point1, Point3D point2)
        {
            //TODO Add z
            if (Math.Abs(point2.X - point1.X) <= 1 &&
                Math.Abs(point2.Y - point1.Y) <= 1 && Math.Abs(point2.Z - point1.Z) <= 1)
                return;

            Point3D newPoint = GetNextPoint(point1, point2);

            points.Add(newPoint);

            CalculateLinePoints(points, point1, newPoint);
            CalculateLinePoints(points, newPoint, point2);
        }

        private Point3D GetNextPoint(Point3D point1, Point3D point2)
        {
            var x = Math.Round((point1.X + point2.X) / 2);
            var y = Math.Round((point1.Y + point2.Y) / 2);
            var z = Math.Round((point1.Z + point2.Z) / 2);

            return new Point3D(x, y, z);
        }
    }
}
