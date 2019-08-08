using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes.ShapeCreation
{
    public abstract class ShapeCreator
    {
        abstract public MShape CreateShape();
    }
}
