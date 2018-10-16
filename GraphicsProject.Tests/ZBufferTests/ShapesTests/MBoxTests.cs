using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZBuffer.Shapes;
using System.Linq;
using KellermanSoftware.CompareNetObjects;

namespace GraphicsProject.Tests.ZBufferTests.ShapesTests
{
    [TestClass]
    public class MBoxTests
    {
        [TestMethod]
        public void MBox_CorrectLeftCorner_MBoxReturned()
        {
            // arrange
            float width = 100, length = (float)58.3, height = 21;

            MPoint leftFaceCorner = new MPoint((float)21.5, (float)45.39, 4);

            MBox expectedBox = new MBox(width, length, height);
            
            // act
            MBox actualBox = new MBox(leftFaceCorner, width, length, height);

            // assert
            CompareLogic compareLogic = new CompareLogic();

            var comparsionResult = compareLogic.Compare(expectedBox, actualBox);

            Assert.IsTrue(comparsionResult.AreEqual);
        }
    }
}
