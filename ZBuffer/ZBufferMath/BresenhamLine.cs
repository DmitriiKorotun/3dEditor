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
            return Bresenham3D((int)point1.X, (int)point1.Y, (int)point1.Z, (int)point2.X, (int)point2.Y, (int)point2.Z);
            //var points = new List<MPoint>();

            //float x1 = point1.X, x2 = point2.X, y1 = point1.Y, y2 = point2.Y;

            //var steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1); // Проверяем рост отрезка по оси икс и по оси игрек
            //                                                   // Отражаем линию по диагонали, если угол наклона слишком большой
            //if (steep)
            //{
            //    Swap(ref x1, ref y1); // Перетасовка координат вынесена в отдельную функцию для красоты
            //    Swap(ref x2, ref y2);
            //}
            //// Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            //if (x1 > x2)
            //{
            //    Swap(ref x1, ref x2);
            //    Swap(ref y1, ref y2);
            //}

            //int dx = (int)(x2 - x1);
            //int dy = (int)Math.Abs(y2 - y1);

            //int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            //int ystep = (y1 < y2) ? 1 : -1; // Выбираем направление роста координаты y
            //int y = (int)y1;

            //for (int x = (int)x1; x <= (int)x2; x++)
            //{
            //    points.Add(new MPoint(steep ? y : x, steep ? x : y, 1)); // Не забываем вернуть координаты на место
            //    error -= dy;
            //    if (error < 0)
            //    {
            //        y += ystep;
            //        error += dx;
            //    }
            //}

            //return points;
        }

        //private List<MPoint> ls(float x1, float y1, float x2, float y2)
        //{
        //    var points = new List<MPoint>();

        //    var steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1); // Проверяем рост отрезка по оси икс и по оси игрек
        //                                                       // Отражаем линию по диагонали, если угол наклона слишком большой
        //    if (steep)
        //    {
        //        Swap(ref x1, ref y1); // Перетасовка координат вынесена в отдельную функцию для красоты
        //        Swap(ref x2, ref y2);
        //    }
        //    // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
        //    if (x1 > x2)
        //    {
        //        Swap(ref x1, ref x2);
        //        Swap(ref y1, ref y2);
        //    }

        //    int dx = (int)(x2 - x1);
        //    int dy = (int)Math.Abs(y2 - y1);

        //    int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
        //    int ystep = (y1 < y2) ? 1 : -1; // Выбираем направление роста координаты y
        //    int y = (int)y1;

        //    for (int x = (int)x1; x <= (int)x2; x++)
        //    {
        //        points.Add(new MPoint(steep ? y : x, steep ? x : y, 1)); // Не забываем вернуть координаты на место
        //        error -= dy;
        //        if (error < 0)
        //        {
        //            y += ystep;
        //            error += dx;
        //        }
        //    }



        //    return points;
        //}

        //private void GetZCoordinates(float z1, float z2, int pointsCount)
        //{
        //    if (z1 > z2)
        //        Swap(ref z1, ref z2);

        //    float error = (z2 - z1) / pointsCount,
        //        ,
        //        treshold = (int)0.5;

        //    for (int z = (int)z1; z <= (int)z2;)
        //    {
        //        if (error >= treshold)
        //            ++z;

        //        z += error;
        //        points.Add(new MPoint(steep ? y : x, steep ? x : y, 1)); // Не забываем вернуть координаты на место
        //        error -= dy;
        //        if (error < 0)
        //        {
        //            y += ystep;
        //            error += dx;
        //        }
        //    }
        //}

        private List<MPoint> Bresenham3D(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var points = new List<MPoint>();

            float x0 = x1, y0 = y1, z0 = z1;
            int i, dx, dy, dz, l, m, n, x_inc, y_inc, z_inc, err_1, err_2, dx2, dy2, dz2;
            int[] point = new int[3];

            point[0] = x1;
            point[1] = y1;
            point[2] = z1;

            dx = x2 - x1;
            dy = y2 - y1;
            dz = z2 - z1;

            x_inc = (dx < 0) ? -1 : 1;
            l = Math.Abs(dx);

            y_inc = (dy < 0) ? -1 : 1;
            m = Math.Abs(dy);

            z_inc = (dz < 0) ? -1 : 1;
            n = Math.Abs(dz);

            dx2 = l << 1;
            dy2 = m << 1;
            dz2 = n << 1;

            if ((l >= m) && (l >= n))
            {
                err_1 = dy2 - l;
                err_2 = dz2 - l;
                for (i = 0; i < l; i++)
                {
                    points.Add(new MPoint(point[0], point[1], point[2]));

                    if (err_1 > 0)
                    {
                        point[1] += y_inc;
                        err_1 -= dx2;
                    }
                    if (err_2 > 0)
                    {
                        point[2] += z_inc;
                        err_2 -= dx2;
                    }
                    err_1 += dy2;
                    err_2 += dz2;
                    point[0] += x_inc;
                }
            }
            else if ((m >= l) && (m >= n))
            {
                err_1 = dx2 - m;
                err_2 = dz2 - m;
                for (i = 0; i < m; i++)
                {
                    points.Add(new MPoint(point[0], point[1], point[2]));

                    if (err_1 > 0)
                    {
                        point[0] += x_inc;
                        err_1 -= dy2;
                    }
                    if (err_2 > 0)
                    {
                        point[2] += z_inc;
                        err_2 -= dy2;
                    }
                    err_1 += dx2;
                    err_2 += dz2;
                    point[1] += y_inc;
                }
            }
            else
            {
                err_1 = dy2 - n;
                err_2 = dx2 - n;
                for (i = 0; i < n; i++)
                {
                    points.Add(new MPoint(point[0], point[1], point[2]));

                    if (err_1 > 0)
                    {
                        point[1] += y_inc;
                        err_1 -= dz2;
                    }
                    if (err_2 > 0)
                    {
                        point[0] += x_inc;
                        err_2 -= dz2;
                    }
                    err_1 += dy2;
                    err_2 += dx2;
                    point[2] += z_inc;
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
