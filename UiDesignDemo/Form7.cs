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
    public partial class Form7 : Form
    {
        
        public Login l;

        public Form7(Login l)
        {
            InitializeComponent();
            this.l = l;
        }


        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (l.doc.Position == "Головний лікар")
            {
                Form8 frm = new Form8(this);
                frm.Show();
                this.Hide();
            }
            else MessageBox.Show("У вас немає права додавати лікарів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
          
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            All_doctors frm = new All_doctors(l);
            frm.Show();
            this.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2),
                (ScreenHeight / 2) - (this.Height / 2));
        }
    }
}
