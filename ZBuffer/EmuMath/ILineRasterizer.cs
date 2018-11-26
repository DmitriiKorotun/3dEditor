using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;

namespace EmuEngine.EmuMath
{
    interface ILineRasterizer
    {
        List<MPoint> GetLine(MPoint point1, MPoint point2);
    }
}
