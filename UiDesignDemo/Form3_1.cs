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
    public partial class Form3_1 : Form
    {
        Login l;

        public Form3_1(Login l)
        {
            InitializeComponent();
            this.l = l;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }
    }
}
