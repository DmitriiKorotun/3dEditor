using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ZBuffer.Shapes
{
    public class MBox : MCommonPrimitive
    {
        private const int verticesCount = 8;

        public MPoint[] Vertices { get; set; }  //вершины
        public MFacet[] Facets { get; set; }  //грани
        public float Heigth { get; set; }  //высота
        public float Length { get; set; }  //длина
        public float Width { get; set; }  //ширина

        //FOR TESTS
        public MBox(float width, float length, float height)
        {
            Width = width;
            Length = length;
            Heigth = height;

            Vertices = new MPoint[] {
                new MPoint((float)21.5, (float)45.39, 4), new MPoint((float)21.5 + width, (float)45.39, 4),
                new MPoint((float)21.5 + width, (float)45.39 + length, 4), new MPoint((float)21.5, (float)45.39 + length, 4),
                new MPoint((float)21.5, (float)45.39, 4 + height), new MPoint((float)21.5 + width, (float)45.39, 4 + height),
                new MPoint((float)21.5 + width, (float)45.39 + length, 4 + height), new MPoint((float)21.5, (float)45.39 + length, 4 + height),
            };

            Facets = new MFacet[] {
                new MFacet(Vertices[0], Vertices[1], Vertices[2]),
                new MFacet(Vertices[0], Vertices[1], Vertices[5]),
                new MFacet(Vertices[0], Vertices[3], Vertices[7]),
                new MFacet(Vertices[6], Vertices[5], Vertices[4]),
                new MFacet(Vertices[6], Vertices[5], Vertices[2]),
                new MFacet(Vertices[6], Vertices[7], Vertices[3]),
            };
        }

        public MBox(MPoint leftFaceCorner, float width, float length, float height)
        {
            InitVertices(leftFaceCorner, width, length, height);
            InitFacets(Vertices);
        }

        private void InitVertices(MPoint leftFrontCorner, float width, float length, float height)
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
                        vertex = new MPoint(leftFrontCorner.SX + width, leftFrontCorner.SY, leftFrontCorner.SZ + applyingHeight);
                        break;

                    //Right rear corner
                    case 2:
                        vertex = new MPoint(leftFrontCorner.SX + width, leftFrontCorner.SY + length, leftFrontCorner.SZ + applyingHeight);
                        break;

                    //Left rear corner
                    case 3:
                        vertex = new MPoint(leftFrontCorner.SX, leftFrontCorner.SY + length, leftFrontCorner.SZ + applyingHeight);
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
