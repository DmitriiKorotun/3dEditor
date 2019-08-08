using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public class MTopCylinder : MCylinder
    {
        public float TopRadius { get; private set; }
        public float BotRadius { get; private set; }

        //TRY TO REWORK DUPLICATION OF CODE
        public MTopCylinder(MPoint centerBot, float botRadius, float topRadius, float heigth) : base(centerBot, heigth)
        {
            if (botRadius < 0 || botRadius > float.MaxValue)
                throw new ArgumentOutOfRangeException("bot radius can't be less than 0 or more than float maxValue");

            if (topRadius < 0 || topRadius > float.MaxValue)
                throw new ArgumentOutOfRangeException("top radius can't be less than 0 or more than float maxValue");

            if (topRadius == 0 && botRadius == 0)
                throw new ArgumentOutOfRangeException("both top and bot radius can't be 0 at the same time");

            BotRadius = botRadius;
            TopRadius = topRadius;

            CalcDots();

            InitFacets();
        }

        protected override void CalcDots()
        {
            double angle, da, topX, topY, botX, botY;

            int i;
            da = 2 * Math.PI / (circleDotsCount - 1);

            for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
            {
                topX = TopRadius * Math.Cos(angle);
                topY = TopRadius * Math.Sin(angle);

                botX = BotRadius * Math.Cos(angle);
                botY = BotRadius * Math.Sin(angle);

                TopDots[i] = new MPoint(CenterTop.Source.X + topX, CenterTop.Source.Y + topY, CenterTop.Source.Z);
                BottomDots[i] = new MPoint(CenterBot.Source.X + botX, CenterBot.Source.Y + botY, CenterBot.Source.Z);
            }
        }
    }
}