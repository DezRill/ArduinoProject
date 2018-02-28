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
    public partial class All_doctors : Form
    {
        public Login l;

        public All_doctors(Login l)
        {
            InitializeComponent();
            this.l = l;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5(this);
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }
    }
}
