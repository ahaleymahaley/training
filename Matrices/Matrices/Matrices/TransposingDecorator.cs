using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class TransposingDecorator : IMatrix, IDrawer
    {
        private IMatrix matrix;
        private IDrawer drawer;

        public TransposingDecorator(IMatrix matrix)
        {
            this.matrix = matrix;
        }

        public IMatrix ClearChanges()
        {
            return matrix;
        }

        public int Columns
        {
            get
            {
                return matrix.Rows;
            }
        }
        public void Draw()
        {
            matrix.SetDrawer(this);
            matrix.Draw();
        }

        public void DrawBorder(int rows, int columns, int cellsize)
        {
            drawer.DrawBorder(Rows, Columns, (new MatrixStatistics(matrix)).Max.ToString().Length);
        }

        public void DrawCellBorder(int i, int j)
        {
            drawer.DrawCellBorder(j, i);
        }

        public void DrawCellValue(int i, int j, int value)
        {
            drawer.DrawCellValue(j, i, value);
        }

        public int Get(int i, int j)
        {
            return matrix.Get(j, i);
        }

        public IDrawer GetDrawer()
        {
            return drawer;
        }

        public int Rows
        {
            get
            {
                return matrix.Columns;
            }
        }

        public void Set(int i, int j, int value)
        {
            matrix.Set(j, i, value);
        }

        public void SetBorder(bool border)
        {
            drawer.SetBorder(border);
        }

        public void SetDrawer(IDrawer drawer)
        {
            this.drawer = drawer;
        }
    }
}
