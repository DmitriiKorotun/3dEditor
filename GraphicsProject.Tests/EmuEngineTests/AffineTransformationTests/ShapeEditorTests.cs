using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmuEngine.EmuMath;
using EmuEngine.Shapes;
using EmuEngine.Affine_Transformation;
using KellermanSoftware.CompareNetObjects;

namespace GraphicsProject.Tests.EmuEngineTests.AffineTransformationTests
{
    //TODO Make test's names more obvious (input part is prioritized)
    [TestClass]
    public class ShapeEditorTests
    {
        [TestMethod]
        public void RotateX_MFacet_MFacetRotatedBy45DegreesAlongZ()
        {
            // arrange
            ShapeEditor editor = new ShapeEditor();

            MFacet testedFacet = new MFacet(new MPoint(50, 35, 0), new MPoint(100, 23, 0), new MPoint(72, 75, 0));

            MFacet expectedFacet = new MFacet(new MPoint(50, 35, 0), new MPoint(100, 23, 0), new MPoint(72, 75, 0));

            SetPointCoordinatesToDestinationPoint(new MPoint(50, 39.1005058, -9.899494), expectedFacet.Vertices[0]);
            SetPointCoordinatesToDestinationPoint(new MPoint(100, 30.6152229, -18.3847752), expectedFacet.Vertices[1]);
            SetPointCoordinatesToDestinationPoint(new MPoint(72, 67.38478, 18.3847752), expectedFacet.Vertices[2]);

            double angle = 45;


            // act
            editor.RotateX(testedFacet, angle);


            // assert
            CompareLogic compareLogic = new CompareLogic();

            var comparsionResult = compareLogic.Compare(expectedFacet, testedFacet);

            Assert.IsTrue(comparsionResult.AreEqual);
        }

        [TestMethod]
        public void RotateY_MFacet_MFacetRotatedBy45DegreesAlongZ()
        {
            // arrange
            ShapeEditor editor = new ShapeEditor();

            MFacet testedFacet = new MFacet(new MPoint(50, 35, 0), new MPoint(100, 23, 0), new MPoint(72, 75, 0));

            MFacet expectedFacet = new MFacet(new MPoint(50, 35, 0), new MPoint(100, 23, 0), new MPoint(72, 75, 0));

            SetPointCoordinatesToDestinationPoint(new MPoint(57.32233, 35, 17.67767), expectedFacet.Vertices[0]);
            SetPointCoordinatesToDestinationPoint(new MPoint(92.67767, 23, -17.67767), expectedFacet.Vertices[1]);
            SetPointCoordinatesToDestinationPoint(new MPoint(72.87868, 75, 2.12132025), expectedFacet.Vertices[2]);

            double angle = 45;


            // act
            editor.RotateY(testedFacet, angle);


            // assert
            CompareLogic compareLogic = new CompareLogic();

            var comparsionResult = compareLogic.Compare(expectedFacet, testedFacet);

            Assert.IsTrue(comparsionResult.AreEqual);
        }

        [TestMethod]
        public void RotateZ_MFacet_MFacetRotatedBy45DegreesAlongZ()
        {
            // arrange
            ShapeEditor editor = new ShapeEditor();

            MFacet testedFacet = new MFacet(new MPoint(50, 35, 0), new MPoint(100, 23, 0), new MPoint(72, 75, 0));

            MFacet expectedFacet = new MFacet(new MPoint(50, 35, 0), new MPoint(100, 23, 0), new MPoint(72, 75, 0));

            SetPointCoordinatesToDestinationPoint(new MPoint(67.2218246, 21.4228363, 0), expectedFacet.Vertices[0]);
            SetPointCoordinatesToDestinationPoint(new MPoint(111.062447, 48.2928925, 0), expectedFacet.Vertices[1]);
            SetPointCoordinatesToDestinationPoint(new MPoint(54.4939041, 65.26346, 0), expectedFacet.Vertices[2]);

            double angle = 45;


            // act
            editor.RotateZ(testedFacet, angle);


            // assert
            CompareLogic compareLogic = new CompareLogic();

            var comparsionResult = compareLogic.Compare(expectedFacet, testedFacet);

            Assert.IsTrue(comparsionResult.AreEqual);
        }

        [TestMethod]
        public void Move_MFacet_MFacetShiftedBy21AlongZ()
        {
            // arrange
            ShapeEditor editor = new ShapeEditor();

            MFacet testedFacet = new MFacet(new MPoint(12, 10, -3), new MPoint(80, 49, 0), new MPoint(34, 105, 4));

            MFacet expectedFacet = new MFacet(new MPoint(12, 10, -3), new MPoint(80, 49, 0), new MPoint(34, 105, 4));

            SetPointCoordinatesToDestinationPoint(new MPoint(12, 10, 18), expectedFacet.Vertices[0]);
            SetPointCoordinatesToDestinationPoint(new MPoint(80, 49, 21), expectedFacet.Vertices[1]);
            SetPointCoordinatesToDestinationPoint(new MPoint(34, 105, 25), expectedFacet.Vertices[2]);

            float distanceZ = 21;


            // act
            editor.Translate(testedFacet, 0, 0, distanceZ);


            // assert
            CompareLogic compareLogic = new CompareLogic();

            var comparsionResult = compareLogic.Compare(expectedFacet, testedFacet);

            Assert.IsTrue(comparsionResult.AreEqual);
        }

