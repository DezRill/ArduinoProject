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
        Login l;
        bool editing = false;
        int id;

        public Form5(Login l)
        {
            InitializeComponent();
            this.l = l;
        }

        public Form5(Login l, int id)
        {
            InitializeComponent();
            this.l = l;
            editing = true;
            label1.Visible = false;
            this.id = id;
            GetDoctor();
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

        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        private void GetDoctor()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.doctors WHERE id=@id";
            command.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dateTimePicker1.Value=Convert.ToDateTime(table.Rows[0]["invite_date"].ToString());
            textBox6.Text = table.Rows[0]["name"].ToString();
            dateTimePicker2.Value = Convert.ToDateTime(table.Rows[0]["birth"].ToString());
            comboBox1.SelectedItem = table.Rows[0]["gender"].ToString();
            textBox10.Text = table.Rows[0]["town"].ToString();
            textBox8.Text = table.Rows[0]["adress"].ToString();
            textBox2.Text = table.Rows[0]["phone"].ToString();
            textBox5.Text = table.Rows[0]["mail"].ToString();
            textBox9.Text = table.Rows[0]["passport"].ToString();
            textBox11.Text = table.Rows[0]["diploma_num"].ToString();
            textBox13.Text = table.Rows[0]["specialty"].ToString();
            textBox15.Text = table.Rows[0]["position"].ToString();
            textBox1.Text = table.Rows[0]["short_char"].ToString();
            textBox16.Text = table.Rows[0]["login"].ToString();
            textBox17.Text = table.Rows[0]["password"].ToString();
            pictureBox2.Image = new Bitmap(ConvertBinaryToImage((byte[])table.Rows[0]["photo"]));
        }

        private void AddDoctor()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.doctors(name, birth, gender, town, phone, mail, adress, photo, passport, diploma_num, specialty, position, invite_date, short_char, login, password) VALUES (@name, @birth, @gender, @town, @phone, @mail, @adress, @photo, @passport, @diploma_num, @specialty, @position, @invite_date, @short_char, @login, @password)";
            try
            {
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

                command.ExecuteNonQuery();
                MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form8 frm = new Form8(l);
                frm.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не всі дані заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDoctor()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "UPDATE dbo.doctors SET name=@name, birth=@birth, gender=@gender, town=@town, phone=@phone, mail=@mail, adress=@adress, photo=@photo, passport=@passport, diploma_num=@diploma_num, specialty=@specialty, position=@position, invite_date=@invite_date, short_char=@short_char, login=@login, password=@password WHERE id=@id";
            try
            {
                command.Parameters.AddWithValue("@id", id);
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

                command.ExecuteNonQuery();
                MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form8 frm = new Form8(l);
                frm.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не всі дані заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form8 frm = new Form8(l);
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (editing) UpdateDoctor();
            else AddDoctor();
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }
    }
}
