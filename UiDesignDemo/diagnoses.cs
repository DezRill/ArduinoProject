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
    public partial class diagnoses : Form
    {
        Form4_2 f;

        public diagnoses(Form4_2 f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
            f.Show();
        }
    }
}
