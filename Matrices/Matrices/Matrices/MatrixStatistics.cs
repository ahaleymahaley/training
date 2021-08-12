using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class MatrixStatistics
    {
        public MatrixStatistics(IMatrix matrix)
        {
            Sum = 0;
            Max = 0;
            NotZeros = 0;
            int rows = matrix.Rows;
            int columns = matrix.Columns;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    int value = matrix.Get(i, j);
                    if (value != 0) NotZeros++;
                    if (value > Max) Max = value;
                    Sum += value;
                }
            Average = Sum / NotZeros;
        }
        public int Sum { get; private set; }
        public double Average { get; private set; }
        public int Max { get; private set; }
        public int NotZeros { get; private set; }
    }
}
