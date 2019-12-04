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
/// Assignment additions found in ToPolynomialString() and ApplyLeastSquares(double x) methods
/// </summary>
namespace Matrix2019
{
    public abstract class AMatrix : IMatrix
    {
        #region Attributes
        private int iRows;
        private int iColumns;

        public int Rows
        {
            get { return iRows; }
            set
            {
                if (value < 1)
                {
                    throw new Exception("Rows must be 1 or greater");
                }
                else
                {
                    iRows = value;
                }
            }
        }

        public int Cols
        {
            get { return iColumns; }
            set
            {
                if (value < 1)
                {
                    throw new Exception("columns must be 1 or greater");
                }
                else
                {
                    iColumns = value;
                }
            }
        }
        #endregion

        #region Abstract Methods
        public abstract double GetElement(int iRow, int iCol);
        public abstract void SetElement(int iRow, int iCol, double dValue);


        internal abstract AMatrix NewMatrix(int iRows, int iCols);  //Creates a matrix full of 0's
        #endregion


        #region Math Methods
        public IMatrix Add(IMatrix mRight)
        {
            
            AMatrix RightOp = (AMatrix) mRight;

            //if operands are the same size
            if(this.Rows == RightOp.Rows && this.Cols == RightOp.Cols)
            {
                AMatrix LeftOp = CreateLeftOp();
                AMatrix Sum = NewMatrix(LeftOp.Rows, LeftOp.Cols);

                for(int r = 1; r <= LeftOp.Rows; r++)
                {
                    for(int c = 1; c <=LeftOp.Cols; c++)
                    {
                        double dVal = LeftOp.GetElement(r, c) + RightOp.GetElement(r, c);

                        Sum.SetElement(r, c, dVal);
                    }
                }
                return Sum;
            }
            else
            {
                throw new Exception("Operands must be the same size");
            }
        }

        public IMatrix Multiply(IMatrix mRight)
        {
            AMatrix LeftOp = this;
            AMatrix RightOp = (AMatrix)mRight;
            AMatrix results = null;

            if (this.Cols != RightOp.Rows)
            {
                throw new Exception("Can not multiply  -  incorrect dimensions");
            }

            results = NewMatrix(LeftOp.Rows, RightOp.Rows);

            for (int x = 1; x <= results.Rows; x++)
            {
                for (int y = 1; y <= results.Cols; y++)
                {
                    double SumOfProd = 0;

                    for (int resultLocation = 1; resultLocation <= LeftOp.Cols; resultLocation++)
                    {
                        SumOfProd = SumOfProd + LeftOp.GetElement(x, resultLocation) * RightOp.GetElement(resultLocation, y);
                    }
                    results.SetElement(x, y, SumOfProd);
                }
            }

            return results;
        }

        public IMatrix ScalarMultiply(double dScalar)
        {
            AMatrix ProdMatrix = NewMatrix(this.Rows, this.Cols);

            for(int r = 1; r <= this.Rows; r++)
            {
                for(int c = 1; c <= this.Cols; c++)
                {
                    double dProd = dScalar * this.GetElement(r, c);
                    ProdMatrix.SetElement(r, c, dProd);
                }
            }

            return ProdMatrix;

        }

        public IMatrix Subtract(IMatrix mRight)
        {
            //AMatrix RightOp = (AMatrix) mRight;

            //if(this.Rows == RightOp.Rows && this.Cols == RightOp.Cols)
            //{
            //    AMatrix LeftOp = CreateLeftOp();
            //    AMatrix Difference = NewMatrix(LeftOp.Rows, LeftOp.Cols);

            //    for(int r = 1; r <= LeftOp.Rows; r++)
            //    {
            //        for(int c = 1; c <= LeftOp.Cols; c++)
            //        {
            //            double dDiff = LeftOp.GetElement(r, c) - RightOp.GetElement(r, c);
            //            Difference.SetElement(r, c, dDiff);
            //        }
            //    }

            //    return Difference;
            //}
            //else
            //{
            //    throw new Exception("Operands must be the same size");
            //}

            //The smart way of doing this. Subtracting is adding the negative of each value
            return this.Add(mRight.ScalarMultiply(-1.0));
        }
        #endregion

        #region Utility Methods
        private AMatrix CreateLeftOp()
        {
            AMatrix LeftOp = NewMatrix(this.Rows, this.Cols);

            for(int r = 1; r <= this.Rows; r++)
            {
                for(int c = 1; c <= this.Cols; c++)
                {
                    LeftOp.SetElement(r, c, this.GetElement(r, c));
                }
            }

            return LeftOp;
        }
        public string ToPlainString()
        {
            StringBuilder s = new StringBuilder();

            for(int r = 1; r <= this.Rows; r++)
            {
                for (int c = 1; c <= this.Cols; c++)
                {
                    s.Append(this.GetElement(r, c) + "\t");
                }
                s.Append("\n");
            }
            return s.ToString();
        }

