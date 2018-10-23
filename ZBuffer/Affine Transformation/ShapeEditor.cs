using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.Shapes;
using ZBuffer.Tools;
using ZBuffer.ZBufferMath;

namespace ZBuffer.Affine_Transformation
{
    //TODO Implement unified rotation method and optimize shape movement (while rotation) 
    public class ShapeEditor : IShapeEditor
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

        public void Move(MCommonPrimitive shape, float Sx, float Sy, float Sz)
        {
            float[,] movementMatrix = {
                { 1, 0, 0, Sx },
                { 0, 1, 0, Sy },
                { 0, 0, 1, Sz },
                { 0, 0, 0, 1 }
            };

            shape.TransformationMatrix = MatrixMultiplier.MultiplyMatrix(movementMatrix, shape.TransformationMatrix);

            //TranformShape(shape, movementMatrix);
        }

        public void Scale(MCommonPrimitive shape, float Sx, float Sy, float Sz)
        {
            float[,] scaleMatrix = {
                { Sx, 0, 0, 0 },
                { 0, Sy, 0, 0 },
                { 0, 0, Sz, 0 },
                { 0, 0, 0, 1 }
            };

            TranformShape(shape, scaleMatrix);
        }

        public void ProjectShape(MCommonPrimitive shape, Camera camera)
        {
            TranformShape(shape, camera.ProjectionMatrix);

            //foreach (MPoint point in shape.GetAllPoints())
            //{
            //    point.
            //}
        }

        private void RotateShape(MCommonPrimitive shape, double angle, Vector3 axis)
        {
            double rads = angle * Math.PI / 180.0;
            var vertices = shape.GetVertices();

            MPoint currentShapeCenter;

            Quaternion rotationQuaternion = GetRotationQuaternion(axis, rads);

            // Sets object in the coordinate's origin, rotates it and move to the previous place
            MoveShapeToOrigin(shape, out currentShapeCenter);                   
            RotateVertices(vertices, rotationQuaternion);
            MoveShapeToPreviousPosition(shape, currentShapeCenter);
        }

        private void RotateVertices(List<MPoint> vertices, Quaternion rotationQuaternion)
        {          
            for (int i = 0; i < vertices.Count; ++i)
            {
                var vertexVector = new Vector3(vertices[i].X, vertices[i].Y, vertices[i].Z);

                var newCoords = Vector3.Transform(vertexVector, rotationQuaternion);

                SetNewCoordinatesToPoint(vertices[i], newCoords);
            }
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

        private void TranformShape(MCommonPrimitive shape, float[,] transformationMatrix)
        {
            List<MPoint> vertices = shape.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {
                float[,] vertexCoords = { { vertices[i].X }, { vertices[i].Y }, { vertices[i].Z }, { 1 } };

                float[,] newCoords = MatrixMultiplier.MultiplyMatrix(transformationMatrix, vertexCoords);

                SetNewCoordinatesToPoint(vertices[i], newCoords);

                //TEST
                TransformPointCoordsToDecart(vertices[i]);
            }
        }

        private void TransformShape(MCommonPrimitive shape)
        {
            List<MPoint> vertices = shape.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {
                float[,] vertexCoords = { { vertices[i].SX }, { vertices[i].SY }, { vertices[i].SZ }, { 1 } };

                float[,] newCoords = MatrixMultiplier.MultiplyMatrix(shape.TransformationMatrix, vertexCoords);

                SetNewCoordinatesToPoint(vertices[i], newCoords);

                ////TEST
                //TransformPointCoordsToDecart(vertices[i]);
            }
        }

        public void TransformShapes(List<MCommonPrimitive> shapes)
        {
            foreach (MCommonPrimitive shape in shapes)
                TransformShape(shape);
        }

        private void SetNewCoordinatesToPoint(MPoint destinationPoint, Vector3 newCoordinates)
        {
            destinationPoint.X = newCoordinates.X;
            destinationPoint.Y = newCoordinates.Y;
            destinationPoint.Z = newCoordinates.Z;
        }

        private void SetNewCoordinatesToPoint(MPoint destinationPoint, float[,] newCoordinates)
        {
            destinationPoint.X = newCoordinates[0, 0];
            destinationPoint.Y = newCoordinates[1, 0];
            destinationPoint.Z = newCoordinates[2, 0];
            destinationPoint.W = newCoordinates[3, 0];
        }

        private void TransformPointCoordsToDecart(MPoint point)
        {
            point.X /= point.W;
            point.Y /= point.W;
            point.Z /= point.W;

            point.X = 640 / 2 * point.X + (point.SX + 640 / 2);
            point.Y = 360 / 2 * point.Y + (point.SY + 360 / 2);
            point.Z = (10 - (-10)) / 2 * point.Z + (0) / 2;
        }
    }
}
