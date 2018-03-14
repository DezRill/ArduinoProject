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
using AForge.Video;
using AForge.Video.DirectShow;
using System.Text.RegularExpressions;

using System.Net;
using System.Net.Mail;

namespace UiDesignDemo
{
    public partial class Form5 : Form
    {
        Login l;
        bool editing = false;
        int id;

        bool control = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        public Form5(Login l)
        {
            InitializeComponent();
            this.l = l;
            this.Text = "Створення нового лікаря";
        }

        public Form5(Login l, int id)
        {
            InitializeComponent();
            this.l = l;
            editing = true;
            label1.Visible = false;
            this.id = id;
            GetDoctor();
            this.Text = "Редагування лікаря";
        }

        private bool CheckLogAndPass()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT id FROM dbo.doctors WHERE login=@login";
            command.Parameters.AddWithValue("@login", textBox16.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count > 0) return false;
            else return true;
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

            dateTimePicker1.Value = Convert.ToDateTime(table.Rows[0]["invite_date"].ToString());
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

                if (CheckLogAndPass())
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    control = true;
                    Form8 frm = new Form8(l);
                    frm.Show();
                    this.Close();
                }
                else MessageBox.Show("Такий лікар вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (CheckLogAndPass())
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    control = true;
                    Form8 frm = new Form8(l);
                    frm.Show();
                    this.Close();
                }
                else MessageBox.Show("Такий лікар вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch
            {
                MessageBox.Show("Не всі дані заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form8 frm = new Form8(l);
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (editing) UpdateDoctor();
            else AddDoctor();
            MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // kuzichkaa @gmail.com"
            //ankuzmynykh @gmail.com
            //olusjkalike@gmail.com
            MailAddress fromMailAddress = new MailAddress("hospitalmaindoctor@gmail.com");
            //   MailAddress toAddress = new MailAddress(f.patient.Mail);
            MailAddress toAddress = new MailAddress(textBox5.Text);

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toAddress))
            using (SmtpClient smtpClient = new SmtpClient())
            {

                mailMessage.Subject = "Приватна клініка Hospital";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<h>Ваші дані:</h>" + "<br></br>" + "<h>" + "login:" + textBox16.Text + "</h>" + "<br></br>" + "<h>" + "password:" + textBox17.Text + "</h>";
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, "doctorhospital");
                smtpClient.Send(mailMessage);
            }
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Зберегти як ...";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                savedialog.FileName = textBox9.Text;
                //список форматов файла, отображаемый в поле "Тип файла"
                savedialog.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                //отображается ли кнопка "Справка" в диалоговом окне
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        pictureBox2.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Неможливо зберегти зображення", "Помилка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            videoSource.Stop();
            videoSource.SignalToStop();
            this.pictureBox2.Image.Save("123");
        }

        private void Form5_Load(object sender, EventArgs e)
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
                button2.Visible = false;
                button3.Visible = false;
            }
        }
    }
}

