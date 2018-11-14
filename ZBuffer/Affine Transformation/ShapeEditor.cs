using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;
using EmuEngine.Tools;
using EmuEngine.EmuMath;

namespace EmuEngine.Affine_Transformation
{
    //TODO Implement unified rotation method and optimize shape movement (while rotation) 
    public class ShapeEditor : IShapeEditor
    {
        //public void Rotate(MCommonPrimitive shape, double sx, double sy, double sz)
        //{
        //    if (sx != 0)
        //        RotateX(shape, sx);

        //    if (sy != 0)
        //        RotateY(shape, sy);

        //    if (sz != 0)
        //        RotateZ(shape, sz);
        //}

        //public void RotateX(MCommonPrimitive shape, double angle)
        //{
        //    var axis = Vector3.UnitX;

        //    RotateShape(shape, angle, axis);
        //}

        //public void RotateY(MCommonPrimitive shape, double angle)
        //{
        //    var axis = Vector3.UnitY;

        //    RotateShape(shape, angle, axis);
        //}

        //public void RotateZ(MCommonPrimitive shape, double angle)
        //{
        //    var axis = Vector3.UnitZ;

        //    RotateShape(shape, angle, axis);
        //}

        //------------------ROTATING-------------------------------
        public void Rotate(MCommonPrimitive shape, double sx, double sy, double sz)
        {
            if (sx != 0)
                RotateX(shape, sx);

            if (sy != 0)
                RotateY(shape, sy);

            if (sz != 0)
                RotateZ(shape, sz);
        }

        public void RotateRange(List<MCommonPrimitive> shapes, double sx, double sy, double sz)
        {
            for (int i = 0; i < shapes.Count; ++i)
                Rotate(shapes[i], sx, sy, sz);
        }

