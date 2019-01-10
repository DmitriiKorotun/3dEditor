using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;
using System.Numerics;

namespace EmuEngine.EmuMath
{
    class EdgeRasterization : ITriangleRasterizer
    {
        public List<MPoint> RasterizePolygon(MFacet polygon)
        {
            throw new NotImplementedException();
        }

        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            T tmp = lhs;
            lhs = rhs;
            rhs = tmp;
        }

        static void SetBoundingBoxMinMax(ref Vector2i bbmin, ref Vector2i bbmax, ref Vector2i v)
        {
            if (v.X < bbmin.X) bbmin.X = v.X;
            if (v.Y < bbmin.Y) bbmin.Y = v.Y;
            if (v.X > bbmin.X) bbmax.X = v.X;
            if (v.Y > bbmin.Y) bbmax.Y = v.Y;
        }

        static void SetBoundingBoxMinMax(ref Vector2i bbmin, ref Vector2i bbmax, ref Vector3i v)
        {
            if (v.X < bbmin.X) bbmin.X = v.X;
            if (v.Y < bbmin.Y) bbmin.Y = v.Y;
            if (v.X > bbmin.X) bbmax.X = v.X;
            if (v.Y > bbmin.Y) bbmax.Y = v.Y;
        }

        static void SetBoundingBoxMinMax(ref Vector2i bbmin, ref Vector2i bbmax, ref Vector3 v)
        {
            if (v.X < bbmin.X) bbmin.X = (int)v.X;
            if (v.Y < bbmin.Y) bbmin.Y = (int)v.Y;
            if (v.X > bbmax.X) bbmax.X = (int)Math.Ceiling(v.X);
            if (v.Y > bbmax.Y) bbmax.Y = (int)Math.Ceiling(v.Y);
        }

