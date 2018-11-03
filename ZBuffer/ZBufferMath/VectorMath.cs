using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using ZBuffer.Shapes;

namespace ZBuffer.ZBufferMath
{
    public class VectorMath
    {
        public List<MPoint> GetAllVectorPoints(MPoint point1, MPoint point2)
        {
            List<MPoint> points = new List<MPoint>() { point1, point2 };

            CalculateLinePoints(points, point1, point2);

            return points;
        }

        private void CalculateLinePoints(List<MPoint> points, MPoint point1, MPoint point2)
        {
            //TODO Add z
            if (Math.Abs(point2.X - point1.X) <= 1 &&
                Math.Abs(point2.Y - point1.Y) <= 1 && Math.Abs(point2.Z - point1.Z) <= 1)
                return;

            MPoint newPoint = GetNextPoint(point1, point2);

            points.Add(newPoint);

            CalculateLinePoints(points, point1, newPoint);
            CalculateLinePoints(points, newPoint, point2);
        }

        private MPoint GetNextPoint(MPoint point1, MPoint point2)
        {
            var x = (float)Math.Round((point1.X + point2.X) / 2);
            var y = (float)Math.Round((point1.Y + point2.Y) / 2);
            var z = (float)Math.Round((point1.Z + point2.Z) / 2);

            return new MPoint(x, y, z);
        }

        public MPoint MovePointTowards(MPoint a, MPoint b, float distance)
        {
            var vector = new MPoint(b.X - a.X, b.Y - a.Y, b.Z - a.Z);

            var length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

            var unitVector = new MPoint(vector.X / length, vector.Y / length, vector.Z / length);

            return new MPoint(a.X + unitVector.X * distance, a.Y + unitVector.Y * distance, a.Z + unitVector.Z * distance);
        }
    }
}
