using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.Shapes;

namespace ZBuffer.Tools
{
    class OrthographicCamera : Camera
    {
        public OrthographicCamera(float top, float bottom, float left, float right, float far, float near)
        {
            projectionMatrix = new float[,] {
                { 2 / (right - left), 0, 0, -(right + left) / (right - left) },
                { 0, 2 / (top - bottom), 0, -(top + bottom) / (top - bottom) },
                { 0, 0, -2 / (far - near), -(far + near) / (far - near) },
                { 0, 0, 0, 1}
            };
        }
    }
}
