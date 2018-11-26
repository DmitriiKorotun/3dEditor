using EmuEngine.EmuMath;
using EmuEngine.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZBuffer.EmuMath
{
    class ParallelRasterizer : ITriangleRasterizer
    {
        public List<MPoint> RasterizePolygon(MFacet polygon)
        {
            var points = new List<MPoint>();

            SortVertices(polygon.Vertices);

            var upperRightCorner = GetMaxCoordinates(polygon.Vertices);
            var bottomLeftCorner = GetMinCoordinates(polygon.Vertices);

            for (int i = (int)bottomLeftCorner.X; i <= upperRightCorner.X; ++i)
            {
                for (int j = (int)bottomLeftCorner.Y; j <= upperRightCorner.Y; ++j)
                {
                    bool inside = true;

                    var p = new MPoint(i, j, 1);

                    inside &= EdgeFunction(polygon.Vertices[0], polygon.Vertices[1], p);
                    inside &= EdgeFunction(polygon.Vertices[1], polygon.Vertices[2], p);
                    inside &= EdgeFunction(polygon.Vertices[2], polygon.Vertices[0], p);

                    if (inside)
                        points.Add(p);
                }
            }

            return points;
        }

        private bool EdgeFunction(MPoint a, MPoint b, MPoint c)
        {
            return ((c.Y - a.Y) * (b.X - a.X) - (c.X - a.X) * (b.Y - a.Y) <= 0);
        }

        private void SortVertices(MPoint[] vertices)
        {
            if (vertices.Length != 3)
                throw new ArgumentException("Triangle expected, but something else was received to sort");

            //Need to create CCW vertices order
            for (int i = 0; i < vertices.Length - 1; ++i)
            {
                for (int j = 1; j < vertices.Length; ++j)
                {
                    if (vertices[j].X < vertices[i].X)
                        Swap(vertices[j], vertices[i]);
                }
            }

            //If second (not index) vertex is bottom vertex, then CCW order turns into CW order.
            if (vertices[1].Y < vertices[0].Y || vertices[1].Y < vertices[2].Y)
                Swap(vertices[1], vertices[2]);
        }

        private void Swap(ref Vector2 vertex0, ref Vector2 vertex1)
        {
            var temp = vertex0;
            vertex0 = vertex1;
            vertex1 = temp;
        }

        private void Swap(MPoint vertex0, MPoint vertex1)
        {
            var temp = vertex0;
            vertex0 = vertex1;
            vertex1 = temp;
        }

        private Vector2 GetMaxCoordinates(MPoint[] vertices)
        {
            Vector2 bbmin = new Vector2(int.MaxValue, int.MaxValue), bbmax = new Vector2(int.MinValue, int.MinValue);

            for (int i = 0; i < 3; ++i)
            {
                if (vertices[i].X > bbmax.X) bbmax.X = vertices[i].X;
                if (vertices[i].Y > bbmax.Y) bbmax.Y = vertices[i].Y;
            }

            return bbmax;
        }

        private Vector2 GetMinCoordinates(MPoint[] vertices)
        {
            Vector2 bbmin = new Vector2(int.MaxValue, int.MaxValue), bbmax = new Vector2(int.MinValue, int.MinValue);

            for (int i = 0; i < 3; ++i)
            {
                if (vertices[i].X < bbmin.X) bbmin.X = vertices[i].X;
                if (vertices[i].Y < bbmin.Y) bbmin.Y = vertices[i].Y;
            }

            return bbmin;
        }

        public List<MPoint> triangle(MFacet triangle)
        {
            var points = new List<MPoint>();

            Vector2 vertex0 = new Vector2(triangle.Vertices[0].X, triangle.Vertices[0].Y),
                vertex1 = new Vector2(triangle.Vertices[1].X, triangle.Vertices[1].Y),
                vertex2 = new Vector2(triangle.Vertices[2].X, triangle.Vertices[2].Y);        

            if (vertex0.Y == vertex1.Y && vertex0.Y == vertex2.Y) return new BresenhamLine().GetLine(triangle.Vertices[0], triangle.Vertices[2]);
                                                      // sort the vertices, vertex0, vertex1, vertex2 lower-to-upper
            if (vertex0.Y > vertex1.Y) Swap(ref vertex0, ref vertex1);
            if (vertex0.Y > vertex2.Y) Swap(ref vertex0, ref vertex2);
            if (vertex1.Y > vertex2.Y) Swap(ref vertex1, ref vertex2);

            int total_height = (int)(vertex2.Y - vertex0.Y);

            for (int i = 0; i < total_height; i++)
            {
                bool second_half = i > vertex1.Y - vertex0.Y || vertex1.Y == vertex0.Y;

                int segment_height = second_half ? (int)(vertex2.Y - vertex1.Y) : (int)(vertex1.Y - vertex0.Y);

                float alpha = (float)i / total_height;
                float beta = (float)(i - (second_half ? vertex1.Y - vertex0.Y : 0)) / segment_height; // be careful: with above conditions no division by zero here

                Vector2 A = vertex0 + (vertex2 - vertex0) * alpha;
                Vector2 B = second_half ? vertex1 + (vertex2 - vertex1) * beta : vertex0 + (vertex1 - vertex0) * beta;

                if (A.X > B.X) Swap(ref A, ref B);

                for (int j = (int)A.X; j <= B.X; j++)
                {
                    points.Add(new MPoint(j, vertex0.Y + i, 100));

                    if (j > B.X || (A.X < vertex0.X && A.X < vertex1.X && A.X < vertex2.X))
                        Swap(ref vertex0, ref vertex0);
                }
            }

            return points;
        }
    }
}