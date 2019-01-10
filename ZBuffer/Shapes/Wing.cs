using EmuEngine.EmuMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmuEngine.EmuEngineExceptions;

namespace EmuEngine.Shapes
{
    public class Wing : MBox
    {
        public MSideCylinder WingCylinder { get; set; }
        public Leg WingLeg { get; set; }

        MPoint Center { get; set; }
        MPoint CylinderCenter { get; set; }

        public Wing(MPoint center, MPoint cylinderCenter, float length, float width, float height, float sx, float sy) : base(center, length, width, height, true)
        {
            Center = center;
            CylinderCenter = cylinderCenter;

            WingCylinder = new MSideCylinder(cylinderCenter, 5, height, true);

            var legCenter = CalculateWingLegCenter(center, cylinderCenter, 50, sx, sy);

            WingLeg = new Leg(legCenter, 5, 50, sx, sy);
        }

        public void ChangeLegRadius(int radius)
        {
            if (radius < 5)
                throw new ShuttleException(ShuttleExceptions.WingLegRadiusTooSmall);

            WingLeg = new Leg(WingLeg.Center, radius, WingLeg.Height, WingLeg.SX, WingLeg.SY);
        }

        public void ChangeLegLength(int length)
        {
            if (length < 10)
                throw new ShuttleException(ShuttleExceptions.WingLegLengthTooSmall);

            var legCenter = CalculateWingLegCenter(Center, CylinderCenter, length, WingLeg.SX, WingLeg.SY);

            WingLeg = new Leg(legCenter, WingLeg.GetRadius(), length, WingLeg.SX, WingLeg.SY);
        }

        public void ChangeLegAngle(int angle)
        {
            if (angle < 10)
                throw new ShuttleException(ShuttleExceptions.WingLegAngleTooSmall);
            else if(angle > 80)
                throw new ShuttleException(ShuttleExceptions.WingLegAngleTooBig);

            int sx = GetSX(angle), sy = GetSY(angle);

            var legCenter = CalculateWingLegCenter(Center, CylinderCenter, WingLeg.Height, sx, sy);

            WingLeg = new Leg(legCenter, WingLeg.GetRadius(), WingLeg.Height, sx, sy);
        }

        private int GetSX(int angle)
        {
            if (WingLeg.SX == 0)
                return 0;
            else if (WingLeg.SX <= 90)
                return angle;
            else
                return 181 - angle;
        }

        private int GetSY(int angle)
        {
            if (WingLeg.SY == 0)
                return 0;
            else if (WingLeg.SY <= 90)
                return angle;
            else
                return 181 - angle;
        }

        private MPoint CalculateWingLegCenter(MPoint center, MPoint cylinderCenter, float legLength, float sx, float sy)
        {
            MPoint legCenter;

            if (sy > 0) {
                if (sy < 90)
                    legCenter = new MPoint(cylinderCenter.SX - legLength * Math.Sin(Math.PI / 180 * sy ) / 2, cylinderCenter.SY, cylinderCenter.SZ + 20 - legLength * Math.Cos(Math.PI / 180 * sy) / 2);
                else
                    legCenter = new MPoint(cylinderCenter.SX + legLength * Math.Sin(Math.PI / 180 * (180 - sy)) / 2, cylinderCenter.SY, cylinderCenter.SZ + 20 - legLength * Math.Cos(Math.PI / 180 * (180 - sy)) / 2);
            }
            else
            {
                if (sx < 90)
                    legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY + legLength * Math.Sin(Math.PI / 180 * sx) / 2, cylinderCenter.SZ + 20 - legLength * Math.Cos(Math.PI / 180 * sx) / 2);
                else
                    legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY - legLength * Math.Sin(Math.PI / 180 * (180 - sx)) / 2, cylinderCenter.SZ + 20 - legLength * Math.Cos(Math.PI / 180 * (180 - sx)) / 2);
            }

            return legCenter;
        }

        public override List<MFacet> GetAllFacets()
        {
            var facets = new List<MFacet>();

            facets.AddRange(WingCylinder.GetAllFacets());
            facets.AddRange(WingLeg.GetAllFacets());
            facets.AddRange(base.GetAllFacets());

            return facets;
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            points.AddRange(WingCylinder.GetAllPoints());
            points.AddRange(WingLeg.GetAllPoints());
            points.AddRange(base.GetAllPoints());

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            var vertices = new List<MPoint>();

            vertices.AddRange(WingCylinder.GetVertices());
            vertices.AddRange(WingLeg.GetVertices());
            vertices.AddRange(base.GetVertices());

            return vertices;
        }

        //public List<MCommonPrimitive> GetAllCommonPrimitives()
        //{
        //    var primitives = new List<MCommonPrimitive>();

        //    this.ModelMatrix = this.ModelMatrix;
        //    WingCylinder.ModelMatrix = this.ModelMatrix;
        //    WingLeg.ModelMatrix = MatrixMultiplier.MultiplyMatrix(this.ModelMatrix, WingLeg.ModelMatrix);

        //    primitives.Add(this);
        //    primitives.Add(WingCylinder);
        //    primitives.Add(WingLeg);

        //    return primitives;
        //}
    }
}
