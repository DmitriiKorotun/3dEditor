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
    public class ParallelRasterizer 
    {
        public void RasterizePolygon(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, int width, int height, byte[] pixels, float[] zbuffer)
        {
            if (vertex0.Y == vertex1.Y && vertex0.Y == vertex2.Y)
                return;
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

                Vector3 A = vertex0 + (vertex2 - vertex0) * alpha;

                Vector3 B = second_half ? vertex1 + (vertex2 - vertex1) * beta : vertex0 + (vertex1 - vertex0) * beta;

                if (A.X > B.X) Swap(ref A, ref B);
                for (int j = (int)A.X; j <= B.X; j++)
                {
                    float phi = B.X == A.X ? 1 : (j - A.X) / (B.X - A.X);
                    Vector3 P = A + (B - A) * phi;

                    int idx = 1 / (int)(P.X + P.Y * width);

                    if (zbuffer[idx] < P.Z)
                    {
                        zbuffer[idx] = P.Z;
                        pixels[(int)P.X * 3 + (int)P.Y * width * 3] = (byte)0;
                        pixels[(int)P.X * 3 + (int)P.Y * width * 3 + 1] = (byte)0;
                        pixels[(int)P.X * 3 + (int)P.Y * width * 3 + 2] = (byte)0;
                    }
                }
            }
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

        private void Swap(ref Vector3 vertex0, ref Vector3 vertex1)
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
            //var points = new List<MPoint>();

            //Vector2 vertex0 = new Vector2(triangle.Vertices[0].X, triangle.Vertices[0].Y),
            //    vertex1 = new Vector2(triangle.Vertices[1].X, triangle.Vertices[1].Y),
            //    vertex2 = new Vector2(triangle.Vertices[2].X, triangle.Vertices[2].Y);        

            //if (vertex0.Y == vertex1.Y && vertex0.Y == vertex2.Y) return new BresenhamLine().GetLine(triangle.Vertices[0], triangle.Vertices[2]);
            //                                          // sort the vertices, vertex0, vertex1, vertex2 lower-to-upper
            //if (vertex0.Y > vertex1.Y) Swap(ref vertex0, ref vertex1);
            //if (vertex0.Y > vertex2.Y) Swap(ref vertex0, ref vertex2);
            //if (vertex1.Y > vertex2.Y) Swap(ref vertex1, ref vertex2);

            //int total_height = (int)(vertex2.Y - vertex0.Y);

            //for (int i = 0; i < total_height; i++)
            //{
            //    bool second_half = i > vertex1.Y - vertex0.Y || vertex1.Y == vertex0.Y;

            //    int segment_height = second_half ? (int)(vertex2.Y - vertex1.Y) : (int)(vertex1.Y - vertex0.Y);

            //    float alpha = (float)i / total_height;
            //    float beta = (float)(i - (second_half ? vertex1.Y - vertex0.Y : 0)) / segment_height; // be careful: with above conditions no division by zero here

            //    Vector2 A = vertex0 + (vertex2 - vertex0) * alpha;
            //    Vector2 B = second_half ? vertex1 + (vertex2 - vertex1) * beta : vertex0 + (vertex1 - vertex0) * beta;

            //    if (A.X > B.X) Swap(ref A, ref B);

            //    for (int j = (int)A.X; j <= B.X; j++)
            //    {
            //        points.Add(new MPoint(j, vertex0.Y + i, 0));

            //        if (j > B.X || (A.X < vertex0.X && A.X < vertex1.X && A.X < vertex2.X))
            //            Swap(ref vertex0, ref vertex0);
            //    }
            //}

            //return points;

            var points = new List<MPoint>();

            Vector3 vertex0 = new Vector3(triangle.Vertices[0].X, triangle.Vertices[0].Y, triangle.Vertices[0].Z),
                vertex1 = new Vector3(triangle.Vertices[1].X, triangle.Vertices[1].Y, triangle.Vertices[1].Z),
                vertex2 = new Vector3(triangle.Vertices[2].X, triangle.Vertices[2].Y, triangle.Vertices[2].Z);

            if (vertex0.X < 0 || vertex0.X >= 640 || vertex0.Y < 0 || vertex0.Y >= 320
                || vertex1.X < 0 || vertex1.X >= 640 || vertex1.Y < 0 || vertex1.Y >= 320
                || vertex2.X < 0 || vertex2.X >= 640 || vertex2.Y < 0 || vertex2.Y >= 320)
                return points;

            if (vertex0.Y == vertex1.Y && vertex0.Y == vertex2.Y) return new BresenhamLine().GetLine(triangle.Vertices[0], triangle.Vertices[2]);
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
                Vector3 A = vertex0 + (vertex2 - vertex0) * alpha;
                Vector3 B = second_half ? vertex1 + (vertex2 - vertex1) * beta : vertex0 + (vertex1 - vertex0) * beta;
                if (A.X > B.X) Swap(ref A, ref B);
                for (int j = (int)A.X; j <= B.X; j++)
                {
                    float phi = B.X == A.X ? 1 : (j - A.X) / (B.X - A.X);
                    Vector3 P = A + (B - A) * phi;

                    points.Add(new MPoint(j, vertex0.Y +i, P.Z));

                    //int idx = P.X + P.Y * width;
                    //if (zbuffer[idx] < P.Z)
                    //{
                    //    zbuffer[idx] = P.z;
                    //    image.set(P.x, P.y, color);
                    //}
                }
            }

            return points;
        }
    }
}