using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public abstract class MCylinder : MCommonPrimitive
    {
        protected MPoint CenterBot { get; set; }  //центр нижнего основания
        protected MPoint CenterTop { get; set; }  //центр верхнего основания
        protected float Radius { get; set; }  //радиус
        protected float Height { get; set; }  //высота
        protected const int circleDotsCount = 32;
        protected const int facetsCount = 64;
        protected MPoint[] BottomDots { get; set; } //точки нижней окружности 36
        protected MPoint[] TopDots { get; set; }  //точки верхней окружности 36
        protected MFacet[] Facets { get; set; }  //боковые грани 72
        protected MFacet[] CircleFacets { get; set; }  //грани оснований 72

        public MCylinder(MPoint centerBot, float radius, float height) : base(centerBot.X, centerBot.Y, centerBot.Z + height / 2)
        {
            CenterBot = new MPoint(0, 0, -height / 2);

            CenterTop = new MPoint(CenterBot.X, CenterBot.Y, height / 2);

            Radius = radius;

            Height = height;

            InitArrays();

            CalcDots();

            InitFacets();
        }

        private void CalcDots()
        {
            double angle, da, x, y, z; // some temp variables
            int i;
            z = Height * 0.5; // half height of cyliner
            da = 2 * Math.PI / (circleDotsCount - 1);
            for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
            {
                x = Radius * Math.Cos(angle);
                y = Radius * Math.Sin(angle);

                TopDots[i] = new MPoint(x, y, +z);
                BottomDots[i] = new MPoint(x, y, -z);
            }
        }

        private void InitArrays()
        {
            BottomDots = new MPoint[circleDotsCount];
            TopDots = new MPoint[circleDotsCount];
            Facets = new MFacet[facetsCount];
            CircleFacets = new MFacet[facetsCount];
        }

        protected void InitFacets()
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



            for (int i = 0; i < BottomDots.Length - 1; ++i)
            {
                Facets[i] = new MFacet(TopDots[i], BottomDots[i], BottomDots[i + 1]);
            }

            Facets[31] = new MFacet(TopDots[31], BottomDots[31], BottomDots[0]);

            for (int i = 0; i < TopDots.Length - 1; ++i)
            {
                Facets[i + 32] = new MFacet(BottomDots[i + 1], TopDots[i], TopDots[i + 1]);
            }

            Facets[63] = new MFacet(BottomDots[0], TopDots[31], TopDots[0]);
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            for (int i = 0; i < CircleFacets.Length; ++i)
                points.AddRange(CircleFacets[i].GetAllPoints());

            for (int i = 0; i < Facets.Length; ++i)
                points.AddRange(Facets[i].GetAllPoints());

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
