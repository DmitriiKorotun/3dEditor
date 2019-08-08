using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmuEngine.Shapes;
using KellermanSoftware.CompareNetObjects;

namespace GraphicsProject.Tests.EmuEngineTests.ShapesTests
{
    [TestClass]
    public class MTopCylinderTests
    {
        //[TestMethod]
        //public void MTopCylinder_AllValuesCorrect_MTopCylinderReturned()
        //{
        //    // arrange
        //    MPoint bottomCenter = new MPoint(0, 0, 0);

        //    float botRadius = 50, topRadius = 25, height = 50;

        //    // act
        //    MTopCylinder actualCylinder = new MTopCylinder(bottomCenter, botRadius, topRadius, height);


        //    // assert
        //    CompareLogic compareLogic = new CompareLogic();

        //    var vericesComparsionResult = compareLogic.Compare(expectedVertices, actualBox.Vertices);
        //    var facetsComparsionResult = compareLogic.Compare(expectedFacets, actualBox.Facets);

        //    Assert.IsTrue(vericesComparsionResult.AreEqual);
        //    Assert.IsTrue(facetsComparsionResult.AreEqual);
        //    Assert.AreEqual(width, actualBox.Width);
        //    Assert.AreEqual(length, actualBox.Length);
        //    Assert.AreEqual(height, actualBox.Height);
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Top cylinder with bottom radius less than 0 was successfully created")]
        public void MTopCylinder_BotRadiusLessThan0_ArgumentOutOfRangeExceptionThrown()
        {
            // arrange
            MPoint bottomCenter = new MPoint(0, 0, 0);

            float botRadius = -1, topRadius = 25, height = 50;

            // act
            MTopCylinder actualCylinder = new MTopCylinder(bottomCenter, botRadius, topRadius, height);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Top cylinder with top radius less than 0 was successfully created")]
        public void MTopCylinder_TopRadiusLessThan0_ArgumentOutOfRangeExceptionThrown()
        {
            // arrange
            MPoint bottomCenter = new MPoint(0, 0, 0);

            float botRadius = 50, topRadius = -1, height = 50;

            // act
            MTopCylinder actualCylinder = new MTopCylinder(bottomCenter, botRadius, topRadius, height);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Top cylinder with both radiuses less than 0 was successfully created")]
        public void MTopCylinder_BothRadiusesLessThan0_ArgumentOutOfRangeExceptionThrown()
        {
            // arrange
            MPoint bottomCenter = new MPoint(0, 0, 0);

            float botRadius = -2, topRadius = -1, height = 50;

            // act
            MTopCylinder actualCylinder = new MTopCylinder(bottomCenter, botRadius, topRadius, height);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Top cylinder with both radiuses less than 0 was successfully created")]
        public void MTopCylinder_HeightLessThan0_ArgumentOutOfRangeExceptionThrown()
        {
            // arrange
            MPoint bottomCenter = new MPoint(0, 0, 0);

            float botRadius = 10, topRadius = 15, height = -1;

            // act
            MTopCylinder actualCylinder = new MTopCylinder(bottomCenter, botRadius, topRadius, height);
        }
    }
}
