using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Core {
    internal static class MatrixMethods {
        public static Matrix<double> Approximate(MatrixBW matrix, int width, int height) {
            VerifySizeArguments(width, height);
            throw new NotImplementedException();
        }
        public static Matrix<double> Approximate(MatrixBit matrix, int width, int height) {
            VerifySizeArguments(width, height);
            throw new NotImplementedException();
            //Projdi celou matici
        }
        private static void VerifySizeArguments(int width, int height) {
            if (width < 1 || height < 1)
                throw new ArgumentOutOfRangeException("Width and height must be positive integers");
        }
    }
}
