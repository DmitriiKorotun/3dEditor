using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBuffer.Shapes
{
    public abstract class MCommonPrimitive : MShape
    {
        public abstract List<MPoint> GetVertices();
    }
}
