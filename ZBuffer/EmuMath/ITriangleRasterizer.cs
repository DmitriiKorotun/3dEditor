using EmuEngine.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBuffer.EmuMath
{
    interface ITriangleRasterizer
    {
        List<MPoint> RasterizePolygon(MFacet polygon);
    }
}
