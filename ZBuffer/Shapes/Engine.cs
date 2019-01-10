using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    class Engine : MCommonPrimitive
    {
        MSideCylinder EngineToBody { get; set; }
        MSideCylinder EngineExhaust { get; set; }

        MTopCylinder EngineMount { get; set; }
        MTopCylinder ExhaustMount { get; set; }

        public float EngineRadius { get; set; }
        const int radiusDiff = 5;

        public Engine(int x, int y, int z)
        {
            EngineRadius = 20;

            float smallEngineRadius = EngineRadius - radiusDiff;

            EngineToBody = new MSideCylinder(new MPoint(x, y, z), EngineRadius, 20, true); // Часть заднего фюзеляжа
            EngineExhaust = new MSideCylinder(new MPoint(x, y, z + -45), smallEngineRadius, 20, true); // Часть двигателя

            EngineMount = new MTopCylinder(new MPoint(x, y, z + -10), smallEngineRadius, EngineRadius, 10, true); // Часть заднего фюзеляжа
            ExhaustMount = new MTopCylinder(new MPoint(x, y, z + -25), smallEngineRadius, EngineRadius - 10, 15, true); // Часть двигателя
        }

        public Engine(int x, int y, int z, float radius)
        {
            EngineRadius = radius;

            float smallEngineRadius = EngineRadius - radiusDiff;

            EngineToBody = new MSideCylinder(new MPoint(x, y, z), EngineRadius, 20, true); // Часть заднего фюзеляжа
            EngineExhaust = new MSideCylinder(new MPoint(x, y, z + -45), smallEngineRadius, 20, true); // Часть двигателя

            EngineMount = new MTopCylinder(new MPoint(x, y, z + -10), smallEngineRadius, EngineRadius, 10, true); // Часть заднего фюзеляжа
            ExhaustMount = new MTopCylinder(new MPoint(x, y, z + -25), smallEngineRadius, EngineRadius - 10, 15, true); // Часть двигателя
        }

        public override List<MFacet> GetAllFacets()
        {
            var facets = new List<MFacet>();

            facets.AddRange(EngineToBody.GetAllFacets());
            facets.AddRange(EngineExhaust.GetAllFacets());

            facets.AddRange(EngineMount.GetAllFacets());
            facets.AddRange(ExhaustMount.GetAllFacets());

            return facets;
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            points.AddRange(EngineToBody.GetAllPoints());
            points.AddRange(EngineExhaust.GetAllPoints());

            points.AddRange(EngineMount.GetAllPoints());
            points.AddRange(ExhaustMount.GetAllPoints());

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            var vertices = new List<MPoint>();

            vertices.AddRange(EngineToBody.GetVertices());
            vertices.AddRange(EngineExhaust.GetVertices());

            vertices.AddRange(EngineMount.GetVertices());
            vertices.AddRange(ExhaustMount.GetVertices());


            return vertices;
        }
    }
}
