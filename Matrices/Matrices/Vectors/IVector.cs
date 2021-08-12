using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public interface IVector
    {
        int Get(int i);
        void Set(int i, int value);
        int Size { get; }
    }
}
