using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.EmuMath.Structures
{
    public abstract class MatrixBase : ICloneable, IEqualityComparer
    {
        public (int Rows, int Columns) Size { get; protected set; }

        protected float[,] elements;

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

        public abstract object Clone();
        public abstract new bool Equals(object x, object y);
        public abstract int GetHashCode(object obj);
    }
}