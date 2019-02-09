using EmuEngine.EmuMath.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    [DataContract]
    public abstract class MCommonPrimitive : MShape
    {
        public float Height { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }

        public MCommonPrimitive()
        {

        }

        public MCommonPrimitive(float x, float y, float z) : base(x, y, z) { }

        public virtual MPoint GetCenterPoint()
        {
            var vertices = GetVertices();

            if (vertices.Count == 0)
                throw new NullReferenceException("Shape doesn't have verices");

            MPoint maxCoords = new MPoint(vertices[0].Current.X, vertices[0].Current.Y, vertices[0].Current.Z);
            MPoint minCoords = new MPoint(vertices[0].Current.X, vertices[0].Current.Y, vertices[0].Current.Z);

            foreach (MPoint vertex in vertices)
            {
                CompareAndSetMaxCoords(vertex, maxCoords);
                CompareAndSetMinCoords(vertex, minCoords);
            }

            return new MPoint((maxCoords.Current.X + minCoords.Current.X) / 2,
                (maxCoords.Current.Y + minCoords.Current.Y) / 2,
                (maxCoords.Current.Z + minCoords.Current.Z) / 2);
        }

        private void CompareAndSetMaxCoords(MPoint sourcePoint, MPoint destinationPoint)
        {
            destinationPoint.Current.X = sourcePoint.Current.X > destinationPoint.Current.X ?
                sourcePoint.Current.X : destinationPoint.Current.X;

            destinationPoint.Current.Y = sourcePoint.Current.Y > destinationPoint.Current.Y ?
                sourcePoint.Current.Y : destinationPoint.Current.Y;

            destinationPoint.Current.Z = sourcePoint.Current.Z > destinationPoint.Current.Z ?
                sourcePoint.Current.Z : destinationPoint.Current.Z;

        }

        private void CompareAndSetMinCoords(MPoint sourcePoint, MPoint destinationPoint)
        {
            destinationPoint.Current.X = sourcePoint.Current.X < destinationPoint.Current.X ?
                sourcePoint.Current.X : destinationPoint.Current.X;

            destinationPoint.Current.Y = sourcePoint.Current.Y < destinationPoint.Current.Y ?
                sourcePoint.Current.Y : destinationPoint.Current.Y;

            destinationPoint.Current.Z = sourcePoint.Current.Z < destinationPoint.Current.Z ?
                sourcePoint.Current.Z : destinationPoint.Current.Z;
        }
    }
}
