using EmuEngine.EmuMath.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    public class MComplex : MShape
    {
        public List<MCommonPrimitive> Primitives { get; set; }
        private List<Matrix4> PrimitivesModel { get; set; }

        public MComplex()
        {
            Primitives = new List<MCommonPrimitive>();
        }

        public MComplex(MCommonPrimitive shape)
        {
            Primitives = new List<MCommonPrimitive>() { shape };
        }

        public MComplex(List<MCommonPrimitive> shapes)
        {
            Primitives = new List<MCommonPrimitive>(shapes);
        }

        public void AddPrimitive(MCommonPrimitive shape)
        {
            Primitives.Add(shape);

        }

        // REWORK
        public void ApplyModelMatrixToChildren()
        {
            PrimitivesModel = new List<Matrix4>();

            for (int i = 0; i < Primitives.Count; ++i)
            {
                PrimitivesModel.Add(Primitives[i].ModelMatrix);
                Primitives[i].ModelMatrix = this.ModelMatrix * Primitives[i].ModelMatrix;               
            }
        }

        // REWORK
        public void RestoreChildrenModelMatrix()
        {
            if (PrimitivesModel is null || PrimitivesModel.Count == 0)
                throw new ArgumentException();

            for (int i = 0; i < Primitives.Count; ++i)
            {
                Primitives[i].ModelMatrix = PrimitivesModel[i];
            }
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            for (int i = 0; i < Primitives.Count; ++i)
                points.AddRange(Primitives[i].GetAllPoints());

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            var vertices = new List<MPoint>();

            for (int i = 0; i < Primitives.Count; ++i)
                vertices.AddRange(Primitives[i].GetVertices());

            return vertices;
        }
    }
}
