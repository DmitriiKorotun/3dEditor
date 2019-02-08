using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.EmuMath.Structures;

namespace EmuEngine.EmuMath.Structures
{
    public class Matrix4 : ICloneable, IEqualityComparer
    {
        public (int Rows, int Columns) Size { get; private set; }

        private float[,] elements;

        public Matrix4()
        {
            elements = new float[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            Size = (Rows: elements.GetLength(0), Columns: elements.GetLength(1));
        }

        public Matrix4(Matrix4 matrix)
        {
            if (matrix.Size.Rows != 4 || matrix.Size.Columns != 4)
                throw new ArgumentException("An attempt to create matrix4 with innapropriate number of columns or rows");

            int rows = matrix.Size.Rows;
            int cols = matrix.Size.Columns;

            elements = new float[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    elements[i, j] = matrix[i, j];

            Size = matrix.Size;
        }

        public Matrix4(float[,] elements)
        {
            this.elements = elements;
            Size = (Rows: elements.GetLength(0), Columns: elements.GetLength(1));
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

        public static Matrix4 operator *(Matrix4 m0, Matrix4 m1)
        {
            if (m0.Size.Columns != m1.Size.Rows)
                throw new ArgumentException("Wrong second operand size");

            int m0Rows = m0.Size.Rows;
            int m1Rows = m1.Size.Rows;
            int m1Cols = m1.Size.Columns;

            float[,] result = new float[m0Rows, m1Cols];

            for (int i = 0; i < m0Rows; i++)
                for (int j = 0; j < m1Cols; j++)
                    for (int k = 0; k < m1Rows; k++)
                        result[i, j] += m0[i, k] * m1[k, j];

            return new Matrix4(result);
        }

        public static Matrix4 operator *(Matrix4 m0, Vector4 v0)
        {
            var vecDigits = new float[,] { { v0.X }, { v0.Y }, { v0.Z }, { v0.W } };
            var m1 = new Matrix4(vecDigits);

            if (m0.Size.Columns != m1.Size.Rows)
                throw new ArgumentException("Wrong second operand size");

            int m0Rows = m0.Size.Rows;
            int m1Rows = m1.Size.Rows;
            int m1Cols = m1.Size.Columns;

            float[,] result = new float[m0Rows, m1Cols];

            for (int i = 0; i < m0Rows; i++)
                for (int j = 0; j < m1Cols; j++)
                    for (int k = 0; k < m1Rows; k++)
                        result[i, j] += m0[i, k] * m1[k, j];

            return new Matrix4(result);
        }

        public static bool operator ==(Matrix4 m0, Matrix4 m1)
        {
            bool isEqual = true;

            if (m0.Size.Rows != m1.Size.Rows || m0.Size.Columns != m1.Size.Columns)
                isEqual = false;
            else
            {
                for (int i = 0; i < m0.Size.Rows; ++i)
                {
                    for (int j = 0; j < m0.Size.Columns; ++j)
                    {
                        if (m0[i, j] != m1[i, j])
                        {
                            isEqual = false;
                            break;
                        }
                    }

                    if (!isEqual)
                        break;
                }
            }

            return isEqual;
        }

        public static bool operator !=(Matrix4 m0, Matrix4 m1)
        {
            bool isNotEqual = false;

            if (m0.Size.Rows != m1.Size.Rows || m0.Size.Columns != m1.Size.Columns)
                isNotEqual = true;
            else
            {
                for (int i = 0; i < m0.Size.Rows; ++i)
                {
                    for (int j = 0; j < m0.Size.Columns; ++j)
                    {
                        if (m0[i, j] != m1[i, j])
                        {
                            isNotEqual = true;
                            break;
                        }
                    }

                    if (isNotEqual)
                        break;
                }
            }

            return isNotEqual;
        }


        public Matrix4 Multiply(Matrix4 matrix)
        {
            if (Size.Columns != matrix.Size.Rows)
                throw new ArgumentException("Wrong second operand size");

            int m1Rows = Size.Rows;
            int m2Rows = matrix.Size.Rows;
            int m2Cols = matrix.Size.Columns;

            float[,] r = new float[m1Rows, m2Cols];

            for (int i = 0; i < m1Rows; i++)
                for (int j = 0; j < m2Cols; j++)
                    for (int k = 0; k < m2Rows; k++)
                        r[i, j] += elements[i, k] * matrix[k, j];

            return new Matrix4(r);
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

        public double[] ToArrayOfDoubles()
        {
            int index = 0;
            int width = elements.GetLength(0);
            int height = elements.GetLength(1);
            double[] single = new double[width * height];
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

        public object Clone()
        {
            return new Matrix4(this);
        }

        public new bool Equals(object x, object y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
