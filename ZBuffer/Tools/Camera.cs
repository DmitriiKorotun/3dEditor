using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.Shapes;

namespace ZBuffer.Tools
{
    class Camera
    {
        MPoint Viewer;  //точка положения наблюдателя
        MPoint ViewPoint;  //точка зрения
        float Near;  //расстояние до ближней плоскости
        float Far;  //расстояние до дальней плоскости
        float Focus;  //фокусное расстояние
        float RotLeftRight;  //горизонтальный поворот
        float RotUpDown;  //вертикальный поворот

        public Camera(MPoint viewer, MPoint viewPoint, float near, float far, float focus)
        {
            Viewer = viewer;
            ViewPoint = viewPoint;

            Near = near;
            Far = far;
            Focus = focus;
        }

        //FOR TEST?
        public Camera(int width, int heigth, int z)
        {
            MPoint center = new MPoint(width / 2, heigth / 2, z / 2);

            Viewer = new MPoint(center.X, center.Y, center.Z + 200);
            ViewPoint = center;

            Near = 100;
            Far = 200;
            Focus = 70;
        }
    }
}
