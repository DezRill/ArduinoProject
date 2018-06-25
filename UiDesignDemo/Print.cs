using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UiDesignDemo
{
    public partial class Print : Form
    {
        Bitmap bmp;

        public Print(string doctor, string inspection, string diagnosis, string recommendations)
        {
            InitializeComponent();
            FillForm(doctor, inspection, diagnosis, recommendations);
            timer1.Interval = 1500;
            timer1.Enabled = true;
        }

        void FillForm(string doctor, string inspection, string diagnosis, string recommendations)
        {
            textBox1.Text = DateTime.Now.Date.ToShortDateString();
            textBox2.Text = doctor;
            textBox3.Text = diagnosis;
            textBox4.Text = inspection;
            textBox5.Text = recommendations;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 110, 130);
        }

        void PrintForm()
        {
            Graphics g = this.CreateGraphics();
            Size size = this.Size;
            bmp = new Bitmap(size.Width, size.Height, g);
            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, size);
            printDialog1.Document = printDocument1;
            var dr = printDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                printDocument1.Print();
            }
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            PrintForm();
        }
    }
}
