using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmuEngine.Shapes;
using KellermanSoftware.CompareNetObjects;

namespace GraphicsProject.Tests.EmuEngineTests.ShapesTests
{
    [TestClass]
    public class MSideCylinderTests
    {
        //[TestMethod]
        //public void MSideCylinder_AllValuesCorrect_MSideCylinderReturned()
        //{
        //    // arrange
        //    MPoint bottomCenter = new MPoint(0, 0, 0);

        //    float radius = 100,
        //        height = 50;

        //    // act
        //    MSideCylinder actualCylinder = new MSideCylinder(bottomCenter, radius, height);


        //    // assert
        //    CompareLogic compareLogic = new CompareLogic();

        //    //var vericesComparsionResult = compareLogic.Compare(expectedVertices, actualBox.Vertices);
        //    //var facetsComparsionResult = compareLogic.Compare(expectedFacets, actualBox.Facets);

        //    //Assert.IsTrue(vericesComparsionResult.AreEqual);
        //    //Assert.IsTrue(facetsComparsionResult.AreEqual);
        //    //Assert.AreEqual(width, actualBox.Width);
        //    //Assert.AreEqual(length, actualBox.Length);
        //    //Assert.AreEqual(height, actualBox.Height);
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Side cylinder with radius less than 0 was successfully created")]
        public void MSideCylinder_BotRadiusLessThan0_ArgumentOutOfRangeExceptionThrown()
        {
            // arrange
            MPoint bottomCenter = new MPoint(0, 0, 0);

            float radius = -1, height = 50;

            // act
            MSideCylinder actualCylinder = new MSideCylinder(bottomCenter, radius, height);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Side cylinder with height less than 0 was successfully created")]
        public void MSideCylinder_HeightLessThan0_ArgumentOutOfRangeExceptionThrown()
        {
            // arrange
            MPoint bottomCenter = new MPoint(0, 0, 0);

            float radius = 10, height = -1;

            // act
            MSideCylinder actualCylinder = new MSideCylinder(bottomCenter, radius, height);
        }
    }
}
