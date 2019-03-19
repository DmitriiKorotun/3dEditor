using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes.ComplexShapes
{
    //TO IMPLEMENT
    class Shuttle : MComplex
    {
        public Shuttle()
        {
            var testBox = new MBox(new MPoint(-75, 0, 0), 50, 50, 50);
            var testBox2 = new MBox(new MPoint(75, 20, 0), 50, 50, 50);

            this.AddPrimitive(testBox);
            this.AddPrimitive(testBox2);
        }
    }
}
