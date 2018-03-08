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

        public Form1(Login l)
        {
            InitializeComponent();
            this.l = l;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bunifuTransition2.HideSync(secondUC1);
            bunifuTransition1.ShowSync(firstUC1);
            Form3 frm = new Form3(l);
            frm.Show();
            this.Close();
        }
        private void button19_Click(object sender, EventArgs e)
        {
            bunifuTransition2.HideSync(secondUC1);
            bunifuTransition1.ShowSync(firstUC1);
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

            Form6 frm = new Form6(l);
            frm.Show();
            this.Close();
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            bunifuTransition2.HideSync(secondUC1);
            bunifuTransition1.ShowSync(firstUC1);
            //Diagram frm = new Diagram(l);
            Form10 frm = new Form10(l);
            frm.Show();
            this.Close();
        }

        private void button16_Click_1(object sender, EventArgs e)
        {

        }
    }
}
