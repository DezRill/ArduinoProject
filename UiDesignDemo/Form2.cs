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
    public partial class Form2 : Form
    {
        Form f;

        public Form2(Form3 f)
        {
            InitializeComponent();
            this.f = f;
        }

        public Form2(Form4_2 f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.Show();
        }
    }
}
