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
    public partial class Form5 : Form
    {
        All_doctors d;

        public Form5(All_doctors d)
        {
            InitializeComponent();
            this.d = d;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
            d.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            d.Show();
        }
    }
}
