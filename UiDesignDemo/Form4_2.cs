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
    public partial class Form4_2 : Form
    {
        Form4 f;

        public Form4_2(Form4 f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            f.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(this);
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            diagnoses frm = new diagnoses();
            frm.Show();
            this.Hide();
        }
    }
}
