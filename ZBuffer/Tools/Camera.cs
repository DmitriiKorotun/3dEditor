using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.Shapes;

namespace ZBuffer.Tools
{
    abstract public class Camera
    {
        protected float[,] projectionMatrix;

        public float[,] ProjectionMatrix { get { return projectionMatrix; } }
    }
}
