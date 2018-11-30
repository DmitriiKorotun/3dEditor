using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public class Shuttle : MCommonPrimitive
    {
        MSideCylinder Body { get; set; }
        MSideCylinder Top { get; set; }
        MSideCylinder EngineToBody { get; set; }
        MSideCylinder EngineExhaust { get; set; }

        MTopCylinder BodyHead { get; set; }
        MTopCylinder EngineMount { get; set; }
        MTopCylinder ExhaustMount { get; set; }

        public Shuttle()
        {
            Body = new MSideCylinder(new MPoint(0, 0, 0), 30, 60);
            Top = new MSideCylinder(new MPoint(0, 0, 80), 20, 10);
            EngineToBody = new MSideCylinder(new MPoint(0, 0, -20), 20, 20);
            EngineExhaust = new MSideCylinder(new MPoint(0, 0, -65), 15, 20);

            BodyHead = new MTopCylinder(new MPoint(0, 0, 60), 30, 20, 20);
            EngineMount = new MTopCylinder(new MPoint(0, 0, -30), 15, 20, 10);
            ExhaustMount = new MTopCylinder(new MPoint(0, 0, -45), 15, 10, 15);
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            points.AddRange(Body.GetAllPoints());
            points.AddRange(Top.GetAllPoints());
            points.AddRange(EngineToBody.GetAllPoints());
            points.AddRange(EngineExhaust.GetAllPoints());

            points.AddRange(BodyHead.GetAllPoints());
            points.AddRange(EngineMount.GetAllPoints());
            points.AddRange(ExhaustMount.GetAllPoints());

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            var vertices = new List<MPoint>();

            vertices.AddRange(Body.GetVertices());
            vertices.AddRange(Top.GetVertices());
            vertices.AddRange(EngineToBody.GetVertices());
            vertices.AddRange(EngineExhaust.GetVertices());

            vertices.AddRange(BodyHead.GetVertices());
            vertices.AddRange(EngineMount.GetVertices());
            vertices.AddRange(ExhaustMount.GetVertices());


            return vertices;
        }

        public List<MCommonPrimitive> GetAllCommonPrimitives()
        {
            var primitives = new List<MCommonPrimitive>();

            primitives.Add(Body);
            primitives.Add(Top);
            primitives.Add(EngineToBody);
            primitives.Add(EngineExhaust);

            primitives.Add(BodyHead);
            primitives.Add(EngineMount);
            primitives.Add(ExhaustMount);

            return primitives;
        }
    }
}
