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
        Login l;
        bool control = false;

        public All_doctors(Login l)
        {
            
            InitializeComponent();
            this.l = l;
            textBox1.Text = l.doc.Name;
            dateTimePicker1.Value = l.doc.Birth;
            textBox3.Text = l.doc.Birth.Date.ToShortDateString();
            textBox6.Text = l.doc.Gender;
            textBox5.Text = l.doc.Town;
            textBox8.Text = l.doc.Phone;
            textBox2.Text = l.doc.Mail;
            textBox9.Text = l.doc.Adress;
            textBox10.Clear();
            pictureBox2.Image = l.doc.Photo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (l.doc.Position == "Головний лікар")
            {
                Form5 frm = new Form5(l);
                frm.Show();
                this.Hide();
            }
            else MessageBox.Show("У вас немає права додавати лікарів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                control = true;
                Form8 frm = new Form8(l);
                frm.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не вдалось під'єднатись до бази даних. Будь ласка, зверніться до системного адміністратора", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                l.Show();
            }
        }

        private void All_doctors_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason==CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
