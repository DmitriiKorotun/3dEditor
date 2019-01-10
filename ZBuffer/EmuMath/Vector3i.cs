using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.EmuMath
{
    public class Vector3i : Vector2i
    {
        public float Z { get; set; }

        public Vector3i(int x, int y, float z) : base(x, y)
        {
            Z = z;
        }

        public Vector3i(Vector3i v) : base(v.X, v.Y)
        {
            Z = v.Z;
        }

        public static explicit operator Vector3i(Vector3 v)
        {
            return new Vector3i((int)v.X, (int)v.Y, v.Z);
        }
    }
}
