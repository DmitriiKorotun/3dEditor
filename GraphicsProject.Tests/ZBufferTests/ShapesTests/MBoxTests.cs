using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZBuffer.Shapes;
using System.Linq;
using KellermanSoftware.CompareNetObjects;
using System.Collections.Generic;

namespace GraphicsProject.Tests.ZBufferTests.ShapesTests
{
    [TestClass]
    public class MBoxTests
    {
        [TestMethod]
        public void MBox_CorrectLeftCornerWidthHeightLength_MBoxReturned()
        {
            // arrange
            float width = 100,
                length = (float)58.3,
                height = 50;

            var expectedVertices = GetVertices(width, length, height);
            var expectedFacets = GetFacets(expectedVertices);

            MPoint leftFaceCorner = new MPoint((float)21.5, (float)45.39, 4);


            // act
            MBox actualBox = new MBox(leftFaceCorner, length, width, height);


            // assert
            CompareLogic compareLogic = new CompareLogic();

            var vericesComparsionResult = compareLogic.Compare(expectedVertices, actualBox.Vertices);
            var facetsComparsionResult = compareLogic.Compare(expectedFacets, actualBox.Facets);

            Assert.IsTrue(vericesComparsionResult.AreEqual);
            Assert.IsTrue(facetsComparsionResult.AreEqual);
            Assert.AreEqual(width, actualBox.Width);
            Assert.AreEqual(length, actualBox.Length);
            Assert.AreEqual(height, actualBox.Height);
        }

        private MPoint[] GetVertices(float width, float length, float height)
        {
            return new MPoint[] {
                new MPoint((float)21.5, (float)45.39, 4), new MPoint((float)21.5 + length, (float)45.39, 4),
                new MPoint((float)21.5 + length, (float)45.39 + width, 4), new MPoint((float)21.5, (float)45.39 + width, 4),
                new MPoint((float)21.5, (float)45.39, 4 + height), new MPoint((float)21.5 + length, (float)45.39, 4 + height),
                new MPoint((float)21.5 + length, (float)45.39 + width, 4 + height), new MPoint((float)21.5, (float)45.39 + width, 4 + height),
            };
        }

        private MFacet[] GetFacets(MPoint[] vertices)
        {
            return new MFacet[] {
                // Bottom rectangle
                new MFacet(vertices[0], vertices[1], vertices[2]),
                new MFacet(vertices[0], vertices[2], vertices[3]),

                // Front rectangle
                new MFacet(vertices[0], vertices[1], vertices[5]),
                new MFacet(vertices[0], vertices[4], vertices[5]),

                // Left rectangle
                new MFacet(vertices[0], vertices[3], vertices[7]),
                new MFacet(vertices[0], vertices[4], vertices[7]),

                // Right rectangle
                new MFacet(vertices[6], vertices[5], vertices[2]),
                new MFacet(vertices[1], vertices[5], vertices[2]),

                // Rear rectangle
                new MFacet(vertices[6], vertices[7], vertices[3]),
                new MFacet(vertices[6], vertices[2], vertices[3]),

                // Top rectangle
                new MFacet(vertices[6], vertices[5], vertices[4]),
                new MFacet(vertices[6], vertices[7], vertices[4])
            };
        }
    }
}
