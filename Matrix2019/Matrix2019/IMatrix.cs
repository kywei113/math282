using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix2019
{
    //The IMatrix interface describes what must be implemented in any derived classes
    //Cannot instantiate an interface
    //Cannot implement functions in the interface
    //Cannot have members within the interface
    //
    public interface IMatrix
    {
        #region Matrix Methods
        IMatrix Add(IMatrix mRight);
        IMatrix Subtract(IMatrix mRight);
        IMatrix Multiply(IMatrix mRight);
        IMatrix ScalarMultiply(double dScalar);

        IMatrix GaussJordanElimination();

        IMatrix LeastSquares(int iDegree);
        #endregion
    }
}
