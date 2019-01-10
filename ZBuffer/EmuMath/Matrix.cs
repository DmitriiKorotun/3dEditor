using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.EmuMath
{
    public class Matrix
    {
        private float[,] elements;

        // Единичная матрица 4x4
        public Matrix()
        {
            elements = new float[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            Size = new Tuple<int, int>(elements.GetLength(0), elements.GetLength(1));
        }

        public Matrix(Matrix matrix)
        {
            int rows = matrix.Size.Item1;
            int cols = matrix.Size.Item2;

            elements = new float[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    elements[i, j] = matrix[i, j];

            Size = matrix.Size;
        }

        public Matrix(float[,] elements)
        {
            this.elements = elements;
            Size = new Tuple<int, int>(elements.GetLength(0), elements.GetLength(1));
        }

        public float this[int i1, int i2]
        {
            get
            {
                return elements[i1, i2];
            }

            set
            {
                elements[i1, i2] = value;
            }
        }

        public Tuple<int, int> Size { get; private set; }

        public Matrix Multiply(Matrix matrix)
        {
            if (Size.Item2 != matrix.Size.Item1)
                throw new ArgumentException("Wrong second operand size");

            int m1Rows = Size.Item1;
            int m2Rows = matrix.Size.Item1;
            int m2Cols = matrix.Size.Item2;

            float[,] r = new float[m1Rows, m2Cols];

            for (int i = 0; i < m1Rows; i++)
                for (int j = 0; j < m2Cols; j++)
                    for (int k = 0; k < m2Rows; k++)
                        r[i, j] += elements[i, k] * matrix[k, j];

            return new Matrix(r);
        }

        public float[] ToArray()
        {
            int index = 0;
            int width = elements.GetLength(0);
            int height = elements.GetLength(1);
            float[] single = new float[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    single[index] = elements[x, y];
                    index++;
                }
            }
            return single;
        }
    }
}