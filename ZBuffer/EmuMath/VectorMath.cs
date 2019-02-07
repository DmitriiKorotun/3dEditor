using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using EmuEngine.Shapes;

namespace EmuEngine.EmuMath
{
    public class VectorMath
    {
        public List<MPoint> GetAllVectorPoints(MPoint point1, MPoint point2)
        {
            ILineRasterizer lineRasterizer = new BresenhamLine();

            return lineRasterizer.GetLine(point1, point2);
            //List<MPoint> points = new List<MPoint>() { point1, point2 };

            //CalculateLinePoints(points, point1, point2);

            //return points;
        }

        private void CalculateLinePoints(List<MPoint> points, MPoint point1, MPoint point2)
        {
            //TODO Add z
            if (Math.Abs(point2.Current.X - point1.Current.X) <= 1 &&
                Math.Abs(point2.Current.Y - point1.Current.Y) <= 1 &&
                Math.Abs(point2.Current.Z - point1.Current.Z) <= 1)
                return;

            MPoint newPoint = GetNextPoint(point1, point2);

            points.Add(newPoint);

            CalculateLinePoints(points, point1, newPoint);
            CalculateLinePoints(points, newPoint, point2);
        }

        private MPoint GetNextPoint(MPoint point1, MPoint point2)
        {
            var x = (float)Math.Round((point1.Current.X + point2.Current.X) / 2);
            var y = (float)Math.Round((point1.Current.Y + point2.Current.Y) / 2);
            var z = (float)Math.Round((point1.Current.Z + point2.Current.Z) / 2);

            return new MPoint(x, y, z);
        }

        

        // Using Bresenham's line algorithm
        //public List<MPoint> GetAllVectorPoints(MPoint point1, MPoint point2)
        //{
        //    var points = new List<MPoint>();

        //    bool isAxisWasSwapped = false;

        //    if (point2.X == point1.X && point2.Y == point1.Y)
        //        return new List<MPoint>() { point1 };

        //    if (Math.Abs(point2.X - point1.X) < Math.Abs(point2.Y - point1.Y))
        //    {
        //        SwapPointAxis(point1);
        //        SwapPointAxis(point2);

        //        isAxisWasSwapped = true;
        //    }

        //    if (point2.X < point1.X)
        //    {
        //        var temp = point1;
        //        point1 = point2;
        //        point2 = temp;
        //    }

            
        //    var angleCoeff = (point2.Y - point1.Y) / (point2.X - point1.X);

        //    float error = 0,
        //        threshold = angleCoeff > 0 ? (float)0.5 : (float)-0.5;

        //    int currX = (int)point1.X,
        //        currY = (int)point1.Y;
        //    do
        //    {             
        //        error += angleCoeff;

        //        ++currX;

        //        if (angleCoeff > 0)
        //        {
        //            if (error > threshold)
        //            {
        //                ++currY;

        //                error -= 1;
        //            }
        //        }
        //        else
        //        {
        //            if (error < threshold)
        //            {
        //                --currY;

        //                error += 1;
        //            }
        //        }

        //        MPoint point = null;

        //        if (isAxisWasSwapped)
        //            point = new MPoint(currY, currX, 1);
        //        else
        //            point = new MPoint(currX, currY, 1);

        //        points.Add(point);

        //    } while (currX != (int)point2.X || currY != (int)point2.Y);

        //    return points;
        //}




        //public MPoint MovePointTowards(MPoint a, MPoint b, float distance)
        //{
        //    var vector = new MPoint(b.X - a.X, b.Y - a.Y, b.Z - a.Z);

        //    var length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

        //    var unitVector = new MPoint(vector.X / length, vector.Y / length, vector.Z / length);

        //    return new MPoint(a.X + unitVector.X * distance, a.Y + unitVector.Y * distance, a.Z + unitVector.Z * distance);
        //}
    }
}
