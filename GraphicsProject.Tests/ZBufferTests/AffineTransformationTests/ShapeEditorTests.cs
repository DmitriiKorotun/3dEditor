using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZBuffer.ZBufferMath;
using ZBuffer.Shapes;
using ZBuffer.Affine_Transformation;

namespace GraphicsProject.Tests.ZBufferTests.AffineTransformationTests
{
    [TestClass]
    public class ShapeEditorTests
    {
        [TestMethod]
        public void RotateX_RotateShapeWith3_1_4CenterBy30DegreesAlongX_ExpectedTransformationMatrixReturned()
        {
            // arrange
            ShapeEditor editor = new ShapeEditor();

            MPoint shapeCenter = new MPoint(3, 1, 4);

            double angle = 30;

            float[,] expectedResult = { { 3 }, { (float)-1.13397455 }, { (float)3.96410155 }, { 1 } };

            // act
            var transformationMatrix = editor.RotateX(shapeCenter, angle);

            bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, transformationMatrix);

            // assert
            Assert.IsTrue(isMatrixesEqual);
        }

        [TestMethod]
        public void RotateY_RotateShapeWith3_1_4CenterBy30DegreesAlongY_ExpectedTransformationMatrixReturned ()
        {
            // arrange
            ShapeEditor editor = new ShapeEditor();

            MPoint shapeCenter = new MPoint(3, 1, 4);

            double angle = 30;

            float[,] expectedResult = { { (float)4.598076 }, { 1 }, { (float)1.96410155 }, { 1 } };

            // act
            var transformationMatrix = editor.RotateY(shapeCenter, angle);  

            bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, transformationMatrix);

            // assert
            Assert.IsTrue(isMatrixesEqual);
        }

        [TestMethod]
        public void RotateZ_RotateShapeWith3_1_4CenterBy30DegreesAlongZ_ExpectedTransformationMatrixReturned()
        {
            // arrange
            ShapeEditor editor = new ShapeEditor();

            MPoint shapeCenter = new MPoint(3, 1, 4);

            double angle = 30;

            float[,] expectedResult = { { (float)2.098076 }, { (float)2.36602545 }, { 4 }, { 1 } };

            // act
            var transformationMatrix = editor.RotateZ(shapeCenter, angle);

            bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, transformationMatrix);

            // assert
            Assert.IsTrue(isMatrixesEqual);
        }

        [TestMethod]
        public void Scale_ShapeCenterCoords_ExpectedShapeCenterCoordsIncreaseByOneAndHalf()
        {
            // arrange
            ShapeEditor editor = new ShapeEditor();

            MPoint shapeCenter = new MPoint(5, 12, 3);

            float[,] expectedResult = { { shapeCenter.SX * (float)1.5 }, { shapeCenter.SY * (float)1.5 }, { shapeCenter.SZ * (float)1.5 }, { 1 } };

            // act
            var transformationMatrix = editor.Scale(shapeCenter, (float)1.5, (float)1.5, (float)1.5);

            bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, transformationMatrix);

            // assert
            Assert.IsTrue(isMatrixesEqual);
        }
    }
}
