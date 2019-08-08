using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public class MTopCylinder : MCylinder
    {
        public float TopRadius { get; set; }
        public float BotRadius { get; set; }

        //TRY TO REWORK DUPLICATION OF CODE
        public MTopCylinder(MPoint centerBot, float botRadius, float topRadius, float heigth) : base(centerBot, heigth)
        {
            BotRadius = botRadius;
            TopRadius = topRadius;

            CalcDots();

            InitFacets();
        }

        protected override void CalcDots()
        {
            {
                double angle, da, topX, topY, botX, botY; // some temp variables
                int i;
                da = 2 * Math.PI / (circleDotsCount - 1);
                for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
                {
                    topX = TopRadius * Math.Cos(angle);
                    topY = TopRadius * Math.Sin(angle);

                    botX = BotRadius * Math.Cos(angle);
                    botY = BotRadius * Math.Sin(angle);

                    TopDots[i] = new MPoint(CenterTop.Source.X + topX, CenterTop.Source.X + topY, CenterTop.Source.Z);
                    BottomDots[i] = new MPoint(CenterBot.Source.X + botX, CenterBot.Source.Y + botY, CenterBot.Source.Z);
                }
            }
        }

        //private void CalcDotsBot(float radius)
        //{
        //    double angle, da, x, y, z; // some temp variables
        //    int i;
        //    da = 2 * Math.PI / (circleDotsCount - 1);
        //    for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
        //    {
        //        x = radius * Math.Cos(angle);
        //        y = radius * Math.Sin(angle);

        //        BottomDots[i] = new MPoint(CenterBot.Source.X + x, CenterBot.Source.Y + y, CenterBot.Source.Z);
        //    }
        //}

        //private void CalcDotsTop(float radius)
        //{
        //    double angle, da, x, y, z; // some temp variables
        //    int i;
        //    da = 2 * Math.PI / (circleDotsCount - 1);
        //    for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
        //    {
        //        x = radius * Math.Cos(angle);
        //        y = radius * Math.Sin(angle);

        //        TopDots[i] = new MPoint(CenterTop.Source.X + x, CenterTop.Source.Y + y, CenterTop.Source.Z);
        //    }
        //}
    }
}
