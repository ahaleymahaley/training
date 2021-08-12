using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matrices
{
    public partial class MatrixForm : Form
    {
        private IMatrix matrix;
        private ConsoleDrawer console;
        private FormDrawer form;
        public MatrixForm()
        {
            InitializeComponent();
            console = new ConsoleDrawer();
            form = new FormDrawer(pictureBox1);
            new Start(this).Execute();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IMatrix temp = new NormalMatrix(5, 5);
            MatrixInitiator.FillTheMatrix(temp, 10, 50);
            new SetNDrawMatrix(this, temp).Execute();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            IMatrix temp = new SparseMatrix(5, 5);
            MatrixInitiator.FillTheMatrix(temp, 10, 50);
            new SetNDrawMatrix(this, temp).Execute();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (matrix != null)
            {
                TransposingDecorator temp = new TransposingDecorator(matrix);                
                new SetNDrawMatrix(this, temp).Execute();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (matrix != null)
            {
                IMatrix temp = matrix.ClearChanges();
                if (matrix != temp) new SetNDrawMatrix(this, temp).Execute();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            CM.GetInstance().Undo();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            CM.GetInstance().Redo();
        }
        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (matrix != null) new SetNDrawBorder(this, checkBox1.Checked).Execute();
            else new SetBorder(this, checkBox1.Checked).Execute();
        }
        public void SetMatrix(IMatrix matrix)
        {
            this.matrix = matrix;
        }
        public void Clear()
        {
            matrix = null;
            pictureBox1.Image = null;
            Console.Clear();
        }
        public void SetBorder(bool isChecked)
        {
            console.SetBorder(isChecked);
            form.SetBorder(isChecked);
            checkBox1.Checked = isChecked;
        }
        public void DrawMatrix()
        {
            matrix.SetDrawer(console);
            matrix.Draw();
            matrix.SetDrawer(form);
            matrix.Draw();
        }        
        public void ActionRecord(string cmdName)
        {
            console.ActionRecord(cmdName);
        }
    }
}
