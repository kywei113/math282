using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// MATH 282 Assignment #3
/// Kyle Wei
/// CST234
/// Due: December 2, 2019
/// </summary>
namespace Matrix2019
{
    class Program
    {
        static double[,] dArray1 = { { 12.0, 3.0, 52.0 }, { -10.0, 45.0, 0.98 } };
        static double[,] dArray2 = { { 1.0, 3.0 }, { -1.0, 2.0 } };
        static double[,] dArray3 = { { -2.0, 2.0 }, { 7.0, 8.0 } };

        static void Main(string[] args)
        {
            //TestCreateMatrix();
            //TestAdd();
            //TestSubtract();
            //TestScalarMultiply();
            //TestMatrixMultiply();
            //TestOperatorOverloading();
            //TestGaussJordanElimination();
            //Console.ReadLine();
            TestLeastSquare();
        }

        static void TestLeastSquare()
        {
            double[,] dArray = { { -1, 0, 1, 2, 3 }, { 4, 2, 1, 2, 4 } };
            Matrix matrix = new Matrix(dArray);
            Matrix mSolution;

            Console.WriteLine(matrix);
            mSolution = (Matrix) matrix.LeastSquares(2);
            Console.WriteLine(mSolution);

            //Testing ToPolynomialString and ApplyLeastSquares
            Console.WriteLine(mSolution.ToPolynomialString());
            Console.WriteLine("Y = " + mSolution.ApplyLeastSquares(3));

            double[,] dArrayTwo = { {0,1,2,3,4,5 }, {1,1,2,4,5,6 } };
            Matrix mTwo = new Matrix(dArrayTwo);
            Console.WriteLine(mTwo);
            mSolution = (Matrix) mTwo.LeastSquares(2);
            Console.WriteLine(mSolution);
            
            //Testing ToPolynomialString and ApplyLeastSquares
            Console.WriteLine(mSolution.ToPolynomialString());
            Console.WriteLine("Y = " + mSolution.ApplyLeastSquares(3));

        }
        static void TestGaussJordanElimination()
        {
            double[,] dArrayA = { { 2, 2, 2, 6 }, { 4, 3, 4, 8 }, { 9, 3, 4, 7 } };
            IMatrix mA = new Matrix(dArrayA);
            Console.WriteLine(mA);

            IMatrix mSolution = mA.GaussJordanElimination();
            Console.WriteLine(mSolution);

            /*
             * 2x  + 3y = 7
             * -7x - 3y = 10
             * so x = -3.4, y = 4.6
             */
            double[,] dArrayB = { { 2, 3, 7 }, { -7, -3, 10 } };

            IMatrix mB = new Matrix(dArrayB);
            Console.WriteLine(mB);
            mSolution = mB.GaussJordanElimination();
            Console.WriteLine(mSolution);

            /* Example 3 from Equation Solving Practice */
            double[,] dArrayC = { { 2, 4, 6, 16 }, { 3, 6, 10, 29 }, { -2, 5, 12, 29 } };

            IMatrix mC = new Matrix(dArrayC);
            Console.WriteLine(mC);
            mSolution = mC.GaussJordanElimination();
            Console.WriteLine(mSolution);



            /*Example 5 from Equation Solving practice */
            double[,] dArrayD = { {3,3,-6,-12 }, {5,9,4,22 }, {1,4,8.5,27.5 } };
            IMatrix mD = new Matrix(dArrayD);
            Console.WriteLine(mD);
            try
            {
                mSolution = mD.GaussJordanElimination();

                Console.WriteLine(mSolution);
            }
            catch (Exception e)
            {
                Console.WriteLine("No Solution");
            }


            /* Example 4 */
            double[,] dArrayFour = { { -1, -1, 1, 0 }, { 1, 10, 100, -1 }, { 15, 15, 15, -1 }, { 100, 10, 1, -1 } };
            IMatrix mFour = new Matrix(dArrayFour);
            Console.WriteLine(mFour);
            try
            {
                mSolution = mFour.GaussJordanElimination();

                Console.WriteLine(mSolution);
            }
            catch (Exception e)
            {
                Console.WriteLine("No Solution");
            }


            Console.WriteLine("Least Squares");
            double[,] dArrayLeastSquare = { {5,5,15,13 },{5,15,35,13 },{15,35,99,49} };
            IMatrix mLeastSquare = new Matrix(dArrayLeastSquare);
            Console.WriteLine(mLeastSquare);
            try
            {
                mSolution = mLeastSquare.GaussJordanElimination();

                Console.WriteLine(mSolution);
            }
            catch (Exception e)
            {
                Console.WriteLine("No Solution");
            }


        }

