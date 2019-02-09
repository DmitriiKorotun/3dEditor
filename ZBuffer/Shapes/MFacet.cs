using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using EmuEngine.EmuMath;
using ZBuffer.EmuMath;

namespace EmuEngine.Shapes
{
    [DataContract]
    public class MFacet : MCommonPrimitive
    {
        [DataMember]
        public MPoint[] Vertices { get; set; }  //вершины
        [DataMember]
        public int Argb { get; set; }  //цвет грани

        public MFacet(MPoint first, MPoint second, MPoint third)
        {
            Vertices = new MPoint[] { first, second, third };
        }

        public MFacet(MPoint first, MPoint second, MPoint third, int argb)
        {
            Vertices = new MPoint[] { first, second, third };

            Argb = argb;
        }

        //public override HashSet<Point3D> GetHashedPoints()
        //{
        //    var points = new HashSet<Point3D>();

        //    VectorMath vectorMath = new VectorMath();

        //    foreach (Point3D point in vectorMath.GetAllVectorPoints(new Point3D(Vert[0].X, Vert[0].Y, Vert[0].Z), new Point3D(Vert[1].X, Vert[1].Y, Vert[1].Z)))
        //    {
        //        points.Add(point);
        //    }

        //    foreach (Point3D point in vectorMath.GetAllVectorPoints(new Point3D(Vert[2].X, Vert[2].Y, Vert[2].Z), new Point3D(Vert[1].X, Vert[1].Y, Vert[1].Z)))
        //    {
        //        points.Add(point);
        //    }

        //    foreach (Point3D point in vectorMath.GetAllVectorPoints(new Point3D(Vert[0].X, Vert[0].Y, Vert[0].Z), new Point3D(Vert[2].X, Vert[2].Y, Vert[2].Z)))
        //    {
        //        points.Add(point);
        //    }

        //    return points;
        //}

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            VectorMath vectorMath = new VectorMath();

            points.AddRange(vectorMath.GetAllVectorPoints(
                new MPoint(Vertices[0].Current.X, Vertices[0].Current.Y, Vertices[0].Current.Z),
                new MPoint(Vertices[1].Current.X, Vertices[1].Current.Y, Vertices[1].Current.Z)));

            points.AddRange(vectorMath.GetAllVectorPoints(
                new MPoint(Vertices[2].Current.X, Vertices[2].Current.Y, Vertices[2].Current.Z),
                new MPoint(Vertices[1].Current.X, Vertices[1].Current.Y, Vertices[1].Current.Z)));

            points.AddRange(vectorMath.GetAllVectorPoints(
                new MPoint(Vertices[0].Current.X, Vertices[0].Current.Y, Vertices[0].Current.Z),
                new MPoint(Vertices[2].Current.X, Vertices[2].Current.Y, Vertices[2].Current.Z)));

            points.AddRange(new ParallelRasterizer().triangle(this));

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            return Vertices.ToList();
        }
    }
}
