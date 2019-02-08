using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;
using EmuEngine.EmuMath.Structures;

namespace EmuEngine.Tools
{
    public class OrthographicCamera : Camera
    {
        public OrthographicCamera(float l, float r, float b, float t, float n, float f) : base()
        {
            projectionMatrix = SetOrthoFrustum(l, r, b, t, n, f);
        }

        ///////////////////////////////////////////////////////////////////////////////
        // set a orthographic frustum with 6 params
        // (left, right, bottom, top, near, far)
        ///////////////////////////////////////////////////////////////////////////////
        private Matrix4 SetOrthoFrustum(float l, float r, float b, float t, float n, float f)
        {
            return new Matrix4(new float[,] {
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
                    -(t + b) / (t - b)
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
            });
        }
    }
}
