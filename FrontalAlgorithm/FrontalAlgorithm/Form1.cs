using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanningTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void выбратьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = "Выбран файл " + openFileDialog1.FileName;
            }
        }

        private void FillTheTable1(Output output)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = output.x.Length + 1;
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[0].Name = "j";
            for (int i = 1; i < output.x.Length + 1; i++)
            {
                dataGridView1.Columns[i].Width = 30;
                dataGridView1.Columns[i].Name = i.ToString();
            }
            string[] str = new string[output.x.Length + 1];
            str[0] = "x(j)";
            for (int i = 1; i < output.x.Length + 1; i++)
            {
                if (output.x[i - 1] != -1) str[i] = output.x[i - 1].ToString();
                else str[i] = "-";
            }
            dataGridView1.Rows.Add(str);
            str[0] = "y(j)";
            for (int i = 1; i < output.x.Length + 1; i++)
            {
                if (output.x[i - 1] != -1) str[i] = output.y[i - 1].ToString();
                else str[i] = "-";
            }
            dataGridView1.Rows.Add(str);
            str[0] = "z(j)";
            for (int i = 1; i < output.x.Length + 1; i++)
            {
                if (output.x[i - 1] != -1) str[i] = output.z[i - 1].ToString();
                else str[i] = "-";
            }
            dataGridView1.Rows.Add(str);
        }

        private void FillTheTable2(Input input, Output output)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.ColumnCount = input.GetOrders().Count + 1;
            dataGridView2.Columns[0].Width = 100;
            dataGridView2.Columns[0].Name = "i";
            for (int i = 1; i < input.GetOrders().Count + 1; i++)
            {
                dataGridView2.Columns[i].Width = 100;
                dataGridView2.Columns[i].Name = i.ToString();
            }
            string[] str = new string[input.GetOrders().Count + 1];
            str[0] = "d(i)";
            string[] str1 = new string[input.GetOrders().Count + 1];
            str1[0] = "t(end)";
            string[] str2 = new string[input.GetOrders().Count + 1];
            str2[0] = "штраф";
            int fine = 0;
            for (int i = 1; i < input.GetOrders().Count + 1; i++)
            {
                int tend = -1;
                for (int j = 0; j < input.GetOrders()[i - 1].GetPsi().Count; j++)
                {
                    if (output.y[input.GetOrders()[i - 1].GetPsi()[j]] == -1)
                    {
                        tend = -1;
                        break;
                    }
                    else if (output.y[input.GetOrders()[i - 1].GetPsi()[j]] > tend) tend = output.y[input.GetOrders()[i - 1].GetPsi()[j]];
                }
                str[i] = input.GetOrders()[i - 1].GetD().ToString();
                if (tend != -1) str1[i] = tend.ToString();
                else str1[i] = "не выполнен";
                if (tend != -1)
                {
                    fine += Math.Max(0, tend - input.GetOrders()[i - 1].GetD());
                    str2[i] = Math.Max(0, tend - input.GetOrders()[i - 1].GetD()).ToString();
                }
                else str2[i] = "не выполнен";
            }
            dataGridView2.Rows.Add(str);
            dataGridView2.Rows.Add(str1);
            dataGridView2.Rows.Add(str2);
            label4.Text = "Общий штраф: " + fine.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Выбран файл")
            {                
                Parser parser = new Parser(openFileDialog1.FileName);
                Output output=new Output(1);
                Algorithm fa;
                if (radioButton1.Checked) { fa = new Frontal(parser.input, 1); output = fa.Solution(); }
                if (radioButton2.Checked) { fa = new Frontal(parser.input, 2); output = fa.Solution(); }
                if (radioButton3.Checked) { fa = new Iterations(parser.input); output = fa.Solution(); }
                if (radioButton4.Checked) { fa = new Heuristics(parser.input); output = fa.Solution(); }
                FillTheTable1(output);
                FillTheTable2(parser.input, output);
                Gant(parser.input.GetT0(), output, parser.input.GetP());
            }
            else MessageBox.Show("Выберите файл");
        }

        void Gant(int t0, Output output, int p)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics gr = Graphics.FromImage(bmp);
            int tend = int.MinValue;
            for (int i = 0; i < output.y.Length; i++)
            {
                if (tend < output.y[i]) tend = output.y[i];
                if (output.y[i] == -1)
                {
                    tend = int.MaxValue;
                    break;
                }
            }
            float e;
            if (tend < t0) e = pictureBox1.Width / tend;
            else e = pictureBox1.Width / t0;
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = DashStyle.Dash;    
            for (int i = 0; i < output.y.Length; i++)
            {
                gr.DrawLine(pen1, e * output.x[i], 0, e * output.x[i], pictureBox1.Height);
                gr.DrawLine(pen1, e * output.y[i], 0, e * output.y[i], pictureBox1.Height);
            }
            for (int i = 0; i < output.y.Length; i++)
            {
                gr.DrawLine(new Pen(Color.LightBlue, 20), e * output.x[i], pictureBox1.Height / (p + 1) * output.z[i], e * output.y[i], pictureBox1.Height / (p + 1) * output.z[i]);                
            }
            pen1.DashStyle = DashStyle.Solid;
            for (int i = 0; i < output.y.Length; i++)
            {
                gr.DrawLine(pen1, e * output.x[i], pictureBox1.Height / (p + 1) * output.z[i] - 10, e * output.x[i], pictureBox1.Height / (p + 1) * output.z[i] + 9);
                gr.DrawLine(pen1, e * output.y[i], pictureBox1.Height / (p + 1) * output.z[i] - 10, e * output.y[i], pictureBox1.Height / (p + 1) * output.z[i] + 9);
                gr.DrawString((i + 1).ToString(), new Font("Arial", 10), Brushes.Black, e * (output.x[i] + (output.y[i] - output.x[i]) /2) - 5, pictureBox1.Height / (p + 1) * output.z[i] - 9);
            }
            if (tend > t0) gr.DrawLine(new Pen(Color.Red, 3), e * t0, 0, e * t0, pictureBox1.Height);            
            pictureBox1.Image = bmp;
        }
    }
}
