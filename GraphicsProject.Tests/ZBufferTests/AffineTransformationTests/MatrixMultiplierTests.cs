using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ZBuffer.ZBufferMath;

namespace GraphicsProject.Tests.ZBufferTests.AffineTransformationTests
{
    [TestClass]
    public class MatrixMultiplierTests
    {
        [TestMethod]
        public void MatrixMultiplication_AMultipliedByBCorrect_ExpectedResultReturned()
        {
            // arrange
            float[,] matrixA = { { 1, 2, 7 }, { 3, 2, 2 }, { 5, 0, 4 } };

            float[,] matrixB = { { 8, 8, 6 }, { 4, 0, 0 }, { 1, 3, 2 } };

            float[,] expectedResult = { { 23, 29, 20 }, { 34, 30, 22 }, { 44, 52, 38 } };

            // act
            float[,] ABMultiplicationResult = MatrixMultiplier.MultiplyMatrix(matrixA, matrixB);

            bool isMatrixesEqual = MatrixComparator.IsMatrixesEqual(expectedResult, ABMultiplicationResult);

            // assert
            Assert.IsTrue(isMatrixesEqual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException),
            "Matrixes with different amount of columns and rows was succesfully multiplied")]
        public void MatrixMultiplication_MatrixesAColumnsAndBRowsDoesntEqual_ExpectedArithmeticException()
        {
            // arrange
            float[,] matrixA = { { 1, 2, 7 }, { 3, 2, 2 } };

            float[,] matrixB = { { 8, 8 }, { 4, 0 } };

            // act
            float[,] ABMultiplicationResult = MatrixMultiplier.MultiplyMatrix(matrixA, matrixB);

            // assert
        }
    }
}