using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using ZBuffer.Shapes;
using ZBuffer.ZBufferMath;

namespace ZBuffer
{ 
    public class Painter
    {
        public void DrawPoint(Scene scene, Point3D point)
        {
            Int32Rect rect = new Int32Rect(0, 0, (int)scene.Bitmap.Width, (int)scene.Bitmap.Height);

            byte[] pixels = new byte[(int)scene.Bitmap.Width * (int)scene.Bitmap.Height * scene.Bitmap.Format.BitsPerPixel / 8];

            for (int y = 0; y < scene.Bitmap.PixelHeight; y++)
            {
                for (int x = 0; x < scene.Bitmap.PixelWidth; x++)
                {
                    int alpha = 255;
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    int pixelOffset = (x + y * scene.Bitmap.PixelWidth) * scene.Bitmap.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)blue;
                    pixels[pixelOffset + 1] = (byte)green;
                    pixels[pixelOffset + 2] = (byte)red;
                    pixels[pixelOffset + 3] = (byte)alpha;
                }

                int stride = (scene.Bitmap.PixelWidth * scene.Bitmap.Format.BitsPerPixel) / 8;

                scene.Bitmap.WritePixels(rect, pixels, stride, 0);
            }   
        }

        public void DrawScene(Scene scene, HashSet<Point3D> points)
        {
            Int32Rect rect = new Int32Rect(0, 0, (int)scene.Bitmap.Width, (int)scene.Bitmap.Height);

            byte[] pixels = new byte[(int)scene.Bitmap.Width * (int)scene.Bitmap.Height * scene.Bitmap.Format.BitsPerPixel / 8];

            for (int y = 0; y < scene.Bitmap.PixelHeight; y++)
            {
                for (int x = 0; x < scene.Bitmap.PixelWidth; x++)
                {
                    var currPoint = new Point3D(x, y, 0);

                    int alpha = 255;
                    int red = 255;
                    int green = 255;
                    int blue = 255;

                    if (points.Contains(currPoint))
                    {
                        alpha = 255;
                        red = 0;
                        green = 0;
                        blue = 0;
                    }

                    int pixelOffset = (x + y * scene.Bitmap.PixelWidth) * scene.Bitmap.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)blue;
                    pixels[pixelOffset + 1] = (byte)green;
                    pixels[pixelOffset + 2] = (byte)red;
                    pixels[pixelOffset + 3] = (byte)alpha;
                }
            }

            int stride = (scene.Bitmap.PixelWidth * scene.Bitmap.Format.BitsPerPixel) / 8;

