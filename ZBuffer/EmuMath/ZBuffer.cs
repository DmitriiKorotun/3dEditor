using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.EmuMath;

namespace EmuEngine.EmuMath
{
    class ZBuffer
    {
        float[] zbuffer;
        byte[] xybuffer;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public ZBuffer(byte[] xybuffer, int width, int height)
        {
            zbuffer = Enumerable.Repeat(float.NegativeInfinity, width * height).ToArray();
            this.xybuffer = xybuffer;
            Width = width;
            Height = height;
        }

        public void DrawTriangleToXyBuffer(Vector3 v0, Vector3 v1, Vector3 v2, int rgb)
        {
            EdgeRasterization.TriangleUsingEdgeFunctionZBufferKek(v0, v1, v2, Width, Height, xybuffer, zbuffer, rgb);
            //var ls = new ParallelRasterizer();
            //ls.RasterizePolygon(v0, v1, v2, Width, Height, xybuffer, zbuffer);
        }

        public void Resize(int width, int height)
        {
            int prevSize = Width * Height;
            int newSize = width * height;
            Array.Resize(ref zbuffer, newSize);

            if (prevSize < newSize)
            {
                for (int i = prevSize; i < newSize; i++)
                    zbuffer[i] = float.NegativeInfinity;
            }

            Width = width;
            Height = height;
        }

        public void SetXYBuffer(byte[] xybuffer)
        {
            this.xybuffer = xybuffer;
        }

        public void Reset()
        {
            for (int i = 0; i < zbuffer.Length; i++)
                zbuffer[i] = float.NegativeInfinity;

            //Parallel.For(0, zbuffer.Length, i =>
            //{
            //    zbuffer[i] = float.NegativeInfinity;
            //});
        }
    }
}
