using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class NormalVector:IVector
    {
        private List<int> values;
        public NormalVector(int i)
        {
            values = new List<int>();
            for (int j = 0; j < i; j++) 
            {
                values.Add(0);
            }
        }
        public int Get(int i)
        {
            return values[i];
        }
        public void Set(int i, int value)
        {
            values[i] = value;
        }
        public int Size
        {
            get
            {
                return values.Count();
            }
        }
    }
}
