using EmuEngine.Affine_Transformation;
using EmuEngine.EmuMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public abstract class MCylinder : MCommonPrimitive
    {
        public MPoint CenterBot { get; set; }  //центр нижнего основания
        public MPoint CenterTop { get; set; }  //центр верхнего основания
        protected float Radius { get; set; }  //радиус
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


        public MCylinder(MPoint centerBot, float radius, float height, bool isHasParent) : base()
        {
            CenterBot = new MPoint(centerBot.X, centerBot.Y, centerBot.Z);

            CenterTop = new MPoint(CenterBot.X, CenterBot.Y, centerBot.Z + height);

            Radius = radius;

            Height = height;

            InitArrays();

            CalcDots();

            InitFacets();
        }

        public MCylinder(MPoint centerBot, float radius, float height, float sx, float sy) : base(centerBot.X, centerBot.Y, centerBot.Z + height / 2)
        {
            double rads = sx != 0 ? sx * Math.PI / 180.0 : sy * Math.PI / 180.0;

            float[,] rotation = null;

            if (sx != 0)
                rotation = new float[,]{
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
                { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };
            else
                rotation = new float[,]{
                { (float)Math.Cos(rads), 0, (float)Math.Sin(rads), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(rads), 0, (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            var coordsBot = new float[4, 1] { {  0 }, { 0 }, { - height / 2 }, { centerBot.SW } };

            //var coordsBot = new float[4, 1] { { centerBot.SX }, { centerBot.SY }, { centerBot.SZ }, { centerBot.SW } };

            coordsBot = MatrixMultiplier.MultiplyMatrix(rotation, coordsBot);

            var coordsTop = new float[4, 1] { { 0 }, { 0 }, { height / 2 }, { centerBot.SW } };

            //var coordsTop = new float[4, 1] { { centerBot.SX }, { centerBot.SY }, { centerBot.SZ + height }, { centerBot.SW } };

            coordsTop = MatrixMultiplier.MultiplyMatrix(rotation, coordsTop);

            coordsBot[0, 0] += centerBot.SX;
            coordsBot[1, 0] += centerBot.SY;
            coordsBot[2, 0] += centerBot.SZ;

            coordsTop[0, 0] += centerBot.SX;
            coordsTop[1, 0] += centerBot.SY;
            coordsTop[2, 0] += centerBot.SZ;




            CenterBot = new MPoint(coordsBot[0, 0], coordsBot[1, 0], coordsBot[2, 0]);

            CenterTop = new MPoint(coordsTop[0, 0], coordsTop[1, 0], coordsTop[2, 0]);

            //CenterBot = new MPoint(0, 0, -height / 2);

            //CenterTop = new MPoint(CenterBot.X, CenterBot.Y, height / 2);

            Radius = radius;

            Height = height;

            InitArrays();

            if (sx != 0)
                CalcDotsNice();
            else if (sy != 0)
                CalcDotsKek();
            else
                CalcDots();

            //RecalculateVertices(sx, sy, centerBot);

            InitFacets();
        }

        public float GetRadius()
        {
            return Radius;
        }

        public void SetRadius(float radius)
        {
            Radius = radius;

            InitArrays();

            CalcDots();

            InitFacets();
        }

        private void CalcDots()
        {
            double angle, da, x, y, z; // some temp variables
            int i;
            da = 2 * Math.PI / (circleDotsCount - 1);
            for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
            {
                x = Radius * Math.Cos(angle);
                y = Radius * Math.Sin(angle);


                TopDots[i] = new MPoint(CenterBot.SX + x, CenterBot.SY + y, CenterTop.SZ);
                BottomDots[i] = new MPoint(CenterBot.SX + x, CenterBot.SY + y, CenterBot.SZ);
            }
        }

        private void CalcDotsNice()
        {
            double angle, da, x, y, z; // some temp variables
            int i;
            da = 2 * Math.PI / (circleDotsCount - 1);
            for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
            {
                x = Radius * Math.Cos(angle);
                z = Radius * Math.Sin(angle);
                y = 0;

                TopDots[i] = new MPoint(CenterTop.SX + x, CenterTop.SY + y, CenterTop.SZ + z);
                BottomDots[i] = new MPoint(CenterBot.SX + x, CenterBot.SY + y, CenterBot.SZ + z);
            }
        }

        private void CalcDotsKek()
        {
            double angle, da, x, y, z; // some temp variables
            int i;
            da = 2 * Math.PI / (circleDotsCount - 1);
            for (angle = 0.0, i = 0; i < circleDotsCount; i++, angle += da)
            {
                x = 0;
                z = Radius * Math.Sin(angle);
                y = Radius * Math.Cos(angle);

                TopDots[i] = new MPoint(CenterTop.SX + x, CenterTop.SY + y, CenterTop.SZ + z);
                BottomDots[i] = new MPoint(CenterBot.SX + x, CenterBot.SY + y, CenterBot.SZ + z);
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

        public override List<MFacet> GetAllFacets()
        {
            var facets = new List<MFacet>(facetsCount * 2);
            facets.AddRange(CircleFacets.ToList());
            facets.AddRange(Facets.ToList());
            return facets;
        }

        private void RecalculateVertices(float sx, float sy, MPoint center)
        {
            double rads = sx * Math.PI / 180.0;

            float[,] rotateX = {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
                { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            float[,] rotateY = {
                { (float)Math.Cos(rads), 0, (float)Math.Sin(rads), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(rads), 0, (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };


            var vertices = new List<MPoint>(BottomDots);
            vertices.AddRange(TopDots.ToList());
            vertices.Add(CenterTop);
            vertices.Add(CenterBot);

            for (int i = 0; i < vertices.Count; ++i)
            {
                var coords = new float[4, 1] { { vertices[i].SX }, { vertices[i].SY }, { vertices[i].SZ }, { vertices[i].SW } };

                if (sx != 0)
                    this.ModelMatrix = MatrixMultiplier.MultiplyMatrix(this.ModelMatrix, rotateX);
                else
                    this.ModelMatrix = MatrixMultiplier.MultiplyMatrix(this.ModelMatrix, rotateY);

                coords = MatrixMultiplier.MultiplyMatrix(this.ModelMatrix, coords);

                vertices[i] = new MPoint(coords[0, 0] + center.SX, coords[1, 0] + center.SY, coords[2, 0] + center.SZ);
            }

            //InitFacets();
        }
    }
}