        public void RotateX(MCommonPrimitive shape, double angle)
        {
            double rads = angle * Math.PI / 180.0;

            float[,] rotateX = {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
                { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, rotateX);
        }

        public void RotateY(MCommonPrimitive shape, double angle)
        {
            double rads = angle * Math.PI / 180.0;

            float[,] rotateY = {
                { (float)Math.Cos(rads), 0, (float)Math.Sin(rads), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(rads), 0, (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, rotateY);
        }

        public void RotateZ(MCommonPrimitive shape, double angle)
        {
            double rads = angle * Math.PI / 180.0;

            float[,] rotateZ = {
                { (float)Math.Cos(rads), -(float)Math.Sin(rads), 0, 0 },
                { (float)Math.Sin(rads), (float)Math.Cos(rads), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, rotateZ);
        }

        //------------------TRANSLATING-------------------------------
        public void Translate(MCommonPrimitive shape, float sx, float sy, float sz)
        {
            float[,] movementMatrix = {
                { 1, 0, 0, sx },
                { 0, 1, 0, sy },
                { 0, 0, 1, sz },
                { 0, 0, 0, 1 }
            };

            shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(movementMatrix, shape.ModelMatrix);
        }

        public void TranslateRange(List<MCommonPrimitive> shapes, float sx, float sy, float sz)
        {
            for (int i = 0; i < shapes.Count; ++i)
                Translate(shapes[i], sx, sy, sz);
        }

        //------------------SCALING-------------------------------
        public void Scale(MCommonPrimitive shape, float sx, float sy, float sz)
        {
            float[,] scaleMatrix = {
                { sx, 0, 0, 0 },
                { 0, sy, 0, 0 },
                { 0, 0, sz, 0 },
                { 0, 0, 0, 1 }
            };

            shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, scaleMatrix);
        }

        public void ScaleRange(List<MCommonPrimitive> shapes, float sx, float sy, float sz)
        {
            for (int i = 0; i < shapes.Count; ++i)
                Scale(shapes[i], sx, sy, sz);
        }

        private void RotateShape(MCommonPrimitive shape, double angle, Vector3 axis)
        {
            double rads = angle * Math.PI / 180.0;

            Quaternion rotationQuaternion = GetRotationQuaternion(axis, rads);

            shape.RotationQuaternion *= rotationQuaternion;
        }

        //private void RotateShape(MCommonPrimitive shape, double angle, Vector3 axis)
        //{
        //    double rads = angle * Math.PI / 180.0;
        //    var vertices = shape.GetVertices();

        //    MPoint currentShapeCenter;

        //    Quaternion rotationQuaternion = GetRotationQuaternion(axis, rads);

        //    shape.RotationQuaternion *= rotationQuaternion;

        //    // Sets object in the coordinate's origin, rotates it and move to the previous place
        //    MoveShapeToOrigin(shape, out currentShapeCenter);
        //    RotateVertices(vertices, rotationQuaternion);
        //    MoveShapeToPreviousPosition(shape, currentShapeCenter);
        //}

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




        private void TransformShape(MCommonPrimitive shape)
        {
            List<MPoint> vertices = shape.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {
                float[,] vertexCoords = { { vertices[i].SX }, { vertices[i].SY }, { vertices[i].SZ }, { 1 } };

                float[,] newCoords = MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, vertexCoords);

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

        public void GetTransformedShapes(List<MCommonPrimitive> shapes, Camera camera)
        {
            for (int i = 0; i < shapes.Count; ++i)
                GetTransformedShape(shapes[i], camera);
        }

        public void GetTransformedShape(MCommonPrimitive shape, Camera camera)
        {
            var vertices = shape.GetVertices();


            string text = "";
            foreach (MPoint vertex in vertices)
            {
                text += "\r\nX: " + vertex.X;
                text += "\tY: " + vertex.Y;
                text += "\tZ: " + vertex.Z;
            }
            //text += "\r\nX: " + windowCoordinates[0, 0];
            //text += "\tY: " + windowCoordinates[1, 0];
            //text += "\tZ: " + windowCoordinates[2, 0];

            for (int i = 0; i < vertices.Count; ++i)
            {
                float[,] vertexCoords = { { vertices[i].SX }, { vertices[i].SY }, { vertices[i].SZ }, { vertices[i].SW } };

                var modelViewMatrix = MatrixMultiplier.MultiplyMatrix(camera.ViewMatrix, shape.ModelMatrix);
                var eyeCoordinates = MatrixMultiplier.MultiplyMatrix(modelViewMatrix, vertexCoords);
                var clipCoordinates = MatrixMultiplier.MultiplyMatrix(camera.ProjectionMatrix, eyeCoordinates);
                
                var ndc = new float[,] {
                    { clipCoordinates[0, 0] / clipCoordinates[3, 0] },
                    { clipCoordinates[1, 0] / clipCoordinates[3, 0] },
                    { clipCoordinates[2, 0] / clipCoordinates[3, 0] },
                    { clipCoordinates[3, 0]}
                };

                var windowCoordinates = new float[,] { 
                    { 640 / 2 * ndc[0, 0] + (640 / 2) },
                    { 360 / 2 * ndc[1, 0] + (360 / 2) },
                    { (50 - (-50)) / 2 * ndc[2, 0] + (50 + (-50)) / 2 },
                    { ndc[3, 0]}
                };

                

                SetNewCoordinatesToPoint(vertices[i], windowCoordinates);             
            }


            text += "\r\n-----------------------------------------------";

            File.AppendAllText("vertices.txt", text);
            //float[,] 
            //    ,
            //    projectionModelViewMatrix = GetProjectionModelViewMatrix(camera.ProjectionMatrix, modelViewMatrix);

            //GetClippedCoordinates(shape, projectionModelViewMatrix);
            //GetNormalizedCoordinates(shape);
            //GetWindowCoordinates(shape);

            //float[,] modelView = MatrixMultiplier.MultiplyMatrix(camera.ViewMatrix, shape.ModelMatrix);
            //float[,] projModelView = MatrixMultiplier.MultiplyMatrix(camera.ProjectionMatrix, modelView);

            //for (int i = 0; i < projModelView.GetLength(0); ++i)
            //{
            //    for (int j = 0; j < projModelView.GetLength(1); ++j)
            //    {
            //        if (i != 3 && j != 3)
            //            projModelView[i, j] /= projModelView[3, 3];
            //    }
            //}

            //List<MPoint> vertices = shape.GetVertices();

            //for (int i = 0; i < vertices.Count; ++i)
            //{
            //    float[,] vertexCoords = { { vertices[i].SX }, { vertices[i].SY }, { vertices[i].SZ }, { vertices[i].SW } };

            //    float[,] newCoords = MatrixMultiplier.MultiplyMatrix(projModelView, vertexCoords);

            //    SetNewCoordinatesToPoint(vertices[i], newCoords);

            //    TransformPointCoordsToDecart(vertices[i]);
            //}
        }

        private float[,] GetModelViewMatrix(float[,] modelMatrix, float[,] viewMatrix)
        {
            return MatrixMultiplier.MultiplyMatrix(viewMatrix, modelMatrix);
        }

        private float[,] GetProjectionModelViewMatrix(float[,] projectionMatrix, float[,] modelViewMatrix)
        {
            return MatrixMultiplier.MultiplyMatrix(projectionMatrix, modelViewMatrix);
        }

        private void GetClippedCoordinates(MCommonPrimitive shape, float[,] projectionMatrix)
        {
            List<MPoint> vertices = shape.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {
                float[,] vertexCoords = { { vertices[i].X }, { vertices[i].Y }, { vertices[i].Z }, { 1 } };

                float[,] newCoords = MatrixMultiplier.MultiplyMatrix(projectionMatrix, vertexCoords);

                SetNewCoordinatesToPoint(vertices[i], newCoords);
            }
        }

        private void GetNormalizedCoordinates(MCommonPrimitive shape)
        {
            List<MPoint> vertices = shape.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {
                float[,] newCoords = { { vertices[i].X / vertices[i].W }, { vertices[i].Y / vertices[i].W }, { vertices[i].Z / vertices[i].W }, { vertices[i].W } };

                SetNewCoordinatesToPoint(vertices[i], newCoords);
            }
        }

        private void GetWindowCoordinates(MCommonPrimitive shape)
        {
            foreach (MPoint point in shape.GetVertices())
                TransformPointCoordsToDecart(point);
        }

        private void TransformPointCoordsToDecart(MPoint point)
        {
            point.X = 640 / 2 * point.X + (point.SX + 640 / 2);
            point.Y = 360 / 2 * point.Y + (point.SY + 360 / 2);
            point.Z = (10 - (-10)) / 2 * point.Z + (0) / 2;
        }
    }
}
