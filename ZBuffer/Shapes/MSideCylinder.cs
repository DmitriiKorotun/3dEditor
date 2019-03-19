using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public class MSideCylinder : MCylinder
    {
        public float Radius { get; set; }

        //TRY TO REWORK DUPLICATION OF CODE
        public MSideCylinder(MPoint centerBot, float radius, float heigth) : base(centerBot, heigth)
        {
            if (radius > 0 && radius <= float.MaxValue)
                Radius = radius;
            else
                throw new ArgumentOutOfRangeException("Radius of cylinder can't be less than 0 or more than maxValue");

            CalcDots();

            InitFacets();
        }

        protected override void CalcDots()
        {
            {
                double angle, da, x, y; // some temp variables
                int i;
                da = 2 * Math.PI / (circleDotsCount - 1);
                for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
                {
                    x = Radius * Math.Cos(angle);
                    y = Radius * Math.Sin(angle);

                    TopDots[i] = new MPoint(CenterTop.Source.X + x, CenterTop.Source.X + y, CenterTop.Source.Z);
                    BottomDots[i] = new MPoint(CenterBot.Source.X + x, CenterBot.Source.Y + y, CenterBot.Source.Z);
                }
            }
        }
    }
}
