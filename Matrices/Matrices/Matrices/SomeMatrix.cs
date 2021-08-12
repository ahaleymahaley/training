using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    abstract class SomeMatrix : IMatrix
    {
        private IDrawer drawer;
        private IVector values;
        private int rows;
        private int columns;        
        public SomeMatrix(int i, int j)
        {
            values = doCreate(i * j);
            rows = i;
            columns = j;
        }
        protected abstract IVector doCreate(int size);        
        public int Get(int i, int j)
        {
            return values.Get(i * columns + j);
        }
        public void Set(int i, int j, int value)
        {
            values.Set(i * columns + j, value);
        }
        public int Rows
        {
            get
            {
                return rows;
            }
        }
        public int Columns
        {
            get
            {
                return columns;
            }
        }
        public void SetDrawer(IDrawer drawer)
        {
            this.drawer = drawer;
        }
        public IDrawer GetDrawer()
        {
            return drawer;
        }
        public void Draw()
        {
            drawer.DrawBorder(rows, columns, (new MatrixStatistics(this)).Max.ToString().Length);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    if (doDisplay(i, j))
                    {
                        drawer.DrawCellBorder(i, j);
                        drawer.DrawCellValue(i, j, values.Get(i * columns + j));
                    }
                }
        }
        protected virtual bool doDisplay(int i, int j)
        {
            return true;
        }

        public IMatrix ClearChanges()
        {
            return this;
        }

    }
}
