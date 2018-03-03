﻿using System;
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
    public partial class Form8 : Form
    {
        Form8 d;
        public Login l;
        private Form7 form7;

        //private bool isEditing = false;

        public Form8(Login l)
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

        public Form8(Form7 form7)
        {
            this.form7 = form7;
        }

        //private void Switcher()
        //{
        //    if (l.doc.Position == "Головний лікар")
        //    {
        //        if (!isEditing)
        //        {
        //            textBox1.ReadOnly = false;
        //            textBox3.Visible = false;
        //            dateTimePicker1.Visible = true;
        //            textBox6.ReadOnly = false;
        //            textBox2.ReadOnly = false;
        //            textBox5.ReadOnly = false;
        //            textBox8.ReadOnly = false;
        //            textBox2.ReadOnly = false;
        //            textBox9.ReadOnly = false;
        //            isEditing = true;
        //        }
        //        else
        //        {
        //            textBox1.ReadOnly = true;
        //            textBox3.Visible = true;
        //            textBox3.Text = dateTimePicker1.Value.Date.ToShortDateString();
        //            dateTimePicker1.Visible = false;
        //            textBox6.ReadOnly = true;
        //            textBox2.ReadOnly = true;
        //            textBox5.ReadOnly = true;
        //            textBox8.ReadOnly = true;
        //            textBox2.ReadOnly = true;
        //            textBox9.ReadOnly = true;
        //            isEditing = false;
        //        }
        //    }
        //    else MessageBox.Show("У вас немає доступу до редагування", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            if (l.doc.Position == "Головний лікар")
            {
                Form5 frm = new Form5(this);
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
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2),
                (ScreenHeight / 2) - (this.Height / 2));
        }
    }
}