        [TestMethod]
        public void Move_MFacet_MFacetShiftedByMinus10AlongYAndBy208AlongX()
        {
            // arrange
            ShapeEditor editor = new ShapeEditor();

            MFacet testedFacet = new MFacet(new MPoint(26, 18, 20), new MPoint(156, 74, 12), new MPoint(85, 26, 0));

            MFacet expectedFacet = new MFacet(new MPoint(26, 18, 20), new MPoint(156, 74, 12), new MPoint(85, 26, 0));

            SetPointCoordinatesToDestinationPoint(new MPoint(234, 8, 20), expectedFacet.Vertices[0]);
            SetPointCoordinatesToDestinationPoint(new MPoint(364, 64, 12), expectedFacet.Vertices[1]);
            SetPointCoordinatesToDestinationPoint(new MPoint(293, 16, 0), expectedFacet.Vertices[2]);

            float distanceY = -10, distanceX = 208;


            // act
            editor.Translate(testedFacet, distanceX, distanceY, 0);


            // assert
            CompareLogic compareLogic = new CompareLogic();

            var comparsionResult = compareLogic.Compare(expectedFacet, testedFacet);

            Assert.IsTrue(comparsionResult.AreEqual);
        }

        //[TestMethod]
        //public void RotateX_RotateShapeWith3_1_4CenterBy30DegreesAlongX_ExpectedTransformationMatrixReturned()
        //{
        //    // arrange
        //    ShapeEditor editor = new ShapeEditor();

        //    MPoint shapeCenter = new MPoint(3, 1, 4);

        //    double angle = 30;

        //    float[,] expectedResult = { { 3 }, { (float)-1.13397455 }, { (float)3.96410155 }, { 1 } };

        //    // act
        //    var transformationMatrix = editor.RotateX(shapeCenter, angle);

        //    bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, transformationMatrix);

        //    // assert
        //    Assert.IsTrue(isMatrixesEqual);
        //}

        //[TestMethod]
        //public void RotateY_RotateShapeWith3_1_4CenterBy30DegreesAlongY_ExpectedTransformationMatrixReturned ()
        //{
        //    // arrange
        //    ShapeEditor editor = new ShapeEditor();

        //    MPoint shapeCenter = new MPoint(3, 1, 4);

        //    double angle = 30;

        //    float[,] expectedResult = { { (float)4.598076 }, { 1 }, { (float)1.96410155 }, { 1 } };

        //    // act
        //    var transformationMatrix = editor.RotateY(shapeCenter, angle);  

        //    bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, transformationMatrix);

        //    // assert
        //    Assert.IsTrue(isMatrixesEqual);
        //}

        //[TestMethod]
        //public void RotateZ_RotateShapeWith3_1_4CenterBy30DegreesAlongZ_ExpectedTransformationMatrixReturned()
        //{
        //    // arrange
        //    ShapeEditor editor = new ShapeEditor();

        //    MPoint shapeCenter = new MPoint(3, 1, 4);

        //    double angle = 30;

        //    float[,] expectedResult = { { (float)2.098076 }, { (float)2.36602545 }, { 4 }, { 1 } };

        //    // act
        //    var transformationMatrix = editor.RotateZ(shapeCenter, angle);

        //    bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, transformationMatrix);

        //    // assert
        //    Assert.IsTrue(isMatrixesEqual);
        //}

        //[TestMethod]
        //public void Scale_ShapeCenterCoords_ExpectedShapeCenterCoordsIncreaseByOneAndHalf()
        //{
        //    // arrange
        //    ShapeEditor editor = new ShapeEditor();

        //    MPoint shapeCenter = new MPoint(5, 12, 3);

        //    float[,] expectedResult = { { shapeCenter.SX * (float)1.5 }, { shapeCenter.SY * (float)1.5 }, { shapeCenter.SZ * (float)1.5 }, { 1 } };

        //    // act
        //    var transformationMatrix = editor.Scale(shapeCenter, (float)1.5, (float)1.5, (float)1.5);

        //    bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, transformationMatrix);

        //    // assert
        //    Assert.IsTrue(isMatrixesEqual);
        //}

        // I need to prevent point.SW from changing in order to compare points
        private void SetPointCoordinatesToDestinationPoint(MPoint sourcePoint, MPoint destinationPoint)
        {
            destinationPoint.X = sourcePoint.X;
            destinationPoint.Y = sourcePoint.Y;
            destinationPoint.Z = sourcePoint.Z;
        }

        //[TestMethod]
        //public void MoveShapeToOrigin_CubeWithAllPositiveCooridnatesMoreThatParameters_ExpectedTransformationMatrixReturned()
        //{
        //    // arrange
        //    ShapeEditor editor = new ShapeEditor();

        //    MPoint shapeCenter = new MPoint(3, 1, 4);

        //    double angle = 30;

        //    float[,] expectedResult = { { 3 }, { (float)-1.13397455 }, { (float)3.96410155 }, { 1 } };

        //    // act
        //    var transformationMatrix = editor.RotateX(shapeCenter, angle);

        //    bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, transformationMatrix);

        //    // assert
        //    Assert.IsTrue(isMatrixesEqual);
        //}
    }
}
