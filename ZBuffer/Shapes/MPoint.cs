using EmuEngine.EmuMath.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    [DataContract]
    public class MPoint
    {
        [DataMember]
        public Vector4 Current { get; set; }  //текущий х
        [DataMember]
        public Vector4 Source { get; set; }  //текущий х

        public int ARGB { get; set; } 

        public MPoint(float x, float y, float z)
        {
            Source = new Vector4(x, y, z, 1);

            Current = (Vector4)Source.Clone();

            ARGB = 0;
        }

        public MPoint(double x, double y, double z)
        {
            Source = new Vector4((float)x, (float)y, (float)z, 1);

            Current = (Vector4)Source.Clone();

            ARGB = 0;
        }

        public MPoint(float x, float y, float z, float w)
        {
            Source = new Vector4(x, y, z, w);

            Current = (Vector4)Source.Clone();

            ARGB = 0;
        }

        public MPoint(double x, double y, double z, double w)
        {
            Source = new Vector4((float)x, (float)y, (float)z, (float)w);

            Current = (Vector4)Source.Clone();

            ARGB = 0;
        }
    }
}