        public override string ToString()
        {
            string s = "";
            char cUL = (char)0x250C;
            char cUR = (char)0x2510;
            char cLL = (char)0x2514;
            char cLR = (char)0x2518;
            char cVLine = (char)0x2502;

            //build the top row
            s += cUL;
            for (int j = 1; j <= this.Cols; j++)
            {
                //s += "\t\t";
                s += "\t";
            }
            s += cUR + "\n";

            //build the data rows
            for (int i = 1; i <= this.Rows; i++)
            {
                s += cVLine;
                for (int j = 1; j <= this.Cols; j++)
                {
                    if (this.GetElement(i, j) >= 0)
                    {
                        s += " ";
                    }
                    //s += String.Format("{0:0.0000 e+00}", this.GetElement(i, j)) + "\t";
                    s += this.GetElement(i, j) + "\t";

                }
                s += cVLine + "\n";
            }
            //Build the bottom row
            s += cLL;
            for (int j = 1; j <= this.Cols; j++)
            {
                //s += "\t\t";
                s += "\t";

            }
            s += cLR + "\n";
            return s;
        }

        /// <summary>
        /// Creates an equation string for the current matrix. 
        /// </summary>
        /// <returns>String representing the polynomial equation from the given matrix</returns>
        public String ToPolynomialString()
        {
            for (int i = 0; i < this.Rows; i++)
            {
                //Augmented matrix for a polynomial equation should have one more column than row, 
                //and diagonal elements should be all 1's
                //Throws an error if conditions are not met
                if (this.Cols - this.Rows <= 1 && this.GetElement(i + 1, i + 1) != 1)    
                {
                    throw new Exception("Not a valid Least-Square Matrix");
                }
            }

            String polyString = "y = ";
            String sVariable = "";
            String sSymbol = "";
            double dVal = 0.0;

            for(int i = 0; i < this.Rows; i++)
            {
                dVal = this.GetElement(i + 1, this.Cols);   //Gets value at end of the matrix row. Value will be the variable value

                sSymbol = i == 0 ? "" : dVal < 0 ? "- " : "+ ";     //Determine positive/negative value. Does not assign anything if looking at first value/row
                sVariable = i > 0 ? "x^" + i + " ": " ";            //Determine X degree to add. Only adds if on row after row 1

                polyString += sSymbol + dVal + sVariable;           //Concatenate string together
            }

            return polyString;
        }

        #endregion

        #region GaussJordanElimination
        public IMatrix GaussJordanElimination()
        {
            if(this.Rows + 1 != this.Cols)
            {
                throw new Exception("Augmented Matrix must have 1 more column than row");
            }

            AMatrix mSolution = new Matrix((Matrix)this);

            /*
             *  For each pivot in the solution matrix (i)
             *      Store a copy of the current pivot element
             *      For each element (j) in the current pivot row, divide by the pivot value
             *      For each row (k) in the soluton matrix
             *          If the current row is not the pivot row
             *              Get the multiplying factor
             *              For each element (j) in the current row
             *                  Current <-- Current element + multiplying factor * corresponding element in pivot
             *                  Current location in the current row <-- Current
             */

            for(int i = 1; i<= mSolution.Rows;i++)
            {
                mSolution.SystemSolvable(i);
                double dPivot = mSolution.GetElement(i, i);
                for(int j = 1; j <= mSolution.Cols; j++)
                {
                    double dCurrent = mSolution.GetElement(i, j) / dPivot;
                    mSolution.SetElement(i, j, dCurrent);
                }

                for(int k = 1; k <= mSolution.Rows; k++)
                {
                    if(k != i)
                    {
                        double dFactor = -1.0 * mSolution.GetElement(k, i);

                        for(int j = 1; j<= mSolution.Cols;j++)
                        {
                            double dCurrent = mSolution.GetElement(k, j) + (dFactor) * mSolution.GetElement(i, j);
                            mSolution.SetElement(k, j, dCurrent);
                        }
                    }
                }
            }

            return mSolution;
        }

