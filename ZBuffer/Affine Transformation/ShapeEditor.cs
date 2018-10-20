using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.Shapes;
using ZBuffer.ZBufferMath;

namespace ZBuffer.Affine_Transformation
{
    public class ShapeEditor
    {
        public void RotateX(MCommonPrimitive shape, double angle)
        {
            var axis = Vector3.UnitX;

            RotateShape(shape, angle, axis);
        }

        public void RotateY(MCommonPrimitive shape, double angle)
        {
            var axis = Vector3.UnitY;

            RotateShape(shape, angle, axis);
        }

        public void RotateZ(MCommonPrimitive shape, double angle)
        {
            var axis = Vector3.UnitZ;

            RotateShape(shape, angle, axis);
        }

        private void RotateShape(MCommonPrimitive shape, double angle, Vector3 axis)
        {
            double rads = angle * Math.PI / 180.0;

            MPoint currentShapeCenter;

            MoveShapeToOrigin(shape, out currentShapeCenter);

            Quaternion rotationQuaternion = GetRotationQuaternion(axis, rads);

            var vertices = shape.GetVertices();

            RotateVertices(vertices, rotationQuaternion);

            MoveShapeToPreviousPosition(shape, currentShapeCenter);
        }

        private void MoveShapeToOrigin(MCommonPrimitive shape, out MPoint shapeCenter)
        {
            var vertices = shape.GetVertices();

            shapeCenter = shape.GetCenterPoint();

            for (int i = 0; i < vertices.Count; ++i)
            {
                vertices[i].X -= shapeCenter.X;
                vertices[i].Y -= shapeCenter.Y;
                vertices[i].Z -= shapeCenter.Z;
            }
        }

        private void MoveShapeToPreviousPosition(MCommonPrimitive shape, MPoint previousShapeCenter)
        {
            var vertices = shape.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {
                vertices[i].X += previousShapeCenter.X;
                vertices[i].Y += previousShapeCenter.Y;
                vertices[i].Z += previousShapeCenter.Z;
            }
        }

        private void RotateVertices(List<MPoint> vertices, Quaternion rotationQuaternion)
        {          
            for (int i = 0; i < vertices.Count; ++i)
            {
                var vertexVector = new Vector3(vertices[i].X, vertices[i].Y, vertices[i].Z);

                var newCoords = Vector3.Transform(vertexVector, rotationQuaternion);

                SetVectorCoordinatesToPoint(vertices[i], newCoords);
            }
        }

        private void SetVectorCoordinatesToPoint(MPoint point, Vector3 vector)
        {
            point.X = vector.X;
            point.Y = vector.Y;
            point.Z = vector.Z;
        }

        private Quaternion GetRotationQuaternion(Vector3 axis, double rads)
        {
            float quaternionX = (float)(Math.Sin(rads / 2) * axis.X),
                   quaternionY = (float)(Math.Sin(rads / 2) * axis.Y),
                   quaternionZ = (float)(Math.Sin(rads / 2) * axis.Z),
                   quaternionW = (float)(Math.Cos(rads / 2));

            return new Quaternion(quaternionX, quaternionY, quaternionZ, quaternionW);
        }

        public float[,] RotateY(MPoint shapeCenter, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateY = {
                { (float)Math.Cos(rads), 0, (float)Math.Sin(rads), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(rads), 0, (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float[,] shapeCoords = { { shapeCenter.SX }, { shapeCenter.SY }, { shapeCenter.SZ }, { 1 } };

            return MatrixMultiplier.MultiplyMatrix(rotateY, shapeCoords);
        }

        public float[,] RotateX(MPoint shapeCenter, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateX = {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
                { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float[,] shapeCoords = { { shapeCenter.SX }, { shapeCenter.SY }, { shapeCenter.SZ }, { 1 } };

            return MatrixMultiplier.MultiplyMatrix(rotateX, shapeCoords);
        }

        //TO DEL
        public void RotateXVertices(MPoint[] vertices, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateX = {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
                { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            for (int i = 0; i < vertices.Length; ++i)
            {
                float[,] shapeCoords = { { vertices[i].SX }, { vertices[i].SY }, { vertices[i].SZ }, { 1 } };

                float[,] newCoords = MatrixMultiplier.MultiplyMatrix(rotateX, shapeCoords);

                vertices[i].X = newCoords[0, 0];
                vertices[i].Y = newCoords[1, 0];
                vertices[i].Z = newCoords[2, 0];
            }
        }

        public float[,] RotateZ(MPoint shapeCenter, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateZ = {
                { (float)Math.Cos(rads), -(float)Math.Sin(rads), 0, 0 },
                { (float)Math.Sin(rads), (float)Math.Cos(rads), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float[,] shapeCoords = { { shapeCenter.SX }, { shapeCenter.SY }, { shapeCenter.SZ }, { 1 } };

            return MatrixMultiplier.MultiplyMatrix(rotateZ, shapeCoords);
        }

        //TO DEL
        public void RotateZVertices(List<MPoint> vertices, double angle)
        {
            double rads = angle * Math.PI / 180.0;
            //формируем матрицу поворота;
            float[,] rotateZ = {
                { (float)Math.Cos(rads), -(float)Math.Sin(rads), 0, 0 },
                { (float)Math.Sin(rads), (float)Math.Cos(rads), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            for (int i = 0; i < vertices.Count; ++i)
            {
                float[,] shapeCoords = { { vertices[i].X }, { vertices[i].Y }, { vertices[i].Z }, { 1 } };

                float[,] newCoords = MatrixMultiplier.MultiplyMatrix(rotateZ, shapeCoords);

                vertices[i].X = newCoords[0, 0];
                vertices[i].Y = newCoords[1, 0];
                vertices[i].Z = newCoords[2, 0];
            }
        }

        public float[,] Scale(MPoint shapeCenter, float Sx, float Sy, float Sz)
        {
            //формируем матрицу масштабирования;
            float[,] rotateZ = {
                { Sx, 0, 0, 0 },
                { 0, Sy, 0, 0 },
                { 0, 0, Sz, 0 },
                { 0, 0, 0, 1 }
            };

            //получаем координаты центральной точки:
            float[,] shapeCoords = { { shapeCenter.SX }, { shapeCenter.SY }, { shapeCenter.SZ }, { 1 } };

            return MatrixMultiplier.MultiplyMatrix(rotateZ, shapeCoords);
        }
    }
}
