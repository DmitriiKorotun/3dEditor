using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;

namespace EmuEngine.Tools
{
    public class PerspectiveCamera : Camera
    {
        //public PerspectiveCamera(float fov, float aspect,
        //    float znear, float zfar)
        //{
        //    float xymax = znear * (float)Math.Tan(fov * Math.PI);
        //    float ymin = -xymax;
        //    float xmin = -xymax;

        //    float width = xymax - xmin;
        //    float height = xymax - ymin;

        //    float depth = zfar - znear;
        //    float q = -(zfar + znear) / depth;
        //    float qn = -2 * (zfar * znear) / depth;

        //    float w = 2 * znear / width;
        //    w = w / aspect;
        //    float h = 2 * znear / height;

        //    projectionMatrix = new float[,] {
        //        { w, 0, 0, 0 },
        //        { 0, h, 0, 0 },
        //        { 0, 0, q, qn },
        //        { 0, 0, -1, 0}
        //    };
        //}

        public PerspectiveCamera(float l, float r, float b, float t, float n, float f) : base()
        {
            //projectionMatrix = SetFrustum(l, r, b, t, n, f);
            projectionMatrix = SetFrustum(90, 45, 1.0f, 1000f);
        }

        public PerspectiveCamera(float fov, float vfov, float n, float f) : base()
        {
            projectionMatrix = SetFrustum(fov, vfov, n, f);
        }

        private float[,] SetFrustum(float fov, float vfov, float n, float f)
        {
            float r = (float)Math.Tan(fov / 2 * Math.PI / 180);
            float l = -r;
            float t = (float)Math.Tan(vfov / 2 * Math.PI / 180);
            float b = -t;

            return new float[,]
                {
                    { n / r, 0,     0,                     0 },
                    { 0,     n / t, 0,                     0 },
                    { 0,     0,     (n - f) / (f - n),     (2 * f * n) / (f - n) },
                    { 0,     0,     -1, 0 }
                };
        }

        ///////////////////////////////////////////////////////////////////////////////
        // return a perspective frustum with 6 params
        // (left, right, bottom, top, near, far)
        ///////////////////////////////////////////////////////////////////////////////
        private float[,] SetFrustum(float l, float r, float b, float t, float n, float f)
        {
            //OLD
            //return new float[,] {
            //    {
            //        2 * n / (r - l),
            //        0,
            //        (r + l) / (r - l),
            //        0
            //    },
            //    {
            //        0,
            //        2 * n / (b - t),
            //        (b + t) / (b - t),
            //        0
            //    },
            //    {
            //        0,
            //        0,
            //        -(f + n) / (f - n),
            //        -(2 * f * n) / (f - n)
            //    },
            //    {
            //        0,
            //        0,
            //        -1,
            //        0
            //    }
            //};

            //OLD
            //return new float[,] {
            //    {
            //        2 * n / (r - l),
            //        0,
            //        (r + l) / (r - l),
            //        0
            //    },
            //    {
            //        0,
            //        2 * n / (t - b),
            //        (t + b) / (t - b),
            //        0
            //    },
            //    {
            //        0,
            //        0,
            //        -(f + n) / (f - n),
            //        -(2 * f * n) / (f - n)
            //    },
            //    {
            //        0,
            //        0,
            //        -1,
            //        0
            //    }
            //};

            f = 300;
            n = 50;

            //NEW
            int zn = -300, zf = 300, w = 320, h = 200;
            float AR = (float)16 / 9, fovY = 120;

            double angle = Math.PI / 180 * fovY * 0.5 ;

            float A = (float)(AR * 1 / Math.Tan(angle)),
                B = 1 / (float)Math.Tan(angle),
                C = f / (f - n),
                D = -1,
                E = -n * f / (f - n);



            //return new float[,] {
            //    {
            //        A,
            //        0,
            //        0,
            //        0
            //    },
            //    {
            //        0,
            //        B,
            //        0,
            //        0
            //    },
            //    {
            //        0,
            //        0,
            //        C,
            //        D
            //    },
            //    {
            //        0,
            //        0,
            //        E,
            //        0
            //    }
            //};
            return new float[,] {
                {
                    1,
                    0,
                    0,
                    0
                },
                {
                    0,
                    1,
                    0,
                    0
                },
                {
                    0,
                    0,
                    1,
                    D
                },
                {
                    0,
                    0,
                    -1 / 1,
                    1
                }
            };
        }
    }
}
