using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;

namespace EmuEngine.Tools
{
    public class OrthographicCamera : Camera
    {
        public OrthographicCamera(float l, float r, float b, float t, float n, float f) : base()
        {
            //projectionMatrix = SetOrthoFrustum(l, r, b, t, n, f);
            projectionMatrix = SetOrthoFrustum(90, 45, 1.0f, 1000f);
        }

        public OrthographicCamera(float fov, float vfov, float n, float f) : base()
        {
            //projectionMatrix = SetOrthoFrustum(l, r, b, t, n, f);
            projectionMatrix = SetOrthoFrustum(fov, vfov, n, f);
        }

        private float[,] SetOrthoFrustum(float fov, float vfov, float n, float f)
        {
            float r = (float)Math.Tan(fov / 2 * Math.PI / 180);
            float l = -r;
            float t = (float)Math.Tan(vfov / 2 * Math.PI / 180);
            float b = -t;

            r *= 100;
            l *= 100;
            t *= 100;
            b *= 100;

            return new float[4, 4]
            {
                    { 1 / r, 0,     0,                          0 },
                    { 0,     n / t, 0,                          0 },
                    { 0,     0,     -2 / (f - n),               (f + n) / (f - n) },
                    { 0,     0,     0,          1 }
            };
        }

        ///////////////////////////////////////////////////////////////////////////////
        // set a orthographic frustum with 6 params
        // (left, right, bottom, top, near, far)
        ///////////////////////////////////////////////////////////////////////////////
        private float[,] SetOrthoFrustum(float l, float r, float b, float t, float n, float f)
        {
            //OLD
            return new float[,] {
                {
                    2 / (r - l),
                    0,
                    0,
                    -(r + l) / (r - l)
                },
                {
                    0,
                    2 / (t - b),
                    0,
                    - (t + b) / (t - b)
                },
                {   0,
                    0,
                    -2 / (f - n),
                    -(f + n) / (f - n)
                },
                {
                    0,
                    0,
                    0,
                    1
                }
            };

            ////NEW
            //return new float[,] {
            //    {
            //        2 / (r - l),
            //        0,
            //        0,
            //        0
            //    },
            //    {
            //        0,
            //        2 / (t - b),
            //        0,
            //        0
            //    },
            //    {   0,
            //        0,
            //        -2 / (f - n),
            //        0
            //    },
            //    {
            //        -(r + l) / (r - l),
            //        - (t + b) / (t - b),
            //        -(f + n) / (f - n),
            //        1
            //    }
            //};
        }
    }
}
