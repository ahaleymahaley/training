using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public interface IDrawer
    {
        void DrawBorder(int rows, int columns, int cellsize);
        void DrawCellBorder(int i, int j);
        void DrawCellValue(int i, int j, int value);
        void SetBorder(bool border);
    }
}
