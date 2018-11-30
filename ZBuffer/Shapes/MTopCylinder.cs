using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public class MTopCylinder : MCylinder
    {
        public MTopCylinder(MPoint centerBot, float radius1, float radius2, float height) : base(centerBot, radius1, height)
        {
            CalcDotsBot(radius1);
            CalcDotsTop(radius2);

            InitFacets();
        }

        private void CalcDotsBot(float radius)
        {
            double angle, da, x, y, z; // some temp variables
            int i;
            z = Height * 0.5; // half height of cyliner
            da = 2 * Math.PI / (circleDotsCount - 1);
            for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
            {
                x = radius * Math.Cos(angle);
                y = radius * Math.Sin(angle);

                BottomDots[i] = new MPoint(x, y, -z);
            }
        }

        private void CalcDotsTop(float radius)
        {
            double angle, da, x, y, z; // some temp variables
            int i;
            z = Height * 0.5; // half height of cyliner
            da = 2 * Math.PI / (circleDotsCount - 1);
            for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
            {
                x = radius * Math.Cos(angle);
                y = radius * Math.Sin(angle);

                TopDots[i] = new MPoint(x, y, +z);
            }
        }
    }
}
