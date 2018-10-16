using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using ZBuffer.ZBufferMath;

namespace ZBuffer.Shapes
{
    public class MFacet : MShape
    {
        public MPoint[] Vert;  //вершины
        public int Argb;  //цвет грани

        public MFacet(MPoint first, MPoint second, MPoint third)
        {
            Vert = new MPoint[] { first, second, third };
        }

        public MFacet(MPoint first, MPoint second, MPoint third, int argb)
        {
            Vert = new MPoint[] { first, second, third };

            Argb = argb;
        }

        public override HashSet<Point3D> GetHashedPoints()
        {
            var points = new HashSet<Point3D>();

            VectorMath vectorMath = new VectorMath();

            foreach (Point3D point in vectorMath.GetAllVectorPoints(new Point3D(Vert[0].X, Vert[0].Y, Vert[0].Z), new Point3D(Vert[1].X, Vert[1].Y, Vert[1].Z)))
            {
                points.Add(point);
            }

            foreach (Point3D point in vectorMath.GetAllVectorPoints(new Point3D(Vert[2].X, Vert[2].Y, Vert[2].Z), new Point3D(Vert[1].X, Vert[1].Y, Vert[1].Z)))
            {
                points.Add(point);
            }

            foreach (Point3D point in vectorMath.GetAllVectorPoints(new Point3D(Vert[0].X, Vert[0].Y, Vert[0].Z), new Point3D(Vert[2].X, Vert[2].Y, Vert[2].Z)))
            {
                points.Add(point);
            }

            return points;
        }

        public override List<Point3D> GetPoints()
        {
            var points = new List<Point3D>();

            VectorMath vectorMath = new VectorMath();

            points.AddRange(vectorMath.GetAllVectorPoints(new Point3D(Vert[0].X, Vert[0].Y, Vert[0].Z), new Point3D(Vert[1].X, Vert[1].Y, Vert[1].Z)));
            points.AddRange(vectorMath.GetAllVectorPoints(new Point3D(Vert[2].X, Vert[2].Y, Vert[2].Z), new Point3D(Vert[1].X, Vert[1].Y, Vert[1].Z)));
            points.AddRange(vectorMath.GetAllVectorPoints(new Point3D(Vert[0].X, Vert[0].Y, Vert[0].Z), new Point3D(Vert[2].X, Vert[2].Y, Vert[2].Z)));

            return points;
        }
    }
}
