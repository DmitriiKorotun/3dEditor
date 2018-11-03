using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ZBuffer.Shapes
{
    [DataContract]
    public class MBox : MCommonPrimitive
    {
        private const int verticesCount = 8;

        [DataMember]
        public MPoint[] Vertices { get; set; }  //вершины
        [DataMember]
        public MFacet[] Facets { get; set; }  //грани

        //public MBox(MPoint leftFaceCorner, float length, float width, float height) : base(leftFaceCorner.X, leftFaceCorner.Y, leftFaceCorner.Z)
        //{
        //    SetParameters(length, width, height);

        //    InitVertices(leftFaceCorner, length, width, height);
        //    InitFacets(Vertices);
        //}

        public MBox(MPoint center, float length, float width, float height) : base(center.X, center.Y, center.Z)
        {
            SetParameters(length, width, height);

            InitVerticesCenter(center, length, width, height);
            InitFacets(Vertices);
        }

        private void SetParameters(float length, float width, float height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        private void InitVerticesCenter(MPoint center, float length, float width, float height)
        {
            Vertices = GetVerticesFromCenter(center, length, width, height);
        }

        private MPoint[] GetVerticesFromCenter(MPoint center, float length, float width, float height)
        {
            const int verticesCount = 8;

            Vertices = new MPoint[verticesCount];

            float halfLength = length / 2,
                halfWidth = width / 2;

            for (int i = 0; i < verticesCount; ++i)
            {
                MPoint vertex = null;

                float halfHeight = i >= verticesCount / 2 ? height / 2 : -height / 2;

                // Calculates new vertices counterclockwise
                switch (i % 4)
                {
                    //Left front corner
                    case 0:
                        vertex = new MPoint( -halfLength, -halfWidth, halfHeight);
                        break;

                    //Right front corner
                    case 1:
                        vertex = new MPoint( halfLength, -halfWidth, halfHeight);
                        break;

                    //Right rear corner
                    case 2:
                        vertex = new MPoint( halfLength, halfWidth, halfHeight);
                        break;

                    //Left rear corner
                    case 3:
                        vertex = new MPoint( -halfLength, halfWidth, halfHeight);
                        break;

                    default:
                        throw new Exception("Error while trying to initialize MBox vertices");
                }

                Vertices[i] = vertex;
            }

            return Vertices;
        }

        private void InitVertices(MPoint leftFrontCorner, float length, float width, float height)
        {
            int verticesCount = 8;

            Vertices = new MPoint[verticesCount];

            Vertices[0] = leftFrontCorner;

            for (int i = 1; i < verticesCount; ++i)
            {
                MPoint vertex = null;

                float applyingHeight = i >= verticesCount / 2 ? height : 0;

                // Calculates new vertices counterclockwise
                switch (i % 4)
                {
                    //Left front corner
                    case 0:
                        vertex = new MPoint(leftFrontCorner.SX, leftFrontCorner.SY, leftFrontCorner.SZ + applyingHeight);
                        break;

                    //Right front corner
                    case 1:
                        vertex = new MPoint(leftFrontCorner.SX + length, leftFrontCorner.SY, leftFrontCorner.SZ + applyingHeight);
                        break;

                    //Right rear corner
                    case 2:
                        vertex = new MPoint(leftFrontCorner.SX + length, leftFrontCorner.SY + width, leftFrontCorner.SZ + applyingHeight);
                        break;

                    //Left rear corner
                    case 3:
                        vertex = new MPoint(leftFrontCorner.SX, leftFrontCorner.SY + width, leftFrontCorner.SZ + applyingHeight);
                        break;

                    default:
                        throw new Exception("Error while trying to initialize MBox vertices");
                }

                Vertices[i] = vertex;
            }
        }

        private void InitFacets(MPoint[] vertices)
        {
            if (vertices.Length != verticesCount)
                throw new Exception("Error while trying to initialize MBox facets");

            Facets = new MFacet[] {
                // Bottom rectangle
                new MFacet(vertices[0], vertices[1], vertices[2]),
                new MFacet(vertices[0], vertices[2], vertices[3]),

                // Front rectangle
                new MFacet(vertices[0], vertices[1], vertices[5]),
                new MFacet(vertices[0], vertices[4], vertices[5]),

                // Left rectangle
                new MFacet(vertices[0], vertices[3], vertices[7]),
                new MFacet(vertices[0], vertices[4], vertices[7]),

                // Right rectangle
                new MFacet(vertices[6], vertices[5], vertices[2]),
                new MFacet(vertices[1], vertices[5], vertices[2]),

                // Rear rectangle
                new MFacet(vertices[6], vertices[7], vertices[3]),
                new MFacet(vertices[6], vertices[2], vertices[3]),

                // Top rectangle
                new MFacet(vertices[6], vertices[5], vertices[4]),
                new MFacet(vertices[6], vertices[7], vertices[4])
            };
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            for (int i = 0; i < Facets.Length; ++i)
                points.AddRange(Facets[i].GetAllPoints());

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            return Vertices.ToList();
        }

        //public override HashSet<Point3D> GetHashedPoints()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
