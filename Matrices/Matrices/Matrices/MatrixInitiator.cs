using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class MatrixInitiator
    {
        static public void FillTheMatrix(IMatrix matrix, int notzeros, int max)
        {
            var rand = new Random(DateTime.Now.GetHashCode()+matrix.GetHashCode());
            int rows = matrix.Rows;
            int colunms = matrix.Columns;
            while (notzeros > 0)  
            {
                int i = rand.Next(rows);
                int j = rand.Next(colunms);
                if (matrix.Get(i, j) == 0)
                {
                    int value = rand.Next(max) + 1;
                    matrix.Set(i, j, value);
                    notzeros--;
                }
            }
        }
    }
}
