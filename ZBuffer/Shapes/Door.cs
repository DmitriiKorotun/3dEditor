using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    class Door : MBox
    {
        public MSideCylinder Window { get; set; }
        public MPoint Center { get; set; }
        public MPoint CylinderCenter { get; set; }

        public Door(MPoint center, MPoint cylinderCenter, float length, float width, float height, float sx, float sy) : base(center, length, width, height, true)
        {
            Center = center;
            CylinderCenter = cylinderCenter;

            var legCenter = CalculateDoorWindowCenter(center, cylinderCenter, 50, sx, sy);

            Window = new MSideCylinder(legCenter, 5, 10, sx, sy);
        }

        private MPoint CalculateDoorWindowCenter(MPoint center, MPoint cylinderCenter, float legLength, float sx, float sy)
        {
            MPoint legCenter;

            if (sy > 0)
            {
                if (sy < 90)
                    legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY, cylinderCenter.SZ);
                else
                    legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY, cylinderCenter.SZ);
            }
            else
            {
                if (sx < 90)
                    legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY, cylinderCenter.SZ);
                else
                    legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY, cylinderCenter.SZ);
            }

            return legCenter;
        }

        public override List<MFacet> GetAllFacets()
        {
            var facets = new List<MFacet>();

            facets.AddRange(Window.GetAllFacets());
            facets.AddRange(base.GetAllFacets());

            return facets;
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            points.AddRange(Window.GetAllPoints());
            points.AddRange(base.GetAllPoints());

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            var vertices = new List<MPoint>();

            vertices.AddRange(Window.GetVertices());
            vertices.AddRange(base.GetVertices());

            return vertices;
        }
    }
}