        private void testc(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
        {
            var c = Vector3.Normalize(v0);

            //if (c.X )
            //if (v0.X > v1.X)
            //{
            //    if (v0.Y > v1.Y)
            //        Swap(ref v0, ref v1);
            //    else if (v0.Y < v1.Y)
            //}
        }

        public static void TriangleUsingEdgeFunctionZBufferKek(Vector3 v0, Vector3 v1, Vector3 v2, int width, int height, byte[] pixels, float[] zbuffer, int rgb)
        {

            //var c = Vector3.Normalize(v0);
            //var b = Vector3.Normalize(v1);
            //var e = Vector3.Normalize(v2);

            //if(c.X > b.X && c.X > e.X)
            //{
            //    if (b.X < e.X)
            //        Swap(ref v1, ref v2);
            //}
            //else if (b.X > c.X && b.X > e.X)
            //{
            //    Swap(ref v0, ref v1);

            //    if (c.X < e.X)
            //        Swap(ref v1, ref v2);                
            //}
            //else
            //{
            //    Swap(ref v0, ref v2);

            //    if (c.X < b.X)
            //        Swap(ref v1, ref v2);
            //}
            //var max = c.X;
            //var min = c.X;

            //if (max < b.X)
            //    max = b.X;

            //if (max < e.X)
            //    max = e.X;

            //if (min > b.X)
            //    min = b.X;

            //if (min > e.X)
            //    min = e.X;


            if (v0.X > v1.X)
                Swap(ref v0, ref v1);

            if (v1.X > v2.X)
                Swap(ref v1, ref v2);

            if (v0.X > v1.X)
                Swap(ref v0, ref v1);

            if (v1.Y < v2.Y)
                Swap(ref v1, ref v2);

            if (v0.Y > v2.Y && v0.Y > v1.Y && v1.X < v2.X)
                Swap(ref v0, ref v2);

            if (v0.X == v1.X && v0.Y > v1.Y)
                Swap(ref v0, ref v1);

            if (v0.X == v1.X && v0.Y > v1.Y)
                Swap(ref v0, ref v1);

            TriangleUsingEdgeFunctionZBufferTop(v0, v1, v2, width, height, pixels, zbuffer, rgb);
        }


            public static void TriangleUsingEdgeFunctionZBufferTop(Vector3 v0, Vector3 v1, Vector3 v2, int width, int height, byte[] pixels, float[] zbuffer, int rgb)
        {
            Vector2i bbmin = new Vector2i((int)v0.X, (int)v0.Y);
            Vector2i bbmax = new Vector2i((int)v0.X, (int)v0.Y);

            SetBoundingBoxMinMax(ref bbmin, ref bbmax, ref v1);
            SetBoundingBoxMinMax(ref bbmin, ref bbmax, ref v2);
            float area = EdgeFunction(v0, v1, v2);

            float invZ0 = v0.Z;
            float invZ1 = v1.Z;
            float invZ2 = v2.Z;

            var color = System.Drawing.Color.FromArgb(rgb);
            int r = color.R, g = color.G, b = color.B;
            for (int x = bbmin.X > 0 ? bbmin.X : 0; x < (bbmax.X + 1 < width ? bbmax.X + 1 : width); x++)
            {
                for (int y = bbmin.Y > 0 ? bbmin.Y : 0; y < (bbmax.Y + 1 < height ? bbmax.Y + 1 : height); y++)
                {

                    Vector3 p = new Vector3(x, y, 0);
                    float w0 = EdgeFunction(v0, v1, p);
                    float w1 = EdgeFunction(v1, v2, p);
                    float w2 = EdgeFunction(v2, v0, p);
                    if (w0 >= 0 && w1 >= 0 && w2 >= 0)
                    {
                        w0 /= area;
                        w1 /= area;
                        w2 /= area;

                        float pz = 1 / (invZ0 * w0 + invZ1 * w2 + invZ2 * w1);

                        int idx = x + y * width;

                        if (pz > zbuffer[idx])
                        {
                            zbuffer[idx] = pz;
                            pixels[x * 3 + y * width * 3] = (byte)r;
                            pixels[x * 3 + y * width * 3 + 1] = (byte)g;
                            pixels[x * 3 + y * width * 3 + 2] = (byte)b;
                        }
                    }
                }
            }
        }

        public static void TriangleUsingEdgeFunctionZBufferBot(Vector3 v0, Vector3 v1, Vector3 v2, int width, int height, byte[] pixels, float[] zbuffer)
        {
            Vector2i bbmin = new Vector2i((int)v0.X, (int)v0.Y);
            Vector2i bbmax = new Vector2i((int)v0.X, (int)v0.Y);

            SetBoundingBoxMinMax(ref bbmin, ref bbmax, ref v1);
            SetBoundingBoxMinMax(ref bbmin, ref bbmax, ref v2);
            float area = EdgeFunction(v0, v1, v2);

            float invZ0 = v0.Z;
            float invZ1 = v1.Z;
            float invZ2 = v2.Z;

            for (int x = bbmin.X > 0 ? bbmin.X : 0; x < (bbmax.X + 1 < width ? bbmax.X + 1 : width); x++)
            {
                for (int y = bbmin.Y > 0 ? bbmin.Y : 0; y < (bbmax.Y + 1 < height ? bbmax.Y + 1 : height); y++)
                {

                    Vector3 p = new Vector3(x, y, 0);
                    float w0 = EdgeFunction(v0, v1, p);
                    float w1 = EdgeFunction(v1, v2, p);
                    float w2 = EdgeFunction(v2, v0, p);
                    if (w0 <= 0 && w1 <= 0 && w2 <= 0)
                    {
                        w0 /= area;
                        w1 /= area;
                        w2 /= area;

                        float pz = 1 / (invZ0 * w0 + invZ1 * w2 + invZ2 * w1);

                        int idx = x + y * width;

                        if (pz > zbuffer[idx])
                        {
                            zbuffer[idx] = pz;
                            pixels[x * 3 + y * width * 3] = (byte)0;
                            pixels[x * 3 + y * width * 3 + 1] = (byte)0;
                            pixels[x * 3 + y * width * 3 + 2] = (byte)0;
                        }
                    }
                }
            }
        }

        public static void TriangleUsingEdgeFunctionZBufferBool(Vector3 v0, Vector3 v1, Vector3 v2, int width, int height, byte[] pixels, float[] zbuffer)
        {
            Vector2i bbmin = new Vector2i((int)v0.X, (int)v0.Y);
            Vector2i bbmax = new Vector2i((int)v0.X, (int)v0.Y);

            SetBoundingBoxMinMax(ref bbmin, ref bbmax, ref v1);
            SetBoundingBoxMinMax(ref bbmin, ref bbmax, ref v2);
            float area = EdgeFunction(v0, v1, v2);

            float invZ0 = v0.Z;
            float invZ1 = v1.Z;
            float invZ2 = v2.Z;

            for (int x = bbmin.X > 0 ? bbmin.X : 0; x < (bbmax.X + 1 < width ? bbmax.X + 1 : width); x++)
            {
                for (int y = bbmin.Y > 0 ? bbmin.Y : 0; y < (bbmax.Y + 1 < height ? bbmax.Y + 1 : height); y++)
                {
                    Vector3 p = new Vector3(x, y, 0);

                    bool inside = true;
                    inside &= EdgeFunctionBool(v0, v1, p);
                    inside &= EdgeFunctionBool(v1, v2, p);
                    //inside &= EdgeFunctionBool(v2, v0, p);

                    if (inside == true)
                    {
                        //w0 /= area;
                        //w1 /= area;
                        //w2 /= area;

                        //float pz = 1 / (invZ0 * w0 + invZ1 * w2 + invZ2 * w1);

                        int idx = x + y * width;

                        zbuffer[idx] = 1;
                        pixels[x * 3 + y * width * 3] = (byte)0;
                        pixels[x * 3 + y * width * 3 + 1] = (byte)0;
                        pixels[x * 3 + y * width * 3 + 2] = (byte)0;
                    }
                }
            }
        }

        static bool EdgeFunctionBool(Vector3 v0, Vector3 v1, Vector3 p)
        {
            return ((v1.X - v0.X) * (p.Y - v0.Y) - (v1.Y - v0.Y) * (p.X - v0.X) >= 0);
        }

        static float EdgeFunction(Vector3 v0, Vector3 v1, Vector3 p)
        {
            return (p.X - v0.X) * (v1.Y - v0.Y) - (p.Y - v0.Y) * (v1.X - v0.X);
        }
    }
}
