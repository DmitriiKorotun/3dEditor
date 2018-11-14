using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.EmuMath
{
    public static class MatrixMultiplier
    {
        public static float[,] MultiplyMatrix(float[,] a, float[,] b)
        {
            float[,] c = null;

            if (a.GetLength(1) == b.GetLength(0))
            {
                c = new float[a.GetLength(0), b.GetLength(1)];
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    for (int j = 0; j < c.GetLength(1); j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < a.GetLength(1); k++) // OR k<b.GetLength(0)
                            c[i, j] = c[i, j] + a[i, k] * b[k, j];
                    }
                }
            }
            else
            {
                Console.WriteLine("\n Number of columns in First Matrix should be equal to Number of rows in Second Matrix.");
                Console.WriteLine("\n Please re-enter correct dimensions.");
                throw new ArithmeticException("Number of columns in First Matrix should be equal to Number of rows in Second Matrix");
            }

            return c;
        }
    }
}
