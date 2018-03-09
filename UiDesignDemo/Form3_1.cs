using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UiDesignDemo
{
    public partial class Form3_1 : Form
    {
        Login l;

        bool control = false;

        public Form3_1(Login l)
        {
            InitializeComponent();
            this.l = l;
        }

        private void UploadImage()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(openFileDialog1.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                label1.Visible = false;
            }
        }

        private byte[] ConvertImageToBinary(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void AddPatient()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.patients(passport, name, birth, gender, town, phone, mail, adress, photo, reg_date) VALUES(@passport, @name, @birth, @gender, @town, @phone, @mail, @adress, @photo, @reg_date)";
            try
            {
                command.Parameters.AddWithValue("@passport", textBox11.Text);
                command.Parameters.AddWithValue("@name", textBox6.Text);
                command.Parameters.AddWithValue("@birth", dateTimePicker1.Value.Date.ToString());
                command.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
                command.Parameters.AddWithValue("@town", textBox10.Text);
                command.Parameters.AddWithValue("@adress", textBox8.Text);
                command.Parameters.AddWithValue("@phone", textBox2.Text);
                command.Parameters.AddWithValue("@mail", textBox5.Text);
                command.Parameters.AddWithValue("@photo", ConvertImageToBinary(pictureBox2.Image));
                command.Parameters.AddWithValue("@reg_date", dateTimePicker2.Value.Date.ToString());

                command.ExecuteNonQuery();
                MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                control = true;
                Form1 frm = new Form1(l);
                frm.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не всі дані заповнено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPatient();
        }

        private void Form3_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
