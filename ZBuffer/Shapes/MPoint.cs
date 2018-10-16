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
            SX = x;

            Y = y;
            SY = y;

            Z = z;
            SZ = z;

            W = 1;
            SW = 1;
        }

        public MPoint(float x, float y, float z, float w)
        {
            X = x;
            SX = x;

            Y = y;
            SY = y;

            Z = z;
            SZ = z;

            W = w;
            SW = w;
        }
    }
}
