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

using AForge.Video.DirectShow;
using System.Text.RegularExpressions;

using AForge.Video;


using System.Net;
using System.Net.Mail;

namespace UiDesignDemo
{
    public partial class Form4 : Form
    {
        public Login l;
        public Patient patient;
        private bool isEditing = false;

        bool control = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
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
            textBox3.Text = patient.Passport;
            textBox8.Text = patient.Phone;
            textBox2.Text = patient.Mail;
            textBox9.Text = patient.Adress;
            pictureBox2.Image = new Bitmap(patient.Photo);
            textBox11.Text = patient.Reg_Date.Date.ToShortDateString();
            dateTimePicker1.Value = patient.Reg_Date;

            this.patient = patient;
        }

        private void UploadImage()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(openFileDialog1.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
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

        private void UpdatePatient()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "UPDATE dbo.patients SET passport=@passport, name=@name, birth=@birth, gender=@gender, town=@town, phone=@phone, mail=@mail, adress=@adress, photo=@photo, reg_date=@reg_date WHERE id=@id";
            command.Parameters.AddWithValue("@id", patient.Id);
            command.Parameters.AddWithValue("@passport", patient.Passport);
            command.Parameters.AddWithValue("@name", patient.Name);
            command.Parameters.AddWithValue("@birth", patient.Birth.Date.ToString());
            command.Parameters.AddWithValue("@gender", patient.Gender);
            command.Parameters.AddWithValue("@town", patient.Town);
            command.Parameters.AddWithValue("@phone", patient.Phone);
            command.Parameters.AddWithValue("@mail", patient.Mail);
            command.Parameters.AddWithValue("@adress", patient.Adress);
            command.Parameters.AddWithValue("@photo", ConvertImageToBinary(patient.Photo));
            command.Parameters.AddWithValue("@reg_date", patient.Reg_Date.Date.ToString());
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Успішно збережено", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Усі поля повинні бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                textBox3.ReadOnly = false;
                textBox8.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox9.ReadOnly = false;
                textBox11.Visible = false;
                dateTimePicker1.Visible = true;
                button6.Visible = true;
                panel4.Visible = true;
                if (videoDevices.Count>0)
                {
                    
                    comboBox2.Visible = true;
                    button8.Visible = true;
                    button9.Visible = true;
                }
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
                textBox3.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox11.Visible = true;
                textBox11.Text = dateTimePicker1.Value.Date.ToShortDateString();
                dateTimePicker1.Visible = false;
                button6.Visible = false;
                panel4.Visible = false;
                comboBox2.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                isEditing = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            control = true;
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
            Form7 frm = new Form7(this);
            frm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Switcher();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox8.Text != "" && textBox9.Text != "")
            {
                patient.Passport = textBox3.Text;
                patient.Name = textBox1.Text;
                patient.Birth = dateTimePicker2.Value.Date;
                patient.Gender = comboBox1.SelectedItem.ToString();
                patient.Town = textBox5.Text;
                patient.Phone = textBox8.Text;
                patient.Mail = textBox2.Text;
                patient.Adress = textBox9.Text;
                patient.Photo = pictureBox2.Image;
                patient.Reg_Date = dateTimePicker1.Value.Date;
                UpdatePatient();
            }
            else MessageBox.Show("Всі поля повинні бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            if (videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                foreach (FilterInfo device in videoDevices)
                {
                    comboBox2.Items.Add(device.Name);
                }

                comboBox2.SelectedIndex = 0;
            }
            else
            {
                comboBox2.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
            }
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            if (isEditing) UploadImage();
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isEditing) UploadImage();
        }

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            if (isEditing) UploadImage();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            videoSource = new VideoCaptureDevice(videoDevices[comboBox2.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
        }
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox2.Image = bitmap;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            videoSource.Stop();
            videoSource.SignalToStop();
            this.pictureBox2.Image.Save("123");
        }
    }
    }

