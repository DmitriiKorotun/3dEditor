using EmuEngine.EmuMath.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace EmuEngine.Shapes
{
    [DataContract]
    public abstract class MShape
    {
        public Matrix4 ModelMatrix { get; set; }
        public Quaternion RotationQuaternion { get; set; }

        public MShape()
        {
            ModelMatrix = new Matrix4(new float[,] {
                {1, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 0, 1, 0 },
                {0, 0, 0, 1 }
            });

            RotationQuaternion = new Quaternion(0, 0, 0, 1);
        }

        public MShape(float x, float y, float z)
        {
            ModelMatrix = new Matrix4(new float[,] {
                {1, 0, 0, x },
                {0, 1, 0, y },
                {0, 0, 1, z },
                {0, 0, 0, 1 }
            });

            RotationQuaternion = new Quaternion(0, 0, 0, 1);
        }

        public abstract List<MPoint> GetVertices();
        public abstract List<MPoint> GetAllPoints();
    }
}
