using Scoi_4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCOI_1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public Bitmap img1 = null;
        public Bitmap img2 = null;
         


       

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "Картинки (png, jpg, bmp, gif) |*.png;*.jpg;*.bmp;*.gif|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (img1 != null)
                {
                    pictureBox1.Image = null;
                    img1.Dispose();
                    img2.Dispose();
                }

                img1 = new Bitmap(openFileDialog.FileName);
                img2 = new Bitmap(img1);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = img1; // помещаем картинку в окошко где она должна валятся
               
                
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.Image = img2; // помещаем картинку в окошко где она должна валятся
            }
            openFileDialog.Dispose();
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            button2.Visible = true;
            
            
        }


        private void button2_Click(object sender, EventArgs e)
        {                                                                                //кнопка обработать

            int w=int.Parse(textBox1.Text);
            int h = int.Parse(textBox2.Text);
            int o = int.Parse(textBox3.Text);

            double[,] matrix = GetMatrix(o, w, h);


            lab4 objj = new lab4((Bitmap)img1.Clone(),w,h,matrix);
            pictureBox2.Image=(Bitmap)objj.ApplyFilter().Clone();
            objj.Dispose();
            pictureBox2.Refresh();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        double[,] GetMatrix(int o, int w, int h)                    //делает матрицу
        {
            double[,] mat = new double[h, w];
            int r1 = (w - 1) / 2;
            int r2 = (h - 1) / 2;
            double s = 0;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    double g = 1.0 / (2.0 * Math.PI * o * o) * Math.Exp(-1.0 * ((i - r2) * (i - r2) + (j - r1) * (j - r1)) / (2.0 * o * o));
                    s += g;
                    mat[i, j] = g;
                    Console.Write(g + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Sum: " + s);
            return mat;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {                                                                                    //собственная матрица
            int w = int.Parse(textBox1.Text);
            int h = int.Parse(textBox2.Text);

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            for (int j = 0; j < w; j++)
            {
                dataGridView1.Columns.Add("name" + j, j.ToString());
            }
            for (int i = 0; i < h; i++)
                dataGridView1.Rows.Add();
        }

        private void button4_Click(object sender, EventArgs e)
        {                                                                           //обработка 2
            int w = int.Parse(textBox1.Text);
            int h = int.Parse(textBox2.Text);
            double[,] matrix = new double[h, w];

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    matrix[i, j] = Convert.ToDouble(dataGridView1[j, i].Value);
                }
            lab4 objj = new lab4((Bitmap)img1.Clone(), w, h, matrix);
            pictureBox2.Image = (Bitmap)objj.ApplyFilter().Clone();
            objj.Dispose();
            pictureBox2.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {//сбросить
            pictureBox2.Image.Dispose();
            pictureBox2.Image = (Bitmap)pictureBox1.Image.Clone();
        }
    }
}
