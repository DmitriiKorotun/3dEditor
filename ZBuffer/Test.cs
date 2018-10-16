using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.Shapes;
using ZBuffer.ZBufferMath;

namespace ZBuffer
{
    public class Test
    {
        //public void Zoom()
        //{
        //    float[,] scale = new float[,] { { param, 0, 0, 0 }, { 0, param, 0, 0 }, { 0, 0, param, 0 }, { 0, 0, 0, 1 } };

        //    //получаем координаты центральной точки:
        //    dx = center.GetX();
        //    dy = center.GetY();
        //    dz = center.GetZ();

        //    //формируем матрицу переноса объекта
        //    float[,] move = new float[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -dx, -dy, -dz, 1 } };

        //    //формируем матрицу обратного переноса
        //    float[,] unmove = new float[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { dx, dy, dz, 1 } };

        //    //умножаем матрицы
        //    this->MatrixMultiplication(move);
        //    this->MatrixMultiplication(scale);
        //    this->MatrixMultiplication(unmove);
        //    //применяем к точкам объекта
        //    this->box.ReplaceBox(matrix);
        //    this->top_box.ReplaceBox(matrix);
        //    this->upper_box.ReplaceBox(matrix);
        //    this->top_cylinder.ReplaceTopCylinder(matrix);
        //    this->top_tor.ReplaceBorder(matrix);
        //    this->VolController[0].ReplaceSideCylinder(matrix);
        //    this->VolController[1].ReplaceSideCylinder(matrix);
        //    this->VolController[2].ReplaceSideCylinder(matrix);
        //}

        public float[,] RotateY(MPoint shapeCenter, double angle)
        {
            //формируем матрицу поворота;
            float[,] rotateY = {
                { (float)Math.Cos(angle * Math.PI / 180.0), 0, (float)Math.Sin(angle * Math.PI / 180.0), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(angle * Math.PI / 180.0), 0, (float)Math.Cos(angle * Math.PI / 180.0), 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float[,] shapeCoords = { { shapeCenter.SX }, { shapeCenter.SY }, { shapeCenter.SZ }, { 1 }  };
            
            return MatrixMultiplier.MultiplyMatrix(rotateY, shapeCoords);
        }

        public void Rotate(MPoint shapeCenter, double angle)
        {
            //формируем матрицу поворота;
            float[,] rotateZ = {
                { (float)Math.Cos(angle), -(float)Math.Sin(angle), 0, 0 },
                { (float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            //формируем матрицу поворота;
            float[,] rotateX = {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(angle), -(float)Math.Sin(angle), 0 },
                { 0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0 },
                { 0, 0, 0, 1 }
            };

            //формируем матрицу поворота;
            float[,] rotateY = {
                { (float)Math.Cos(angle), 0, (float)Math.Sin(angle), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float dx = shapeCenter.SX;
            float dy = shapeCenter.SY;
            float dz = shapeCenter.SZ;

            //формируем матрицу переноса объекта
            float[,] move = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -dx, -dy, -dz, 1 } };

            //формируем матрицу обратного переноса
            float[,] unmove = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { dx, dy, dz, 1 } };

            ////умножаем матрицы
            //this->MatrixMultiplication(move);
            //this->MatrixMultiplication(rotate);
            //this->MatrixMultiplication(unmove);

            ////применяем к точкам объекта
            //this->box.ReplaceBox(matrix);
            //this->top_box.ReplaceBox(matrix);
            //this->upper_box.ReplaceBox(matrix);
            //this->top_cylinder.ReplaceTopCylinder(matrix);
            //this->top_tor.ReplaceBorder(matrix);
            //this->VolController[0].ReplaceSideCylinder(matrix);
            //this->VolController[1].ReplaceSideCylinder(matrix);
            //this->VolController[2].ReplaceSideCylinder(matrix);
        }
    }
}