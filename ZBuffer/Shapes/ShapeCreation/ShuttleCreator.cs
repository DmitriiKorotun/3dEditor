using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes.ComplexShapes;

namespace EmuEngine.Shapes.ShapeCreation
{
    public class ShuttleCreator : ShapeCreator
    {
        public override MShape CreateShape()
        {
            return new Shuttle();
        }
    }
}
