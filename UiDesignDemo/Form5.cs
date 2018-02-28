using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace UiDesignDemo
{
    public partial class Form5 : Form
    {
        All_doctors d;

        public Form5(All_doctors d)
        {
            InitializeComponent();
            this.d = d;
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

        private void AddDoctor()
        {
            SqlCommand command = d.l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.doctors(name, birth, gender, town, phone, mail, adress, photo, passport, diploma_num, specialty, position, invite_date, short_char, login, password) VALUES (@name, @birth, @gender, @town, @phone, @mail, @adress, @photo, @passport, @diploma_num, @specialty, @position, @invite_date, @short_char, @login, @password)";
            command.Parameters.AddWithValue("@name", textBox6.Text);
            command.Parameters.AddWithValue("@birth", dateTimePicker2.Value.Date.ToString());
            command.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@town", textBox10.Text);
            command.Parameters.AddWithValue("@phone", textBox2.Text);
            command.Parameters.AddWithValue("@mail", textBox5.Text);
            command.Parameters.AddWithValue("@adress", textBox8.Text);
            command.Parameters.AddWithValue("@photo", ConvertImageToBinary(pictureBox2.Image));
            command.Parameters.AddWithValue("@passport", textBox9.Text);
            command.Parameters.AddWithValue("@diploma_num", textBox11.Text);
            command.Parameters.AddWithValue("@specialty", textBox13.Text);
            command.Parameters.AddWithValue("@position", textBox15.Text);
            command.Parameters.AddWithValue("@invite_date", dateTimePicker1.Value.Date.ToString());
            command.Parameters.AddWithValue("@short_char", textBox1.Text);
            command.Parameters.AddWithValue("@login", textBox16.Text);
            command.Parameters.AddWithValue("@password", textBox17.Text);
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Не всі дані заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
            d.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddDoctor();
            this.Close();
            d.Show();
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
            label1.Visible = false;
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
            label1.Visible = false;
        }
    }
}
