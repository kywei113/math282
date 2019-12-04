using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix2019
{
    public class Matrix : AMatrix
    {
        #region Attributes
        //Set up a 2D array
        private double[,] dArray;   //When creating a double 2D array, you can use a [,] to actually build a 2D array instead of an array within an array
        #endregion

        #region Constructors
        public Matrix(double[,] dArray)
        {
            //Set the Row and COl properties
            //Note how the dimensions of the array are obtained in C#
            this.Rows = dArray.GetLength(0); //Rows
            this.Cols = dArray.GetLength(1); //Columns

            //Creating a deep copy of the matrix - allows modification of the matrix or the array without affecting the other
            this.dArray = new double[this.Rows, this.Cols];

            for(int i = 0; i < this.Rows;i++)
            {
                for (int j = 0; j < this.Cols;j++)
                {
                    this.dArray[i,j] = dArray[i,j];
                }
            }
        }

        //Copy Constructor - Uses constructor chaining
        public Matrix(Matrix m) : this(m.dArray)
        {

        }
        #endregion

        #region Implemented Methods
        public override double GetElement(int iRow, int iCol)
        {
            //Offset into the array as matrices start at 1,1 not 0,0
            return dArray[iRow - 1,iCol - 1];
        }

        public override void SetElement(int iRow, int iCol, double dValue)
        {
            dArray[iRow - 1, iCol - 1] = dValue;
        }

        internal override AMatrix NewMatrix(int iRows, int iCols)
        {
            //Create a new matrix with all values set to 0.0;
            return new Matrix(new double[iRows, iCols]);
        }
        #endregion

        #region Operator overloading
        //Notes that not all languages support operator overloading - (ie. Java does not)
        //Some languages will restrict the operators that can be overloaded
        //Some languages require that certain operators be overloaded in pairs

        public static IMatrix operator + (Matrix LeftOp, Matrix RightOp)
        {
            return LeftOp.Add(RightOp);
        }

        public static IMatrix operator - (Matrix LeftOp, Matrix RightOp)
        {
            return LeftOp.Subtract(RightOp);
        }

        public static IMatrix operator * (Matrix LeftOp, Matrix RightOp)
        {
            return LeftOp.Multiply(RightOp);
        }

        public static IMatrix operator * (Matrix LeftOp, double dScalar)
        {
            return LeftOp.ScalarMultiply(dScalar);
        }

        public static IMatrix operator * (double dScalar, Matrix RightOp)
        {
            return RightOp.ScalarMultiply(dScalar);
        }
        #endregion
    }
}
