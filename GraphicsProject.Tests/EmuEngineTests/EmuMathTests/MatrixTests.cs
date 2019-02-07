using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmuEngine.EmuMath.Structures;

namespace GraphicsProject.Tests.EmuEngineTests.EmuMathTests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Matrix_CreatedFromFloatArray_SimilarToFloatArrayExpected()
        {
            // arrange
            var expected = new float[,] { 
                {2, 7, 1, 9}, 
                {3.5f, 200.576f, 0, -19}, 
                {0.4857f, -1.245f, 0.104423f, 10}, 
                {94, -175, -0.630f, -2 }
            };

            // act
            var Matrix = new Matrix(expected);

            bool isEqual = IsEqual(expected, Matrix);

            // assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Matrix_CreatedFromMatrix_SimilarToSourceMatrixExpected()
        {
            // arrange
            var source = new float[,] {
                {2, 7, 1, 9},
                {3.5f, 200.576f, 0, -19},
                {0.4857f, -1.245f, 0.104423f, 10},
                {94, -175, -0.630f, -2 }
            };

            var expected = new Matrix(source);

            // act
            var copied = new Matrix(expected);

            bool isEqual = IsEqual(expected, copied);

            // assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void IsEqual_EqualMatrices_TrueExpected()
        {
            // arrange
            var source = new float[,] {
                {5, 17, -8, 0},
                {3.2f, -2, 0, 0},
                {42.487f, -1.245f, 0.104423f, 10},
                {0, -175, -0.630f, -2 }
            };

            var m1 = new Matrix(source);
            var m2 = new Matrix(source);

            // act
            var isEqual = m1 == m2;

            // assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void IsEqual_UnequalMatrices_FalseExpected()
        {
            // arrange
            var m1Source = new float[,] {
                {5, 17, -8, 0},
                {3.2f, -2, 0, 0},
                {42.487f, -1.245f, 0.104423f, 10},
                {0, -175, -0.630f, -2 }
            };

            var m2Source = new float[,] {
                {5, 17, -8, 0},
                {3.2f, -2, 0, 0},
                {42.487f, -1.245f, 0.104423f, 10},
                {0, -175, -0.630f, -1 }
            };

            var m1 = new Matrix(m1Source);
            var m2 = new Matrix(m2Source);

            // act
            var isEqual = m1 == m2;

            // assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void IsUnequal_EqualMatrices_FalseExpected()
        {
            // arrange
            var source = new float[,] {
                {5, 17, -8, 0},
                {3.2f, -2, 0, 0},
                {42.487f, -1.245f, 0.104423f, 10},
                {0, -175, -0.630f, -2 }
            };

            var m1 = new Matrix(source);
            var m2 = new Matrix(source);

            // act
            var isUnequal = m1 != m2;

            // assert
            Assert.IsFalse(isUnequal);
        }

        [TestMethod]
        public void IsUnequal_UnequalMatrices_TrueExpected()
        {
            // arrange
            var m1Source = new float[,] {
                {5, 17, -8, 0},
                {3.2f, -2, 0, 0},
                {42.487f, -1.245f, 0.104423f, 10},
                {0, -175, -0.630f, -2 }
            };

            var m2Source = new float[,] {
                {5, 17, -8, 0},
                {3.2f, -2, 0, 0},
                {42.487f, -1.245f, 0.104423f, 10},
                {0, -175, -0.630f, -1 }
            };

            var m1 = new Matrix(m1Source);
            var m2 = new Matrix(m2Source);

            // act
            var isUnequal = m1 != m2;

            // assert
            Assert.IsTrue(isUnequal);
        }

        [TestMethod]
        public void Multiply_Sign_MatricesWithEqualRowsAndColumnsNumber_MatrixWithSameRowsAndColumnsNumberExpected()
        {
            // arrange
            var m1Source = new float[,] {
                {5, 17, -8, 0},
                {3.2f, -2, 0, 0},
                {42.487f, -1.245f, 0.104423f, 10},
                {0, -175, -0.630f, -2 }
            };

            var m2Source = new float[,] {
                {2, 7, 1, 9},
                {3.5f, 200.576f, 0, -19},
                {0.4857f, -1.245f, 0.104423f, 10},
                {94, -175, -0.630f, -2 }
            };

            var expectedSource = new float[,] {
                {65.6144f, 3454.752f, 4.164616f, -358},
                {-0.5999999f, -378.752f, 3.2f, 66.8f},
                {1020.66724f, -1702.43811f, 36.1979027f, 387.0822f},
                {-800.805969f, -34750.0156f, 1.19421351f, 3322.7f }
            };

            var m1 = new Matrix(m1Source);
            var m2 = new Matrix(m2Source);
            var expected = new Matrix(expectedSource);

            // act
            Matrix result = m1 * m2;

            var isEqual = IsEqual(result, expected);

            // assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Multiply_Sign_FirstMatrixWithMoreRowsThanSecond_MatrixWithFirstRowsAndSecondColumnsNumberExpected()
        {
            // arrange
            var m1Source = new float[,] {
                {5, 17, -8},
                {3.2f, -2, 0},
                {42.4f, -1.25f, 0.12f},
                {0, -175, -0.63f}
            };

            var m2Source = new float[,] {
                {2, 7, 1},
                {3.5f, 200.65f, 0},
                {0.48f, -1.24f, 0.1f}
            };

            var expectedSource = new float[,] {
                    {65.66f, 3455.96973f, 4.2f},
                    {-0.5999999f, -378.9f, 3.2f},
                    {80.482605f, 45.838726f, 42.4120026f},
                    {-612.8024f, -35112.97f, -0.063f}
                };

            var m1 = new Matrix(m1Source);
            var m2 = new Matrix(m2Source);

            var expected = new Matrix(expectedSource);

            // act
            Matrix result = m1 * m2;

            var isEqual = IsEqual(result, expected);

            // assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Multiply_Sign_SecondMatrixWithMoreColumnsThanFirst_MatrixWithFirstRowsAndSecondColumnsNumberExpected()
        {
            //arrange
            var m1Source = new float[,] {
                {5, 0, -8},
                {3.2f, -2, 0},
                {-7, 0.5f, -4}
           };

            var m2Source = new float[,] {
                {2, 7, 1, -5.5f},
                {3.5f, 1.3f, 1, 2.4f},
                {0, 4.9f, 8, -10}
            };

            var expectedSource = new float[,] {
                    {10, -4.200001f, -59, 52.5f},
                    {-0.5999999f, 19.8f, 1.2f, -22.4000015f},
                    {-12.25f, -67.95f, -38.5f, 79.7f}
                };

            var m1 = new Matrix(m1Source);
            var m2 = new Matrix(m2Source);

            var expected = new Matrix(expectedSource);

            //act
            Matrix result = m1 * m2;

            var isEqual = IsEqual(result, expected);

            //assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Matrix with more columns was inappropriately multiplied by second matrix with less columns number.")]
        public void Multiply_Sign_FirstMatrixWithMoreColumnsThanSecondsRows_ArgumentExceptionExpected()
        {
            // arrange
            var m1Source = new float[,] {
                {7, 15, 5.6f, 7.36f},
                {2, 1, -0.5f, -17}
            };

            var m2Source = new float[,] {
                {3.74f, 0.2f, 1, 9},
                {-8, -8.1f, 0, -19},
                {0.4857f, -1.245f, 0.104423f, 10}
            };

            var m1 = new Matrix(m1Source);
            var m2 = new Matrix(m2Source);

            // act
            Matrix result = m1 * m2;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Matrix with more columns was inappropriately multiplied by second matrix with less columns number.")]
        public void Multiply_Sign_SecondMatrixWithMoreRowsThanFirstsColumns_ArgumentExceptionExpected()
        {
            // arrange
            var m1Source = new float[,] {
                {7, 15, 5.6f},
                {2, 1, -0.5f},
                {4.25f, -9, 0}
            };

            var m2Source = new float[,] {
                {3.74f, 0.2f, 1},
                {-8, -8.1f, 0},
                {0.4f, -1.24f, 0.104f},
                {0.6f, 1, 1}
            };

            var m1 = new Matrix(m1Source);
            var m2 = new Matrix(m2Source);

            // act
            Matrix result = m1 * m2;
        }

        private bool IsEqual(float[,] m0, Matrix m1)
        {
            bool isEqual = true;

            int rows = m0.GetLength(0), columns = m0.GetLength(1);

            if (m1.Size.Rows != rows || m1.Size.Columns != columns)
                isEqual = false;
            else
            {
                for (int i = 0; i < m0.GetLength(0); ++i)
                {
                    for (int j = 0; j < m0.GetLength(1); ++j) {
                        if (m0[i, j] != m1[i, j])
                        {
                            isEqual = false;
                            break;
                        }
                    }

                    if (!isEqual)
                        break;
                }
            }

            return isEqual;
        }

        private bool IsEqual(Matrix m0, Matrix m1)
        {
            bool isEqual = true;

            if (m0.Size.Rows != m1.Size.Rows || m0.Size.Columns != m1.Size.Columns)
                isEqual = false;
            else
            {
                for (int i = 0; i < m0.Size.Rows; ++i)
                {
                    for (int j = 0; j < m0.Size.Columns; ++j)
                    {
                        if (m0[i, j] != m1[i, j])
                        {
                            isEqual = false;
                            break;
                        }
                    }

                    if (!isEqual)
                        break;
                }
            }

            return isEqual;
        }
    }
}
