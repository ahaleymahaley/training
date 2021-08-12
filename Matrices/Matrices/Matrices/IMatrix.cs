using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public interface IMatrix
    {
        int Get(int i, int j);
        void Set(int i, int j, int value);
        int Rows { get; }
        int Columns { get; }
        void SetDrawer(IDrawer drawer);
        IDrawer GetDrawer();
        void Draw();
        IMatrix ClearChanges();
    }
}
