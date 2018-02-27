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
    public partial class Diagram : Form
    {
        Login l;

        public Diagram(Login l)
        {
            InitializeComponent();
            this.l = l;
        }

        private void Diagram_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
        }
    }
}
