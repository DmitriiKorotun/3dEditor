using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Affine_Transformation
{
    public enum MatrixID
    {
        ROTATEX,
        ROTATEY,
        ROTATEZ
    }

    public class MatrixCatalogue
    {
        public float[,] GetMatrix(MatrixID id, double angle)
        {
            float[,] matrix;

            switch (id)
            {
                case MatrixID.ROTATEX:
                    matrix = new float[4, 4]{
                        { 1, 0, 0, 0 },
                        { 0, (float)Math.Cos(angle), -(float)Math.Sin(angle), 0 },
                        { 0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0 },
                        { 0, 0, 0, 1 }
                    };
                    break;
                default:
                    matrix = null;
                    break;
            }

            return matrix;
        }
    }
}
