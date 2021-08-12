using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class NormalMatrix : SomeMatrix
    {
        public NormalMatrix(int i, int j) : base(i, j)
        {
        }

        protected override IVector doCreate(int size)
        {
            return new NormalVector(size);
        }
    }
}
