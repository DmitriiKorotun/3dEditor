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

        public MTopCylinder(MPoint centerBot, float radius1, float radius2, float height, bool isHasParent) : base(centerBot, radius1, height, isHasParent)
        {
            CalcDotsBot(radius1);
            CalcDotsTop(radius2);

            InitFacets();
        }

        private void CalcDotsBot(float radius)
        {
            double angle, da, x, y, z; // some temp variables
            int i;
            da = 2 * Math.PI / (circleDotsCount - 1);
            for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
            {
                x = radius * Math.Cos(angle);
                y = radius * Math.Sin(angle);

                BottomDots[i] = new MPoint(CenterBot.SX + x, CenterBot.SY + y, CenterBot.SZ);
            }
        }

        private void CalcDotsTop(float radius)
        {
            double angle, da, x, y, z; // some temp variables
            int i;
            da = 2 * Math.PI / (circleDotsCount - 1);
            for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
            {
                x = radius * Math.Cos(angle);
                y = radius * Math.Sin(angle);

                TopDots[i] = new MPoint(CenterTop.SX + x, CenterTop.SY + y, CenterTop.SZ);
            }
        }
    }
}
