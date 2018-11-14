using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;

namespace EmuEngine.Tools
{
    abstract public class Camera
    {
        protected float[,] projectionMatrix;

        public float[,] ProjectionMatrix { get { return projectionMatrix; } }
        public float[,] ViewMatrix { get; set; }

        public Camera()
        {
            ViewMatrix = new float[,] {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, -60 },
                { 0, 0, 0, 1 }
            };
        }
    }
}
