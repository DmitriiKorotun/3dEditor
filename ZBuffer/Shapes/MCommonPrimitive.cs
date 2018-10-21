﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZBuffer.Shapes
{
    [DataContract]
    public abstract class MCommonPrimitive : MShape
    {
        public float Height { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }

        public abstract List<MPoint> GetVertices();
        public virtual MPoint GetCenterPoint()
        {
            var vertices = GetVertices();

            if (vertices.Count == 0)
                throw new NullReferenceException("Shape doesn't have verices");

            MPoint maxCoords = new MPoint(vertices[0].X, vertices[0].Y, vertices[0].Z);
            MPoint minCoords = new MPoint(vertices[0].X, vertices[0].Y, vertices[0].Z);

            foreach (MPoint vertex in vertices)
            {
                CompareAndSetMaxCoords(vertex, maxCoords);
                CompareAndSetMinCoords(vertex, minCoords);
            }

            return new MPoint((maxCoords.X + minCoords.X) / 2, (maxCoords.Y + minCoords.Y) / 2, (maxCoords.Z + minCoords.Z) / 2);
        }

        private void CompareAndSetMaxCoords(MPoint sourcePoint, MPoint destinationPoint)
        {
            destinationPoint.X = sourcePoint.X > destinationPoint.X ? sourcePoint.X : destinationPoint.X;
            destinationPoint.Y = sourcePoint.Y > destinationPoint.Y ? sourcePoint.Y : destinationPoint.Y;
            destinationPoint.Z = sourcePoint.Z > destinationPoint.Z ? sourcePoint.Z : destinationPoint.Z;
        }

        private void CompareAndSetMinCoords(MPoint sourcePoint, MPoint destinationPoint)
        {
            destinationPoint.X = sourcePoint.X < destinationPoint.X ? sourcePoint.X : destinationPoint.X;
            destinationPoint.Y = sourcePoint.Y < destinationPoint.Y ? sourcePoint.Y : destinationPoint.Y;
            destinationPoint.Z = sourcePoint.Z < destinationPoint.Z ? sourcePoint.Z : destinationPoint.Z;
        }
    }
}
