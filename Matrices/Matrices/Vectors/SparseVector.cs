using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class SparseVector : IVector
    {
        private List<int> values;
        private List<int> positions;
        public SparseVector(int i)
        {
            values = new List<int>();
            positions = new List<int>();
        }
        public int Get(int i)
        {
            int pos = positions.IndexOf(i);
            if (pos > -1) return values[pos];            
            else return 0;
        }
        public void Set(int i, int value)
        {
            int pos = positions.IndexOf(i);
            if (value != 0)
            {
                if (pos > -1) values[pos] = value;
                else
                {
                    values.Add(value);
                    positions.Add(i);
                }
            }
            else 
            {
                if (pos != -1)
                {
                    values.RemoveAt(pos);
                    positions.RemoveAt(pos);
                }
            }
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
