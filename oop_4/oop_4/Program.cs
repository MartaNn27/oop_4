
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace opp10
{
    public partial class Form1 : Form
    {
        Matrix matrix;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int cols = int.Parse(textBox1.Text);
            int rows = int.Parse(textBox2.Text);
            if (cols == rows)
            {
                matrix = new Matrix(cols, rows);
            }
            else
            {
                rows = cols;
                MessageBox.Show("Введена матриця не квадратна!! Тому тепер твоя матриця - " + rows.ToString() + " x " + rows.ToString());


                matrix = new Matrix(cols, rows);

            }

            int min = int.Parse(textBox3.Text);
            int max = int.Parse(textBox4.Text);

            matrix.FillElements(min, max);
            Print(matrix);


            int columnIndex = matrix.SumCol();
            label7.Visible = true;

            label7.Text = columnIndex.ToString();

            int z = matrix.SumD();
            label5.Visible = true;

            label5.Text = z.ToString();


        }
        private void Print(Matrix matrix)
        {
            DataTable dt = new DataTable();
            int columns = matrix.ColCount;
            int rows = matrix.RowCount;
            for (int i = 0; i < columns; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(float));
            }
            for (int row = 0; row < rows; row++)
            {
                DataRow dr = dt.NewRow();
                for (int col = 0; col < columns; col++)
                {
                    dr[col] = matrix[row, col];
                }
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt.DefaultView;

        }
    }
    class Matrix
    {
        int[,] matrixA;
        public int RowCount { get; set; }
        public int ColCount { get; set; }

        public Matrix(int rows, int cols)
        {
            matrixA = new int[rows, cols];
            RowCount = matrixA.GetLength(0);
            ColCount = matrixA.GetLength(1);
        }
        public int this[int i, int j]
        {
            get
            {
                if (i < RowCount && i >= 0 && j < ColCount && j >= 0)
                    return matrixA[i, j];
                else throw new IndexOutOfRangeException("Індекс виходить за межі масиву!");
            }
        }
        public void FillElements(int min, int max)
        {
            Random rand = new Random();
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {


                    matrixA[i, j] = rand.Next(min, max + 1);



                }
            }
        }

        public int SumCol()
        {




            int sum = 0;


            for (int i = 0; i < ColCount; i++)
            {
                int sumCol = 0;

                int positiveElements = 1;
                for (int j = 0; j < RowCount; j++)
                {

                    sumCol += matrixA[j, i];

                    if (matrixA[j, i] < 0)
                        positiveElements = 0;

                }
                if (positiveElements == 1)
                {

                    sum += sumCol;
                    MessageBox.Show(" Сума стовпця  " + i + "  =  " + sumCol.ToString());

                }






            }

            return sum;
        }
        public int SumD()
        {
            int max1 = matrixA[0, 0];
            int sum = 0;
            for (int i = 0; i < ColCount; i++)
            {
                for (int j = 0; j < RowCount; j++)
                {
                    if (i != j && j > i)
                    {
                        sum += matrixA[i, j];
                        max1 = sum;
                        break;

                    }
                }
            }
            int max2 = matrixA[0, 0];
            int sum2 = 0;
            for (int i = 0; i < ColCount; i++)
            {
                for (int j = 0; j < RowCount; j++)
                {
                    if (j > i)
                    {
                        sum2 += matrixA[j, i];
                        max2 = sum2;
                        break;

                    }
                }
            }

            if (max1 >= max2) return max1;

            return max2;
        }


    }

}
