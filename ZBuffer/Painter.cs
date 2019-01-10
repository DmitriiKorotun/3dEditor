using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using EmuEngine.Shapes;
using EmuEngine.EmuMath;
using System.Drawing;

namespace EmuEngine
{ 
    public class Painter
    { 


        public WriteableBitmap DrawSceneByPoints(int width, int height, List<MPoint> points)
        {
            WriteableBitmap wBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);

            Int32Rect rect = new Int32Rect(0, 0, width, height);

            var stride = (rect.Width * wBitmap.Format.BitsPerPixel + 7) / 8;

            byte[] pixels = new byte[rect.Height * stride];

            //Moves 0,0 point from top-left to bottom-left
            for (int i = 0; i < points.Count; ++i)
                points[i].Y = height - points[i].Y;

            var zBufferManager = new Tools.ZBuffer(width, height);
            var zBuffer = zBufferManager.GetBuffer(points);

            for (int i = 0; i < zBuffer.Length; ++i)
            {
                var color = System.Drawing.Color.FromArgb(zBuffer[i].ARGB);

                int pixelOffset = i * wBitmap.Format.BitsPerPixel / 8;

                pixels[pixelOffset] = color.B;
                pixels[pixelOffset + 1] = color.G;
                pixels[pixelOffset + 2] = color.R;
                pixels[pixelOffset + 3] = 255;
            }


            wBitmap.WritePixels(rect, pixels, stride, 0);

            return wBitmap;
        }

        public WriteableBitmap DrawSceneByPoints(int width, int height, byte[] pixels)
        {
            var wBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Rgb24, null);

            int stride = 3 * width;
            wBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);

            return wBitmap;
        }

        public Bitmap DrawSceneByPointsBitmap(int width, int height, List<MPoint> points)
        {
            Bitmap bitmap = new Bitmap(width, height);

            var zBufferManager = new Tools.ZBuffer(width, height);
            var zBuffer = zBufferManager.GetBuffer(points);

            //Moves 0,0 point from top-left to bottom-left
            for (int i = 0; i < points.Count; ++i)
                points[i].Y = height - points[i].Y;

            for (int i = 0; i < bitmap.Width; ++i)
            {
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    var color = System.Drawing.Color.FromArgb(zBuffer[i + j * width].ARGB);

                    bitmap.SetPixel(i, j, color);
                }                    
            }


            return bitmap;
        }

    }
}
