using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.Shapes;
using ZBuffer.ZBufferMath;

namespace ZBuffer.Affine_Transformation
{
    public class ShapeEditor
    {
        public float[,] RotateY(MPoint shapeCenter, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateY = {
                { (float)Math.Cos(rads), 0, (float)Math.Sin(rads), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(rads), 0, (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float[,] shapeCoords = { { shapeCenter.SX }, { shapeCenter.SY }, { shapeCenter.SZ }, { 1 } };

            return MatrixMultiplier.MultiplyMatrix(rotateY, shapeCoords);
        }

        public float[,] RotateX(MPoint shapeCenter, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateX = {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
                { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float[,] shapeCoords = { { shapeCenter.SX }, { shapeCenter.SY }, { shapeCenter.SZ }, { 1 } };

            return MatrixMultiplier.MultiplyMatrix(rotateX, shapeCoords);
        }

        //TO DEL
        public void RotateXVertices(MPoint[] vertices, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateX = {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
                { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            for (int i = 0; i < vertices.Length; ++i)
            {
                float[,] shapeCoords = { { vertices[i].SX }, { vertices[i].SY }, { vertices[i].SZ }, { 1 } };

                float[,] newCoords = MatrixMultiplier.MultiplyMatrix(rotateX, shapeCoords);

                vertices[i].X = newCoords[0, 0];
                vertices[i].Y = newCoords[1, 0];
                vertices[i].Z = newCoords[2, 0];
            }
        }

        public float[,] RotateZ(MPoint shapeCenter, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateZ = {
                { (float)Math.Cos(rads), -(float)Math.Sin(rads), 0, 0 },
                { (float)Math.Sin(rads), (float)Math.Cos(rads), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float[,] shapeCoords = { { shapeCenter.SX }, { shapeCenter.SY }, { shapeCenter.SZ }, { 1 } };

            return MatrixMultiplier.MultiplyMatrix(rotateZ, shapeCoords);
        }

        //TO DEL
        public void RotateZVertices(MPoint[] vertices, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateZ = {
                { (float)Math.Cos(rads), -(float)Math.Sin(rads), 0, 0 },
                { (float)Math.Sin(rads), (float)Math.Cos(rads), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            for (int i = 0; i < vertices.Length; ++i)
            {
                float[,] shapeCoords = { { vertices[i].X }, { vertices[i].Y }, { vertices[i].Z }, { 1 } };

                float[,] newCoords = MatrixMultiplier.MultiplyMatrix(rotateZ, shapeCoords);

                vertices[i].X = newCoords[0, 0];
                vertices[i].Y = newCoords[1, 0];
                vertices[i].Z = newCoords[2, 0];
            }
        }

        public float[,] Scale(MPoint shapeCenter, float Sx, float Sy, float Sz)
        {
            //формируем матрицу масштабирования;
            float[,] rotateZ = {
                { Sx, 0, 0, 0 },
                { 0, Sy, 0, 0 },
                { 0, 0, Sz, 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float[,] shapeCoords = { { shapeCenter.SX }, { shapeCenter.SY }, { shapeCenter.SZ }, { 1 } };

            return MatrixMultiplier.MultiplyMatrix(rotateZ, shapeCoords);
        }
    }
}
