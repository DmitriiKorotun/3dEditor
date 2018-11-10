using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.Shapes;

namespace ZBuffer.ZBufferMath
{
    class BresenhamLine : ILineRasterizer
    {
        public List<MPoint> GetLine(MPoint point1, MPoint point2)
        {
            var points = new List<MPoint>();

            float x1 = point1.X, x2 = point2.X, y1 = point1.Y, y2 = point2.Y;

            var steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1); // Проверяем рост отрезка по оси икс и по оси игрек
                                                               // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x1, ref y1); // Перетасовка координат вынесена в отдельную функцию для красоты
                Swap(ref x2, ref y2);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (point1.X > point2.X)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            int dx = (int)(x2 - x1);
            int dy = (int)Math.Abs(y2 - y1);

            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (point1.Y < point2.Y) ? 1 : -1; // Выбираем направление роста координаты y
            int y = (int)y1;

            for (int x = (int)x1; x <= (int)x2; x++)
            {
                points.Add(new MPoint(steep ? y : x, steep ? x : y, 1)); // Не забываем вернуть координаты на место
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }

            return points;
        }

        private void Swap<T>(ref T l, ref T r)
        {
            T temp = l;

            l = r;

            r = temp;
        }
    }
}
