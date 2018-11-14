using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.EmuMath
{
    public static class MatrixComparator
    {
        public static bool IsMatrixesEqual(float[,] matrixA, float[,] matrixB)
        {
            bool isMatrixesEqual = true;

            if (matrixA.Rank == matrixB.Rank && matrixB.Rank == 2 &&
                matrixA.GetLength(0) == matrixB.GetLength(0) &&
                matrixA.GetLength(1) == matrixB.GetLength(1))
            {
                for (int i = 0; i < matrixA.GetLength(0); ++i)
                {
                    for (int j = 0; j < matrixA.GetLength(1); ++j)
                    {
                        if (matrixA[i, j] != matrixB[i, j])
                            isMatrixesEqual = false;
                    }
                }
            }
            else
                isMatrixesEqual = false;

            return isMatrixesEqual;
        }
    }
}
