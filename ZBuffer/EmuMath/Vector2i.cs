using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.EmuMath
{
    public class Vector2i
    {
        public int X { set; get; }
        public int Y { set; get; }

        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2i(Vector2i v)
        {
            X = v.X;
            Y = v.Y;
        }

        public static explicit operator Vector2i(Vector3 v)
        {
            return new Vector2i((int)v.X, (int)v.Y);
        }
    }
}
