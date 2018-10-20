using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBuffer.Shapes
{
    public abstract class MCommonPrimitive : MShape
    {
        public float Height { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }

        public abstract List<MPoint> GetVertices();
        public abstract MPoint GetCenterPoint();
    }
}
