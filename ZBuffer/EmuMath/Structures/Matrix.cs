using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.EmuMath.Structures
{
    public class Matrix : MatrixBase
    {
        protected Matrix() { }

        public Matrix(Matrix matrix)
        {
            int rows = matrix.Size.Rows;
            int cols = matrix.Size.Columns;

            elements = new float[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    elements[i, j] = matrix[i, j];

            Size = matrix.Size;
        }

        public Matrix(float[,] elements)
        {
            this.elements = elements;
            Size = (Rows: elements.GetLength(0), Columns: elements.GetLength(1));
        }

        public static Matrix operator *(Matrix m0, Matrix m1)
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

            return new Matrix(result);
        }

        public static Matrix operator *(Matrix m0, Vector4 v0)
        {
            var vecDigits = new float[,] { { v0.X }, { v0.Y }, { v0.Z }, { v0.W } };
            var m1 = new Matrix(vecDigits);

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

            return new Matrix(result);
        }

        public static bool operator ==(Matrix m0, Matrix m1)
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

        public static bool operator !=(Matrix m0, Matrix m1)
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

        public Matrix Multiply(Matrix matrix)
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

            return new Matrix(r);
        }

        public override object Clone()
        {
            return new Matrix(this);
        }

        public override bool Equals(object x, object y)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
