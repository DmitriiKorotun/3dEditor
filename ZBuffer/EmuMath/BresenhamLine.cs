using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;

namespace EmuEngine.EmuMath
{
    class BresenhamLine : ILineRasterizer
    {
        public List<MPoint> GetLine(MPoint point1, MPoint point2)
        {
            return Bresenham3D((int)point1.X, (int)point1.Y, (int)point1.Z, (int)point2.X, (int)point2.Y, (int)point2.Z);
        }

        private List<MPoint> Bresenham3D(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var points = new List<MPoint>();

            //if (x1 < 0 || x1 >= 640 || x2 < 0 || x2 >= 640 || y1 < 0 || y1 >= 320 || y2 < 0 || y2 >= 320 || z1 < 50 || z1 > 200 || z2 < 50 || z2 > 200)
            //    return points;

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
            if (dz == Int32.MinValue)
                dz += 1;
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
                    points.Add(new MPoint(point[0], point[1], point[2] - 2));

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
                    points.Add(new MPoint(point[0], point[1], point[2] - 2));

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
                    points.Add(new MPoint(point[0], point[1], point[2] - 2));

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

            foreach (MPoint pointe in points)
            {
                pointe.ARGB = 16711680;
            }
            return points;
        }

        private void KEK(MPoint point0, MPoint point1)
        {
            const int znear = 50, zfar = 200, width = 640, height = 360;

            bool isPoint0OnScreen = IsPointOnScreen(point0, width, height, znear, zfar),
                isPoint1OnScreen = IsPointOnScreen(point1, width, height, znear, zfar);

            if (!isPoint0OnScreen && !isPoint1OnScreen)
                throw new EntryPointNotFoundException();
            else {
                if (!isPoint0OnScreen)
                    point1 = GetPointOnScreen(point0, point1);
                else
                    point0 = GetPointOnScreen(point0, point1);
            }
        }

        private MPoint GetPointOnScreen(MPoint point0, MPoint point1)
        {
            const int znear = 50, zfar = 200, width = 640, height = 360;

            MPoint sourcePointOnScreen = null;

            bool isPoint0OnScreen = IsPointOnScreen(point0, width, height, znear, zfar),
                isPoint1OnScreen = IsPointOnScreen(point1, width, height, znear, zfar);

            if (!isPoint0OnScreen && !isPoint1OnScreen)
                return null;
            else if (isPoint0OnScreen && isPoint1OnScreen)
                return null;
            else if (isPoint0OnScreen)
                sourcePointOnScreen = point0;
            else
                sourcePointOnScreen = point1;

            var centerPoint = GetCenter(point0, point1);

            var kek = GetPointOnScreen(point0, centerPoint);
            var lol = GetPointOnScreen(centerPoint, point1);

            var pointOnScreen = kek ?? lol;

            return pointOnScreen;
        }

        private bool IsPointOnScreen(MPoint point, int width, int height, int znear, int zfar)
        {
            if (point.X < 0 || point.X >= width || point.Y < 0 || point.Y >= height || point.Z < znear || point.Z > zfar)
                return false;
            else
                return true;
        }

        private MPoint GetCenter(MPoint point0, MPoint point1)
        {
            return new MPoint((point0.X + point1.X) / 2, (point0.Y + point1.Y) / 2, (point0.Z + point1.Z) / 2);
        }

        private void Swap<T>(ref T l, ref T r)
        {
            T temp = l;

            l = r;

            r = temp;
        }
    }
}
