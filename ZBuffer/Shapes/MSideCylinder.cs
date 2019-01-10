using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public class MSideCylinder : MCylinder
    {
        public MSideCylinder(MPoint centerBot, float radius, float heigth) : base(centerBot, radius, heigth)
        {
        }

        public MSideCylinder(MPoint centerBot, float radius, float heigth, bool isHasParent) : base(centerBot, radius, heigth, isHasParent)
        {
        }

        public MSideCylinder(MPoint centerBot, float radius, float heigth, float sx, float sy) : base(centerBot, radius, heigth, sx, sy)
        {
        }

    }
}