            scene.Bitmap.WritePixels(rect, pixels, stride, 0);
        }

        public void DrawSceneByPoints(Scene scene, List<MPoint> points)
        {
            Int32Rect rect = new Int32Rect(0, 0, (int)scene.Bitmap.Width, (int)scene.Bitmap.Height);

            byte[] pixels = new byte[(int)scene.Bitmap.Width * (int)scene.Bitmap.Height * scene.Bitmap.Format.BitsPerPixel / 8];

            for (int i = 0; i < points.Count; ++i)
            {
                points[i].Y = (float)scene.Bitmap.Height - points[i].Y;
            }

            int alpha = 255;
            int red = 0;
            int green = 0;
            int blue = 0;

            for (int i = 0; i < points.Count; ++i)
            {
                int pixelOffset = (int)(points[i].X + points[i].Y * scene.Bitmap.PixelWidth) * scene.Bitmap.Format.BitsPerPixel / 8;

                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = (byte)alpha;
            }

            //for (int y = 0; y < scene.Bitmap.PixelHeight; y++)
            //{
            //    for (int x = 0; x < scene.Bitmap.PixelWidth; x++)
            //    {
            //        var currPoint = new Point3D(x, y, 0);

            //        int alpha = 255;
            //        int red = 255;
            //        int green = 255;
            //        int blue = 255;

            //        if (points.Contains(currPoint))
            //        {
            //            alpha = 255;
            //            red = 0;
            //            green = 0;
            //            blue = 0;
            //        }

            //        int pixelOffset = (x + y * scene.Bitmap.PixelWidth) * scene.Bitmap.Format.BitsPerPixel / 8;
            //        pixels[pixelOffset] = (byte)blue;
            //        pixels[pixelOffset + 1] = (byte)green;
            //        pixels[pixelOffset + 2] = (byte)red;
            //        pixels[pixelOffset + 3] = (byte)alpha;
            //    }               
            //}

            int stride = (scene.Bitmap.PixelWidth * scene.Bitmap.Format.BitsPerPixel) / 8;

            scene.Bitmap.WritePixels(rect, pixels, stride, 0);
        }

        //private void CalculateLinePoints(List<Point3D> points, Point3D point1, Point3D point2)
        //{
        //    //TODO Add z
        //    if (Math.Abs(point2.X - point1.X) <= 1 &&
        //        Math.Abs(point2.Y - point1.Y) <= 1 && Math.Abs(point2.Z - point1.Z) <= 1)
        //        return;

        //    Point3D newPoint = (Point3D)Point3D.Subtract(point2, point1);

        //    newPoint.X = newPoint.X < 0 ? 0 : newPoint.X;
        //    newPoint.Y = newPoint.Y < 0 ? 0 : newPoint.Y;
        //    newPoint.Z = newPoint.Z < 0 ? 0 : newPoint.Z;

        //    points.Add(newPoint);

        //    CalculateLinePoints(points, point1, newPoint);
        //    CalculateLinePoints(points, newPoint, point2);
        //}

        public void DrawFacet(Scene scene, MFacet facet)
        {
            MPoint point1 = new MPoint(facet.Vertices[0].SX, facet.Vertices[0].SY, facet.Vertices[0].SZ);
            MPoint point2 = new MPoint(facet.Vertices[1].SX, facet.Vertices[1].SY, facet.Vertices[1].SZ);

            List<MPoint> points = new VectorMath().GetAllVectorPoints(point1, point2);

            Int32Rect rect = new Int32Rect(0, 0, (int)scene.Bitmap.Width, (int)scene.Bitmap.Height);

            byte[] pixels = new byte[(int)scene.Bitmap.Width * (int)scene.Bitmap.Height * scene.Bitmap.Format.BitsPerPixel / 8];

            for (int y = 0; y < scene.Bitmap.PixelHeight; y++)
            {
                for (int x = 0; x < scene.Bitmap.PixelWidth; x++)
                {
                    var currPoint = new MPoint(x, y, -1);

                    int alpha = 255;
                    int red = 255;
                    int green = 255;
                    int blue = 255;

                    if (points.Contains(currPoint))
                    {
                        alpha = 255;
                        red = 0;
                        green = 0;
                        blue = 0;
                    }

                    int pixelOffset = (x + y * scene.Bitmap.PixelWidth) * scene.Bitmap.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)blue;
                    pixels[pixelOffset + 1] = (byte)green;
                    pixels[pixelOffset + 2] = (byte)red;
                    pixels[pixelOffset + 3] = (byte)alpha;
                }

                int stride = (scene.Bitmap.PixelWidth * scene.Bitmap.Format.BitsPerPixel) / 8;

                scene.Bitmap.WritePixels(rect, pixels, stride, 0);
            }
        }

        public WriteableBitmap DrawCube(int imgWidth, int imgHeigth, int cubeWidth, int cubeHeigth, int centerX, int centerY)
        {
            //cubeWidth = centerX + cubeWidth > imgWidth ? cubeWidth - centerX : cubeWidth;

            //cubeHeigth = centerY + cubeHeigth > imgHeigth ? cubeHeigth - centerY : cubeHeigth;

            // Create the bitmap, with the dimensions of the image placeholder.
            WriteableBitmap wb = new WriteableBitmap(imgWidth,
                imgHeigth, 96, 96, PixelFormats.Bgra32, null);

            // Define the update square (which is as big as the entire image).
            Int32Rect rect = new Int32Rect(0, 0, imgWidth, imgHeigth);

            byte[] pixels = new byte[imgWidth * imgHeigth * wb.Format.BitsPerPixel / 8];
            Random rand = new Random();
            for (int y = 0; y < wb.PixelHeight; y++)
            {
                for (int x = 0; x < wb.PixelWidth; x++)
                {
                    int alpha = 255;
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    // Determine the pixel's color.
                    if (Math.Abs(centerX / 2 - x) > cubeWidth || Math.Abs(centerY / 2 - y) > cubeHeigth)
                    {
                        red = 255;
                        green = 255;
                        blue = 255;
                        alpha = 255;
                    }

                    int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)blue;
                    pixels[pixelOffset + 1] = (byte)green;
                    pixels[pixelOffset + 2] = (byte)red;
                    pixels[pixelOffset + 3] = (byte)alpha;


                }

                int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;

                wb.WritePixels(rect, pixels, stride, 0);
            }

            // Show the bitmap in an Image element.
            return wb;
        }

        public WriteableBitmap DrawRandom(int width, int heigth)
        {
            // Create the bitmap, with the dimensions of the image placeholder.
            WriteableBitmap wb = new WriteableBitmap(width,
                heigth, 96, 96, PixelFormats.Bgra32, null);

            // Define the update square (which is as big as the entire image).
            Int32Rect rect = new Int32Rect(0, 0, width, heigth);

            byte[] pixels = new byte[width * heigth * wb.Format.BitsPerPixel / 8];
            Random rand = new Random();
            for (int y = 0; y < wb.PixelHeight; y++)
            {
                for (int x = 0; x < wb.PixelWidth; x++)
                {
                    int alpha = 0;
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    // Determine the pixel's color.
                    if ((x % 5 == 0) || (y % 7 == 0))
                    {
                        red = (int)((double)y / wb.PixelHeight * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)x / wb.PixelWidth * 255);
                        alpha = 255;
                    }
                    else
                    {
                        red = (int)((double)x / wb.PixelWidth * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)y / wb.PixelHeight * 255);
                        alpha = 50;
                    }

                    int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)blue;
                    pixels[pixelOffset + 1] = (byte)green;
                    pixels[pixelOffset + 2] = (byte)red;
                    pixels[pixelOffset + 3] = (byte)alpha;


                }

                int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;

                wb.WritePixels(rect, pixels, stride, 0);
            }

            // Show the bitmap in an Image element.
            return wb;
        }
    }
}
