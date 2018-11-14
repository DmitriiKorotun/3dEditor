using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;

namespace EmuEngine.Tools
{
    class PerspectiveCamera : Camera
    {
        MPoint Viewer;  //точка положения наблюдателя
        MPoint ViewPoint;  //точка зрения
        float Near;  //расстояние до ближней плоскости
        float Far;  //расстояние до дальней плоскости
        float Focus;  //фокусное расстояние
        float RotLeftRight;  //горизонтальный поворот
        float RotUpDown;  //вертикальный поворот

        public PerspectiveCamera(float fov, float aspect,
            float znear, float zfar)
        {
            float xymax = znear * (float)Math.Tan(fov * Math.PI);
            float ymin = -xymax;
            float xmin = -xymax;

            float width = xymax - xmin;
            float height = xymax - ymin;

            float depth = zfar - znear;
            float q = -(zfar + znear) / depth;
            float qn = -2 * (zfar * znear) / depth;

            float w = 2 * znear / width;
            w = w / aspect;
            float h = 2 * znear / height;

            projectionMatrix = new float[,] {
                { w, 0, 0, 0 },
                { 0, h, 0, 0 },
                { 0, 0, q, qn },
                { 0, 0, -1, 0}
            };
        }

        //FOR TEST?
        public PerspectiveCamera(int width, int heigth, int z)
        {
            MPoint center = new MPoint(width / 2, heigth / 2, z / 2);

            Viewer = new MPoint(center.X, center.Y, center.Z + 200);
            ViewPoint = center;

            Near = 100;
            Far = 200;
            Focus = 70;
        }

        public PerspectiveCamera(float l, float r, float b, float t, float n, float f) : base()
        {
            projectionMatrix = SetFrustum(l, r, b, t, n, f);
        }

        ///////////////////////////////////////////////////////////////////////////////
        // return a perspective frustum with 6 params
        // (left, right, bottom, top, near, far)
        ///////////////////////////////////////////////////////////////////////////////
        private float[,] SetFrustum(float l, float r, float b, float t, float n, float f)
        {
            return new float[,] {
                {
                    2 * n / (r - l),
                    0,
                    (r + l) / (r - l),
                    0
                },
                {
                    0,
                    2 * n / (t - b),
                    (t + b) / (t - b),
                    0
                },
                {
                    0,
                    0,
                    -(f + n) / (f - n),
                    -(2 * f * n) / (f - n)
                },
                {
                    0,
                    0,
                    -1,
                    0
                }
            };
        }
    }
}
