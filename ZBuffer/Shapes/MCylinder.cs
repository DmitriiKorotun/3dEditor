using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public abstract class MCylinder : MCommonPrimitive
    {
        public MPoint CenterBot { get; private set; }
        public MPoint CenterTop { get; private set; }
        protected MPoint[] BottomDots { get; set; }
        protected MPoint[] TopDots { get; set; }
        protected MFacet[] SideFacets { get; set; }
        protected MFacet[] CircleFacets { get; set; }

        protected const int circleDotsCount = 32;
        protected const int facetsCount = 64;


        protected abstract void CalcDots();


        public MCylinder(MPoint centerBot, float height) : base(centerBot.Source.X, centerBot.Source.Y, centerBot.Source.Z)
        {
            CenterBot = new MPoint(centerBot.Source.X, centerBot.Source.Y, centerBot.Source.Z);

            CenterTop = new MPoint(centerBot.Source.X, centerBot.Source.Y, centerBot.Source.Z + height);
           
            if (height < 0 || height > float.MaxValue)
                throw new ArgumentOutOfRangeException("Height of cylinder can't be equal or less than 0 or more than float maxValue");

            Height = height;

            InitArrays();
        }

        private void InitArrays()
        {
            BottomDots = new MPoint[circleDotsCount];
            TopDots = new MPoint[circleDotsCount];
            SideFacets = new MFacet[facetsCount];
            CircleFacets = new MFacet[facetsCount];
        }

        protected void InitFacets()
        {
            InitCircleFacets();
            InitSideFacets();
        }

        private void InitCircleFacets()
        {
            for (int i = 0; i < BottomDots.Length - 1; ++i)
            {
                CircleFacets[i] = new MFacet(CenterBot, BottomDots[i], BottomDots[i + 1]);
            }

            CircleFacets[31] = new MFacet(CenterBot, BottomDots[31], BottomDots[0]);

            for (int i = 0; i < TopDots.Length - 1; ++i)
            {
                CircleFacets[i + 32] = new MFacet(CenterTop, TopDots[i], TopDots[i + 1]);
            }

            CircleFacets[63] = new MFacet(CenterTop, TopDots[31], TopDots[0]);
        }

        private void InitSideFacets()
        {
            for (int i = 0; i < BottomDots.Length - 1; ++i)
            {
                SideFacets[i] = new MFacet(TopDots[i], BottomDots[i], BottomDots[i + 1]);
            }

            SideFacets[31] = new MFacet(TopDots[31], BottomDots[31], BottomDots[0]);

            for (int i = 0; i < TopDots.Length - 1; ++i)
            {
                SideFacets[i + 32] = new MFacet(TopDots[i], TopDots[i + 1], BottomDots[i + 1]);
            }

            SideFacets[63] = new MFacet(BottomDots[0], TopDots[31], TopDots[0]);
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            for (int i = 0; i < CircleFacets.Length; ++i)
                points.AddRange(CircleFacets[i].GetAllPoints());

            for (int i = 0; i < SideFacets.Length; ++i)
                points.AddRange(SideFacets[i].GetAllPoints());

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            var vertices = new List<MPoint>(BottomDots);
            vertices.AddRange(TopDots.ToList());
            vertices.Add(CenterTop);
            vertices.Add(CenterBot);
            return vertices;
        }
    }
}