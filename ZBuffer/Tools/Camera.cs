using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;
using EmuEngine.EmuMath;

namespace EmuEngine.Tools
{
    abstract public class Camera
    {
        public Matrix4 ViewMatrix { get; set; }
        public Matrix4 ProjectionMatrix { get { return projectionMatrix; } }

        protected Matrix4 projectionMatrix;
      
        private const int defaultZoom = -60;

       
        public Camera()
        {
            ViewMatrix = new Matrix4();
            ViewMatrix[2, 3] = defaultZoom;
        }
    }
}
