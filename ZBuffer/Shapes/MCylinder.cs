using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public abstract class MCylinder : MCommonPrimitive
    {
        //MPoint CenterBot { get; set; }  //центр нижнего основания
        //MPoint CenterTop { get; set; }  //центр верхнего основания
        //float Radius { get; set; }  //радиус
        //float Heigth { get; set; }  //высота
        //MPoint[] BottomDots { get; set; } //точки нижней окружности 36
        //MPoint[] TopDots { get; set; }  //точки верхней окружности 36
        //MFacet[] Facets { get; set; }  //боковые грани 72
        //MFacet[] CircleFacets { get; set; }  //грани оснований 72

        //public MCylinder(MPoint centerBot, float radius, float heigth)
        //{
        //    CenterBot = centerBot;

        //    Radius = radius;

        //    Heigth = heigth;
        //}

        //private MPoint[] CalcDots(MPoint center)
        //{
        //    return new MPoint[36];
        //}



        public MPoint CenterBot { get; set; }  //центр нижнего основания
        public MPoint CenterTop { get; set; }  //центр верхнего основания
        protected const int circleDotsCount = 32;
        protected const int facetsCount = 64;
        protected MPoint[] BottomDots { get; set; } //точки нижней окружности 36
        protected MPoint[] TopDots { get; set; }  //точки верхней окружности 36
        protected MFacet[] Facets { get; set; }  //боковые грани 72
        protected MFacet[] CircleFacets { get; set; }  //грани оснований 72


        public MCylinder(MPoint centerBot, float height) : base(centerBot.Source.X, centerBot.Source.Y, centerBot.Source.Z)
        {
            CenterBot = new MPoint(centerBot.Source.X, centerBot.Source.Y, centerBot.Source.Z);

            CenterTop = new MPoint(centerBot.Source.X, centerBot.Source.Y, centerBot.Source.Z + height);
           
            Height = height;

            InitArrays();
        }

        //public void SetRadius(float radius)
        //{
        //    Radius = radius;

        //    InitArrays();

        //    CalcDots();

        //    InitFacets();
        //}

        protected abstract void CalcDots();

        //private void CalcDotsNice()
        //{
        //    double angle, da, x, y, z; // some temp variables
        //    int i;
        //    da = 2 * Math.PI / (circleDotsCount - 1);
        //    for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
        //    {
        //        x = Radius * Math.Cos(angle);
        //        z = Radius * Math.Sin(angle);
        //        y = 0;

        //        TopDots[i] = new MPoint(CenterTop.SX + x, CenterTop.SY + y, CenterTop.SZ + z);
        //        BottomDots[i] = new MPoint(CenterBot.SX + x, CenterBot.SY + y, CenterBot.SZ + z);
        //    }
        //}

        //private void CalcDotsKek()
        //{
        //    double angle, da, x, y, z; // some temp variables
        //    int i;
        //    da = 2 * Math.PI / (circleDotsCount - 1);
        //    for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
        //    {
        //        x = 0;
        //        z = Radius * Math.Sin(angle);
        //        y = Radius * Math.Cos(angle);

        //        TopDots[i] = new MPoint(CenterTop.SX + x, CenterTop.SY + y, CenterTop.SZ + z);
        //        BottomDots[i] = new MPoint(CenterBot.SX + x, CenterBot.SY + y, CenterBot.SZ + z);
        //    }
        //}

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
                Facets[i + 32] = new MFacet(TopDots[i], TopDots[i + 1], BottomDots[i + 1]);
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

        //public override List<MFacet> GetAllFacets()
        //{
        //    var facets = new List<MFacet>(facetsCount * 2);
        //    facets.AddRange(CircleFacets.ToList());
        //    facets.AddRange(Facets.ToList());
        //    return facets;
        //}

        //private void RecalculateVertices(float sx, float sy, MPoint center)
        //{
        //    double rads = sx * Math.PI / 180.0;

        //    float[,] rotateX = {
        //        { 1, 0, 0, 0 },
        //        { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
        //        { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
        //        { 0, 0, 0, 1 }
        //    };

        //    float[,] rotateY = {
        //        { (float)Math.Cos(rads), 0, (float)Math.Sin(rads), 0 },
        //        { 0, 1, 0, 0 },
        //        { -(float)Math.Sin(rads), 0, (float)Math.Cos(rads), 0 },
        //        { 0, 0, 0, 1 }
        //    };


        //    var vertices = new List<MPoint>(BottomDots);
        //    vertices.AddRange(TopDots.ToList());
        //    vertices.Add(CenterTop);
        //    vertices.Add(CenterBot);

        //    for (int i = 0; i < vertices.Count; ++i)
        //    {
        //        var coords = new float[4, 1] { { vertices[i].SX }, { vertices[i].SY }, { vertices[i].SZ }, { vertices[i].SW } };

        //        if (sx != 0)
        //            this.ModelMatrix = MatrixMultiplier.MultiplyMatrix(this.ModelMatrix, rotateX);
        //        else
        //            this.ModelMatrix = MatrixMultiplier.MultiplyMatrix(this.ModelMatrix, rotateY);

        //        coords = MatrixMultiplier.MultiplyMatrix(this.ModelMatrix, coords);

        //        vertices[i] = new MPoint(coords[0, 0] + center.SX, coords[1, 0] + center.SY, coords[2, 0] + center.SZ);
        //    }

        //    //InitFacets();
        //}
    }
}
