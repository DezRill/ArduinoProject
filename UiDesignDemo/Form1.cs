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
    public partial class Form1 : Form
    {
        public Login l;

        bool control = false;

        public Form1(Login l)
        {
            InitializeComponent();
            this.l = l;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bunifuTransition2.HideSync(secondUC1);
            bunifuTransition1.ShowSync(firstUC1);
            control = true;
            Form3 frm = new Form3(l);
            frm.Show();
            this.Close();
        }
        private void button19_Click(object sender, EventArgs e)
        {
            bunifuTransition2.HideSync(secondUC1);
            bunifuTransition1.ShowSync(firstUC1);
            control = true;
            All_doctors frm = new All_doctors(l);
            frm.Show();
            this.Close();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bunifuTransition2.HideSync(firstUC1);
            bunifuTransition1.ShowSync(secondUC1);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bunifuTransition2.HideSync(firstUC1);
            bunifuTransition1.ShowSync(secondUC1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            bunifuTransition2.HideSync(secondUC1);
            bunifuTransition1.ShowSync(firstUC1);
            control = true;
            Form6 frm = new Form6(l);
            frm.Show();
            this.Close();
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            bunifuTransition2.HideSync(secondUC1);
            bunifuTransition1.ShowSync(firstUC1);
            control = true;
            Diagram frm = new Diagram(l);
            frm.Show();
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
