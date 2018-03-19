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
        bool control = false;

        public diagnoses(Form4_2 f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            this.Close();
            f.Show();
        }

        private void diagnoses_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