        /// <summary>
        /// Checks the solution matrix to see if the system is solveable.
        /// If so, the method will ensure that the pivot is a non-zero value
        /// by swapping the rows if necessary.
        /// If not, the system is not solveable and an exception is thrown
        /// </summary>
        /// <param name="i"></param>
        private void SystemSolvable(int i)
        {
            /*
             * CurrentPivot <-- get the current pivot
             * NextRow <-- Current row to check for a non-zero pivot
             * 
             * While the CurrentPivot equals 0, there are still rows to check
             *      CurrentPivot <-- Possible pivot from this row
             *      If CurrentPivot is not 0
             *          Swap the pivot row (i) with the current row (NextRow)
             *      else
             *          Check the next row
             *  If a non-zero pivot wasn't found
             *      Indicate an error
             */

            double dCurrentPivot = this.GetElement(i, i);
            int iNextRow = i + 1;

            while(dCurrentPivot == 0 && iNextRow <= this.Rows)
            {
                dCurrentPivot = this.GetElement(iNextRow, i);
                if(dCurrentPivot != 0.0)
                {
                    //swap rows
                    for(int j = 1; j <= this.Cols; j++)
                    {
                        double dTemp = this.GetElement(i, j);
                        this.SetElement(i, j, this.GetElement(iNextRow, j));
                        this.SetElement(iNextRow, j, dTemp);
                    }
                }
                else
                {
                    iNextRow++;
                }
            }

            if(dCurrentPivot == 0.0)
            {
                throw new Exception("System is not solveable");
            }

        }


        #endregion

        #region LeastSquares
        public IMatrix LeastSquares(int iDegree)
        {
            /*
            Coding Least Squares:
            Given an invoking matrix of size n x 2, where n= # of data points and n >= m+1
            Determine # of x and y sums
            Create an augmented matrix of size (m+1) x (m+2)
            Calculate the x-sums and y-sums
            Put the x-sums and y-sums into the augmented matrix
            Perform Gaussian elimination on the augmented matrix
            */

            int numXSums = 2 * iDegree + 1;
            int numYSums = iDegree + 1;

            Matrix augMatrix = new Matrix(new double[iDegree + 1, iDegree + 2]);

            int n = this.Rows;

            double xSum = 0;
            double ySum = 0;

            for(int j = 1; j <= augMatrix.Rows; j++)
            {
                double deg = j-1;
                for(int k = 1; k <= augMatrix.Cols; k++)
                {
                    if(k != augMatrix.Cols)
                    {
                        augMatrix.SetElement(j, k, SumXPow(deg++));
                    }
                    else
                    {
                        augMatrix.SetElement(j, k, (j-1 ==0) ? SumY() : SumXY(j-1));
                    }
                }
            }

            return augMatrix.GaussJordanElimination();
        }

        private double SumXPow(double degree)
        {
            double sum = 0.0;
            
            for(int i = 1; i <= this.Cols; i++)
            {
                sum += Math.Pow(this.GetElement(1, i), degree);
            }

            return sum;
        }

        private double SumY()
        {
            double sum = 0.0;

            for (int i = 1; i <= this.Cols; i++)
            {
                sum += this.GetElement(2, i);
            }

            return sum;
        }
        
        private double SumXY(double degree)
        {
            double sum = 0.0;
            double xVal = 0.0;
            double yVal = 0.0;

            for(int i = 1; i <= this.Cols; i++)
            {
                xVal = this.GetElement(1, i);
                yVal = this.GetElement(2, i);
                sum += Math.Pow(xVal, degree) * yVal;
            }

            return sum;
        }

        private double SumXDegY(double degree)
        {
            double sum = 0.0;
            double xVal = 0.0;
            double yVal = 0.0;

            return sum;
        }

        /// <summary>
        /// Applies an X value to a given Least-Square matrix. Solves for the Y value of the matrix's equation
        /// </summary>
        /// <param name="x">Value to use for X in equation</param>
        /// <returns>Value of Y</returns>
        public double ApplyLeastSquares(double x)
        {
            for (int i = 0; i < this.Rows; i++)
            {
                //Augmented matrix for a polynomial equation should have one more column than row, 
                //and diagonal elements should be all 1's
                //Throws an error if conditions are not met
                if (this.Cols - this.Rows <= 1 && this.GetElement(i + 1, i + 1) != 1)
                {
                    throw new Exception("Not a valid Least-Square Matrix");
                }
            }

            double dResultY = 0.0;      //Tracking overall result for y  
            double dPowResult = 0.0;    //Tracking result of value^power
            for (int i = 0; i < this.Rows; i++)
            {
                dPowResult = Math.Pow(x, i) * this.GetElement(i + 1, this.Cols);    //Calculates the result from x^i * rowValue
                dResultY += dPowResult < 0 ? -1 * dPowResult : dPowResult;          //Checks if dPowResult is negative, multiplies by -1 to flip to positive number if it is for proper addition
            }

            return dResultY;
        }
        #endregion

    }
}
