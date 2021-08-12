using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matrices
{
    class FormDrawer : IDrawer
    {
        private bool border=true;
        private PictureBox pictureBox;
        private Graphics g;
        private Font myFont;
        private int cellsize;
        private int rowheight = 14;
        public void SetBorder(bool border)
        {
            this.border = border;
        }
        public FormDrawer(PictureBox pictureBox)
        {            
            this.pictureBox = pictureBox;
        }
        public void DrawBorder(int rows, int columns, int cellsize)
        {
            Bitmap DrawArea = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            pictureBox.Image = DrawArea;
            g = Graphics.FromImage(DrawArea);            
            Pen myPen = new Pen(Brushes.Black, 3);
            myFont = new Font("Arial", rowheight);
            this.cellsize = cellsize;
            if (border)
            {
                g.DrawLine(myPen, 0, rowheight, rowheight, 0);
                g.DrawLine(myPen, 0, rowheight, 0, rowheight * rows);
                g.DrawLine(myPen, 0, rowheight * rows, rowheight, rowheight * (rows + 1));
                g.DrawLine(myPen, rowheight * (columns * (cellsize + 2) + 1), 0, rowheight * (columns * (cellsize + 2) + 2), rowheight);
                g.DrawLine(myPen, rowheight * (columns * (cellsize + 2) + 2), rowheight, rowheight * (columns * (cellsize + 2) + 2), rowheight * rows);
                g.DrawLine(myPen, rowheight * (columns * (cellsize + 2) + 2), rowheight * rows, rowheight * (columns * (cellsize + 2) + 1), rowheight * (rows + 1));
            }
        }

        public void DrawCellBorder(int i, int j)
        {
            g.DrawString("(", myFont, Brushes.Black, new Point(rowheight * (1 + j * (cellsize + 2)), i * rowheight));
            g.DrawString(")", myFont, Brushes.Black, new Point(rowheight * ((j + 1)* (cellsize + 2)), i * rowheight));
        }

        public void DrawCellValue(int i, int j, int value)
        {
            g.DrawString(value.ToString(), myFont, Brushes.Black, new Point(rowheight * (2 + j * (cellsize + 2)), i * rowheight));
        }
    }
}
