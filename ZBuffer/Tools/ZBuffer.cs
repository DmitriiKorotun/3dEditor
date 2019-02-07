using EmuEngine.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Tools
{
    class ZBuffer
    {
        private int Width { get; set; }
        private int Height { get; set; }

        public Cell[] Buffer { get; set; }

        public struct Cell
        {
            public float Z { get; set; }  //координата z
            public int ARGB { get; set; }  //цвет пикселя
        };

        public ZBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            Buffer = new Cell[Width * Height];

            for (int i = 0; i < Buffer.Length; ++i)
            {
                Buffer[i] = new Cell();
                Buffer[i].Z = Int32.MaxValue;
                Buffer[i].ARGB = Int32.MaxValue;
            }
        }

        public Cell[] GetBuffer(List<MPoint> points)
        {
            foreach (MPoint point in points)
            {
                if (point.Current.X < 0 || point.Current.X >= 640 ||
                    point.Current.Y < 0 || point.Current.Y >= 360)
                    continue;

                var offset = (int)point.Current.X + (int)point.Current.Y * Width;

                if (Buffer[offset].Z > point.Current.Z)
                {
                    Buffer[offset].Z = point.Current.Z;
                    Buffer[offset].ARGB = point.ARGB;
                }
            }

            return Buffer;
        }
    }
}
