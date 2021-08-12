using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class ConsoleDrawer : IDrawer
    {
        private bool border=true;
        private int cellsize;
        private List<char[]> frames;
        public void SetBorder(bool border)
        {
            this.border = border;
        }
        public void DrawBorder(int rows, int columns, int cellsize)
        {
            this.cellsize = cellsize;
            //строки для отрисовки
            frames = new List<char[]>();
            char[] frame = new char[2 + (cellsize + 4) * columns];
            if (border) frame[0] = '/';
            else frame[0] = ' ';
            for (int i = 0; i < columns; i++) 
            {
                for (int j = 0; j < (cellsize + 4); j++)
                    frame[1 + i * (cellsize + 4) + j] = ' ';
            }
            if (border) frame[1 + (cellsize + 4) * columns] = '\\';
            else frame[1 + (cellsize + 4) * columns] = ' ';            
            frames.Add(frame);
            for (int k = 0; k < rows; k++) 
            {
                frame = new char[2 + (cellsize + 4) * columns];
                if (border) frame[0] = '|';
                else frame[0] = ' ';
                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < (cellsize + 4); j++)
                        frame[1 + i * (cellsize + 4) + j] = ' ';
                }
                if (border) frame[1 + (cellsize + 4) * columns] = '|';
                else frame[1 + (cellsize + 4) * columns] = ' ';
                frames.Add(frame);
            }
            frame = new char[2 + (cellsize + 4) * columns];
            if (border) frame[0] = '\\';
            else frame[0] = ' ';
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < (cellsize + 4); j++)
                    frame[1 + i * (cellsize + 4) + j] = ' ';
            }
            if (border) frame[1 + (cellsize + 4) * columns] = '/';
            else frame[1 + (cellsize + 4) * columns] = ' ';
            frames.Add(frame);
            for (int i = 0; i < frames.Count; i++) Console.WriteLine(frames[i]);            
        }
        public void DrawCellBorder(int i, int j)
        {
            frames[i + 1][2 + (cellsize + 4) * j] = '(';
            frames[i + 1][3 + cellsize + (cellsize + 4) * j] = ')';
        }
        public void DrawCellValue(int i, int j, int value)
        {
            char[] val = value.ToString().ToCharArray();
            for (int k = 0; k < val.Length; k++)
            {
                frames[i + 1][3 + k + (cellsize + 4) * j] = val[k];
            }
            Print();
        }
        private void Print()
        {
            Console.SetCursorPosition(0, Console.CursorTop - frames.Count);
            for (int i = 0; i < frames.Count; i++)
            {
                Console.WriteLine(frames[i]);
            }
        }
        public void ActionRecord(string cmdName)
        {
            Console.WriteLine(cmdName);
        }
    }
}
