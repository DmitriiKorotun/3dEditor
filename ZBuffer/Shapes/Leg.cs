using EmuEngine.EmuMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public class Leg : MSideCylinder
    {
        public MBox Platform { get; set; }
        public MPoint Center { get; set; }
        public MPoint PlatformCenter { get; set; }

        public float SX { get; set; }
        public float SY { get; set; }

        public Leg(MPoint centerBot, float radius, float heigth, float sx, float sy) : base(centerBot, radius, heigth, sx, sy)
        {
            Center = centerBot;

            SX = sx;
            SY = sy;

            PlatformCenter = new MPoint(centerBot.SX, centerBot.SY, centerBot.SZ);
            if (sy > 0)
            {
                if (sy < 90)
                    PlatformCenter = new MPoint(this.CenterBot.SX, this.CenterBot.SY, this.CenterBot.SZ - radius);
                else
                    PlatformCenter = new MPoint(this.CenterTop.SX, this.CenterTop.SY, this.CenterTop.SZ - radius);
            }
            else
            {
                if (sx < 90)
                    PlatformCenter = new MPoint(this.CenterBot.SX, this.CenterBot.SY, this.CenterBot.SZ - radius);
                else
                    PlatformCenter = new MPoint(this.CenterTop.SX, this.CenterTop.SY, this.CenterTop.SZ - radius);
            }

            Platform = new MBox(PlatformCenter, 15, 10, 5, true);
        }

        public void ChangePlatformLength(int length)
        {
            Platform = new MBox(PlatformCenter, length, Platform.Width, Platform.Height, true);
        }

        public void ChangePlatformWidth(int width)
        {
            Platform = new MBox(PlatformCenter, Platform.Length, width, Platform.Height, true);
        }

        //public void ChangePlatformHeigth(int height)
        //{

        //}

        public override List<MFacet> GetAllFacets()
        {
            var facets = new List<MFacet>();

            facets.AddRange(Platform.GetAllFacets());
            facets.AddRange(base.GetAllFacets());

            return facets;
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            points.AddRange(Platform.GetAllPoints());
            points.AddRange(base.GetAllPoints());

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            var vertices = new List<MPoint>();

            vertices.AddRange(Platform.GetVertices());
            vertices.AddRange(base.GetVertices());

            return vertices;
        }
    }
}
