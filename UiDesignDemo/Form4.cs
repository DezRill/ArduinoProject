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
    public partial class Form4 : Form
    {
        public Login l;
        private Patient patient;
        private bool isEditing = false;

        public Form4(Login l, Patient patient)
        {
            InitializeComponent();
            this.l = l;
            textBox1.Text = patient.Name;
            textBox7.Text = patient.Birth.Date.ToShortDateString();
            dateTimePicker2.Value = patient.Birth;
            textBox6.Text = patient.Gender;
            comboBox1.SelectedItem = patient.Gender;
            textBox5.Text = patient.Town;
            textBox10.Clear();
            textBox8.Text = patient.Phone;
            textBox2.Text = patient.Mail;
            textBox9.Text = patient.Adress;
            pictureBox2.Image = patient.Photo;
            textBox11.Text = patient.Reg_Date.Date.ToShortDateString();
            dateTimePicker1.Value = patient.Reg_Date;
            this.patient = patient;
        }

        private void Switcher()
        {
            if (!isEditing)
            {
                textBox1.ReadOnly = false;
                textBox7.Visible = false;
                dateTimePicker2.Visible = true;
                textBox6.Visible = false;
                comboBox1.Visible = true;
                textBox5.ReadOnly = false;
                textBox8.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox9.ReadOnly = false;
                textBox11.Visible = false;
                dateTimePicker1.Visible = true;
                isEditing = true;
            }
            else
            {
                textBox1.ReadOnly = true;
                textBox7.Visible = true;
                textBox7.Text = dateTimePicker2.Value.Date.ToShortDateString();
                dateTimePicker2.Visible = false;
                textBox6.Visible = true;
                textBox6.Text = comboBox1.SelectedItem.ToString();
                comboBox1.Visible = false;
                textBox5.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox11.Visible = true;
                textBox11.Text = dateTimePicker1.Value.Date.ToShortDateString();
                dateTimePicker1.Visible = false;
                isEditing = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4_2 frm = new Form4_2(this);
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Switcher();
        }
    }
}
