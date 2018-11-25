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
        public float[,] ViewMatrix { get; set; }
        public float[,] ProjectionMatrix { get { return projectionMatrix; } }

        protected float[,] projectionMatrix;
      
        private const int defaultZoom = -60;

       
        public Camera()
        {
            ViewMatrix = new float[,] {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, defaultZoom },
                { 0, 0, 0, 1 }
            };
        }
    }
}
