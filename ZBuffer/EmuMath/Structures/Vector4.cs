using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.EmuMath.Structures
{
    public class Vector4 : ICloneable
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public Vector4()
        {
            X = 0;
            Y = 0;
            Z = 0;
            W = 0;
        }

        public Vector4(float value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public object Clone()
        {
            return new Vector4(X, Y, Z, W);
        }
    }
}