        static void TestMatrixMultiply()
        {
            IMatrix m1 = new Matrix(dArray1);
            IMatrix m2 = new Matrix(dArray2);
            IMatrix m3 = new Matrix(dArray3);

            Console.WriteLine("Testing matrix multiplication");
            IMatrix mProduct = m2.Multiply(m3);
            Console.WriteLine(mProduct);

            mProduct = m2.Multiply(m1);
            Console.WriteLine(mProduct);

            try
            {
                mProduct = m1.Multiply(m2);
                Console.WriteLine(mProduct);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n");
            }
        }

        static void TestCreateMatrix()
        {
            Console.WriteLine("Testing creating matrices");

            Matrix m = new Matrix(dArray1);
            dArray1[0, 0] = 0.0; // shouldn't affect m
                                 // since our constructor makes a deep copy of the array

            Console.WriteLine("Creating matrix from array:");
            Console.WriteLine(m.ToString());

            Console.WriteLine("Creating matrix from another matrix (copy constructor):");
            IMatrix m2 = new Matrix((Matrix)m); // instead of (Matrix)m.Clone();
            Console.WriteLine(m2);

            dArray1[0, 0] = 12.0; // set it back so it doesn't affect other tests

            try
            {
                m = new Matrix(new double[0, 0]);
                Console.WriteLine(m);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n");
            }
        }

        static void TestOperatorOverloading()
        {
            Matrix m1 = new Matrix(dArray1);

            Console.WriteLine("Testing Operator Overload\n");
            Console.WriteLine(m1);
            Console.WriteLine(m1 * 0.5);
            Console.WriteLine(10 * m1);
            Console.WriteLine(m1 - m1);
            Console.WriteLine(m1 + m1);

        }

        static void TestAdd()
        {
            IMatrix m1 = new Matrix(dArray1);
            IMatrix m2 = new Matrix(dArray2);
            IMatrix m3 = new Matrix(dArray3);

            Console.WriteLine("Testing matrix addition");
            //IMatrix mSum = m2.Add(m3);
            IMatrix mSum = (Matrix)m2 + (Matrix)m3;
            Console.WriteLine(mSum);

            try
            {
                mSum = m1.Add(m2);
                Console.WriteLine(mSum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n");
            }
        }

        static void TestSubtract()
        {
            IMatrix m1 = new Matrix(dArray1);
            IMatrix m2 = new Matrix(dArray2);
            IMatrix m3 = new Matrix(dArray3);

            Console.WriteLine("Testing matrix subtraction");
            IMatrix mSum = m2.Subtract(m3);
            Console.WriteLine(mSum);

            try
            {
                mSum = m1.Subtract(m2);
                Console.WriteLine(mSum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n");
            }
        }

        static void TestScalarMultiply()
        {
            IMatrix m1 = new Matrix(dArray1);
            IMatrix m2 = new Matrix(dArray2);

            Console.WriteLine("Testing scalar multiplication");
            IMatrix mResult = m2.ScalarMultiply(10.0);
            Console.WriteLine(mResult);

            mResult = m1.ScalarMultiply(0.5);
            Console.WriteLine(mResult);
        }

    }
}
