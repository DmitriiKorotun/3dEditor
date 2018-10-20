using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBuffer.Shapes
{
    public class MPoint
    {
        public float X { get; set; }  //текущий х
        public float Y { get; set; }  //текущий у
        public float Z { get; set; }  //текущая z
        public float W { get; set; }  //текщий параметр масштабирования
        public float SX { get; }  //начальный х
        public float SY { get; }  //начальный y
        public float SZ { get; }  //начальная z
        public float SW { get; }  //начальный параметр масштабирования

        public MPoint(float x, float y, float z)
        {
            X = x;
            SX = X;

            Y = y;
            SY = Y;

            Z = z;
            SZ = Z;

            W = 1;
            SW = W;
        }

        public MPoint(double x, double y, double z)
        {
            X = (float)x;
            SX = X;

            Y = (float)y;
            SY = Y;

            Z = (float)z;
            SZ = Z;

            W = 1;
            SW = W;
        }

        public MPoint(float x, float y, float z, float w)
        {
            X = x;
            SX = X;

            Y = y;
            SY = Y;

            Z = z;
            SZ = Z;

            W = w;
            SW = W;
        }

        public MPoint(double x, double y, double z, double w)
        {
            X = (float)x;
            SX = X;

            Y = (float)y;
            SY = Y;

            Z = (float)z;
            SZ = Z;

            W = (float)w;
            SW = W;
        }
    }
}
