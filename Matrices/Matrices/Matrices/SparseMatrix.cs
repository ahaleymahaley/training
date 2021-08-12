using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class SparseMatrix : SomeMatrix
    {

        public SparseMatrix(int i, int j) : base(i,j)
        {
        }

        protected override IVector doCreate(int size)
        {
            return new SparseVector(size);
        }

        protected override bool doDisplay(int i, int j)
        {
            return Get(i, j) != 0;
        }
    }
}
