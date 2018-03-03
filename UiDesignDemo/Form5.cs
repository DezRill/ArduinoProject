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
        Form8 d;

        public Form5(Form8 d)
        {
            InitializeComponent();
            this.d = d;
        }

        public Form5(All_doctors all_doctors)
        {
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
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
