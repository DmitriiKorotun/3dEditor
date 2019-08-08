using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;
using EmuEngine.Tools;
using EmuEngine.EmuMath.Structures;

namespace EmuEngine.Affine_Transformation
{
    public class ShapeEditor : IShapeEditor
    {
        //------------------ROTATING-------------------------------
        public void Rotate(MShape shape, double sx, double sy, double sz)
        {
            if (sx != 0)
                RotateX(shape, sx);

            if (sy != 0)
                RotateY(shape, sy);

            if (sz != 0)
                RotateZ(shape, sz);
        }

        public void RotateRange(List<MShape> shapes, double sx, double sy, double sz)
        {
            for (int i = 0; i < shapes.Count; ++i)
                Rotate(shapes[i], sx, sy, sz);
        }

        public void RotateX(MShape shape, double angle)
        {
            double rads = angle * Math.PI / 180.0;

            var rotateX = new Matrix4(new float[,] {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
                { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            });

            shape.ModelMatrix *= rotateX;

            //shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, rotateX);
        }

        public void RotateY(MShape shape, double angle)
        {
            double rads = angle * Math.PI / 180.0;

            var rotateY = new Matrix4(new float[,] {
                { (float)Math.Cos(rads), 0, (float)Math.Sin(rads), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(rads), 0, (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            });

            shape.ModelMatrix *= rotateY;

            //shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, rotateY);
        }

        public void RotateZ(MShape shape, double angle)
        {
            double rads = angle * Math.PI / 180.0;

            var rotateZ = new Matrix4(new float[,] {
                { (float)Math.Cos(rads), -(float)Math.Sin(rads), 0, 0 },
                { (float)Math.Sin(rads), (float)Math.Cos(rads), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });

            shape.ModelMatrix *= rotateZ;

            //shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, rotateZ);
        }

        //------------------TRANSLATING-------------------------------
        public void Translate(MShape shape, float sx, float sy, float sz)
        {
            var movementMatrix = new Matrix4(new float[,] {
                { 1, 0, 0, sx },
                { 0, 1, 0, sy },
                { 0, 0, 1, sz },
                { 0, 0, 0, 1 }
            });

            //shape.ModelMatrix *= movementMatrix;
            shape.ModelMatrix = shape.ModelMatrix  * movementMatrix;

            //shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(movementMatrix, shape.ModelMatrix);
        }

        public void TranslateRange(List<MShape> shapes, float sx, float sy, float sz)
        {
            for (int i = 0; i < shapes.Count; ++i)
                Translate(shapes[i], sx, sy, sz);
        }

        //------------------SCALING-------------------------------
        public void Scale(MShape shape, float sx, float sy, float sz)
        {
            var scaleMatrix = new Matrix4(new float[,] {
                { sx, 0, 0, 0 },
                { 0, sy, 0, 0 },
                { 0, 0, sz, 0 },
                { 0, 0, 0, 1 }
            });

            shape.ModelMatrix *= scaleMatrix;

            //shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, scaleMatrix);
        }

        public void ScaleRange(List<MShape> shapes, float sx, float sy, float sz)
        {
            for (int i = 0; i < shapes.Count; ++i)
                Scale(shapes[i], sx, sy, sz);
        }

        private void RotateShape(MShape shape, double angle, System.Numerics.Vector3 axis)
        {
            double rads = angle * Math.PI / 180.0;

            Quaternion rotationQuaternion = GetRotationQuaternion(axis, rads);

            //shape.RotationQuaternion *= rotationQuaternion;
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
                var vertexVector = new System.Numerics.Vector3(vertices[i].Current.X, vertices[i].Current.Y, vertices[i].Current.Z);

                var newCoords = System.Numerics.Vector3.Transform(vertexVector, rotationQuaternion);

                SetNewCoordinatesToPoint(vertices[i], newCoords);
            }
        }

        private void MoveShapeToOrigin(MCommonPrimitive shape, out MPoint shapeCenter)
        {
            var vertices = shape.GetVertices();

            shapeCenter = shape.GetCenterPoint();

            for (int i = 0; i < vertices.Count; ++i)
            {
                vertices[i].Current.X -= shapeCenter.Current.X;
                vertices[i].Current.Y -= shapeCenter.Current.Y;
                vertices[i].Current.Z -= shapeCenter.Current.Z;
            }
        }

        private void MoveShapeToPreviousPosition(MCommonPrimitive shape, MPoint previousShapeCenter)
        {
            var vertices = shape.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {
                vertices[i].Current.X += previousShapeCenter.Current.X;
                vertices[i].Current.Y += previousShapeCenter.Current.Y;
                vertices[i].Current.Z += previousShapeCenter.Current.Z;
            }
        }

        private Quaternion GetRotationQuaternion(System.Numerics.Vector3 axis, double rads)
        {
            float quaternionX = (float)(Math.Sin(rads / 2) * axis.X),
                   quaternionY = (float)(Math.Sin(rads / 2) * axis.Y),
                   quaternionZ = (float)(Math.Sin(rads / 2) * axis.Z),
                   quaternionW = (float)(Math.Cos(rads / 2));

            return new Quaternion(quaternionX, quaternionY, quaternionZ, quaternionW);
        }

        private void SetNewCoordinatesToPoint(MPoint destinationPoint, System.Numerics.Vector3 newCoordinates)
        {
            destinationPoint.Current.X = newCoordinates.X;
            destinationPoint.Current.Y = newCoordinates.Y;
            destinationPoint.Current.Z = newCoordinates.Z;
        }

        //REWORK
        private void SetNewCoordinatesToPoint(MPoint destinationPoint, Matrix4 newCoordinates)
        {
            destinationPoint.Current.X = newCoordinates[0, 0];
            destinationPoint.Current.Y = newCoordinates[1, 0];
            destinationPoint.Current.Z = newCoordinates[2, 0];
            destinationPoint.Current.W = newCoordinates[3, 0];
        }

        private void SetNewCoordinatesToPoint(MPoint destinationPoint, float[,] newCoordinates)
        {
            destinationPoint.Current.X = newCoordinates[0, 0];
            destinationPoint.Current.Y = newCoordinates[1, 0];
            destinationPoint.Current.Z = newCoordinates[2, 0];
            destinationPoint.Current.W = newCoordinates[3, 0];
        }




        private void TransformShape(MShape shape)
        {
            List<MPoint> vertices = shape.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {

                var vertexCoords = new EmuMath.Structures.Vector4(
                    vertices[i].Source.X, vertices[i].Source.Y, vertices[i].Source.Z, vertices[i].Source.W
                    );

                var newCoords = shape.ModelMatrix * vertexCoords;

                //MatrixMultiplier.MultiplyMatrix(shape.ModelMatrix, vertexCoords);

                SetNewCoordinatesToPoint(vertices[i], newCoords);
            }
        }

        public void TransformShapes(List<MShape> shapes)
        {
            foreach (MShape shape in shapes)
                TransformShape(shape);
        }

        public void TransformComplex(MComplex complex, Camera camera)
        {
            complex.ApplyModelMatrixToChildren();

            for (int i = 0; i < complex.Primitives.Count; ++i)
                TransformPrimitive(complex.Primitives[i], camera);

            complex.RestoreChildrenModelMatrix();
        }

        public void TransformPrimitive(MCommonPrimitive primitive, Camera camera)
        {
            var vertices = primitive.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {
                var vertexCoords = new EmuMath.Structures.Vector4(
                    vertices[i].Source.X, vertices[i].Source.Y, vertices[i].Source.Z, vertices[i].Source.W
                    );

                //var modelViewMatrix = MatrixMultiplier.MultiplyMatrix(camera.ViewMatrix, shape.ModelMatrix);
                //var eyeCoordinates = MatrixMultiplier.MultiplyMatrix(modelViewMatrix, vertexCoords);
                //var clipCoordinates = MatrixMultiplier.MultiplyMatrix(camera.ProjectionMatrix, eyeCoordinates);
                var modelViewMatrix = GetModelViewMatrix(camera.ViewMatrix, primitive.ModelMatrix);
                var eyeCoordinates = GetEyeCoordinatesMatrix(modelViewMatrix, vertexCoords);
                var clipCoordinates = GetClipCoordinatesMatrix(camera.ProjectionMatrix, eyeCoordinates);

                var ndc = GetNormalizedDeviceCoordinatesVector(clipCoordinates);

                var windowCoordinates = GetWindowCoordinatesVector(ndc, new Screen(640, 360, 50, -50));

                SetNewCoordinatesToPoint(vertices[i], windowCoordinates);
            }
        }

        public void TransformShapes(List<MShape> shapes, Camera camera)
        {
            for (int i = 0; i < shapes.Count; ++i)
                TransformShape(shapes[i], camera);
        }

        public void TransformShape(MShape shape, Camera camera)
        {
            if (shape is MComplex)
                TransformComplex(shape as MComplex, camera);
            else
                TransformPrimitive(shape as MCommonPrimitive, camera);
        }

        public void TransformShapes(List<MComplex> shapes, Camera camera)
        {
            for (int i = 0; i < shapes.Count; ++i)
                TransformShape(shapes[i], camera);
        }

        public void TransformShape(MComplex shape, Camera camera)
        {
            var vertices = shape.GetVertices();

            for (int i = 0; i < vertices.Count; ++i)
            {
                var vertexCoords = new EmuMath.Structures.Vector4(
                    vertices[i].Source.X, vertices[i].Source.Y, vertices[i].Source.Z, vertices[i].Source.W
                    );

                var modelViewMatrix = GetModelViewMatrix(camera.ViewMatrix, shape.ModelMatrix);
                var eyeCoordinates = GetEyeCoordinatesMatrix(modelViewMatrix, vertexCoords);
                var clipCoordinates = GetClipCoordinatesMatrix(camera.ProjectionMatrix, eyeCoordinates);

                var ndc = GetNormalizedDeviceCoordinatesVector(clipCoordinates);

                var windowCoordinates = GetWindowCoordinatesVector(ndc, new Screen(640, 360, 50, -50));

                SetNewCoordinatesToPoint(vertices[i], windowCoordinates);
            }
        }


        private Matrix4 GetClipCoordinatesMatrix(Matrix4 projectionMatrix, Matrix4 eyeCoordinates)
        {
            return projectionMatrix * eyeCoordinates;
        }

        private Matrix4 GetModelViewMatrix(Matrix4 modelMatrix, Matrix4 viewMatrix)
        {
            return modelMatrix * viewMatrix;
        }

        //Change to Vector4 
        private Matrix4 GetEyeCoordinatesMatrix(Matrix4 modelViewMatrix, EmuMath.Structures.Vector4 vertexCoords)
        {
            return modelViewMatrix * vertexCoords;
        }

        //Change to Vector4 
        private Matrix4 GetNormalizedDeviceCoordinatesVector(Matrix4 clipCoordinates)
        {
            return new Matrix4(new float[,] {
                    { clipCoordinates[0, 0] / clipCoordinates[3, 0] },
                    { clipCoordinates[1, 0] / clipCoordinates[3, 0] },
                    { clipCoordinates[2, 0] / clipCoordinates[3, 0] },
                    { clipCoordinates[3, 0]}
                });
        }

        //Change to Vector4
        private Matrix4 GetWindowCoordinatesVector(Matrix4 ndc, Screen screen)
        {
            return new Matrix4(new float[,] {
                    { screen.Width / 2 * ndc[0, 0] + (screen.Width / 2) },
                    { screen.Height / 2 * ndc[1, 0] + (screen.Height / 2) },
                    { (screen.Far - screen.Near) / 2 * ndc[2, 0] + (screen.Far + screen.Near) / 2 },
                    { ndc[3, 0]}
                });
        }
    }
}
