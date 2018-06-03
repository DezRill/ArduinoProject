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
using AForge.Video;
using AForge.Video.DirectShow;
using System.Text.RegularExpressions;

using System.Net;
using System.Net.Mail;

namespace UiDesignDemo
{
    public partial class Form3_1 : Form
    {
        Login l;

        bool control = false;

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

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

        private bool CheckPassport()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT id FROM dbo.patients WHERE passport=@passport";
            if (checkBox1.Checked) command.Parameters.AddWithValue("@passport", maskedTextBox1.Text);
            else if (checkBox2.Checked) command.Parameters.AddWithValue("@passport", maskedTextBox4.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count > 0) return false;
            else return true;
        }

        public static byte[] ConvertImageToBinary(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void AddPatient()
        {
            try
            {
                SqlCommand command = l.connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.patients(passport, name, birth, gender, town, phone, mail, adress, photo, reg_date) VALUES(@passport, @name, @birth, @gender, @town, @phone, @mail, @adress, @photo, @reg_date)";
                if (checkBox1.Checked) command.Parameters.AddWithValue("@passport", maskedTextBox1.Text);
                else if (checkBox2.Checked) command.Parameters.AddWithValue("@passport", maskedTextBox4.Text);
                command.Parameters.AddWithValue("@name", textBox6.Text);
                command.Parameters.AddWithValue("@birth", dateTimePicker1.Value.Date.ToString());
                command.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
                command.Parameters.AddWithValue("@town", textBox10.Text);
                command.Parameters.AddWithValue("@adress", textBox8.Text);
                command.Parameters.AddWithValue("@phone", maskedTextBox2.Text);
                command.Parameters.AddWithValue("@mail", textBox5.Text);
                command.Parameters.AddWithValue("@photo", ConvertImageToBinary(pictureBox2.Image));
                command.Parameters.AddWithValue("@reg_date", dateTimePicker2.Value.Date.ToString());

                if (CheckPassport())
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MailAddress fromMailAddress = new MailAddress("hospitalmaindoctor@gmail.com");
                    MailAddress toAddress = new MailAddress(textBox5.Text);

                    using (MailMessage mailMessage = new MailMessage(fromMailAddress, toAddress))
                    using (SmtpClient smtpClient = new SmtpClient())
                    {

                        mailMessage.Subject = "Приватна клініка Hospital";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<h1>Дякую, що Ви обрали нас.</h1>" +
                            "<p>ВІДПОВІДАЛЬНІСТЬ - ЦЕ ГОЛОВНЕ КРЕДО КОЛЕКТИВУ «HOSPITAL» І МИ ГОТОВІ ЇЇ НЕСТИ!</p>" +
                            "<h1>ЧОМУ САМЕ HOSPITAL?</h1>" +
                            "<b>«HOSPITAL» почав свою діяльність 12 січня 2018 року. За час нашої роботи наша клініка значно збільшила свої виробничі можливості, завдяки чому стало можливим виконання широкого спектру лабораторних досліджень. Високі виробничі потужності дозволили створити широку мережу маніпуляційних кабінетів для забору біоматеріалу для лабораторного обстеження. Девізом «Hospital» є: «Якість у нас в крові». <u>Якісне лікування – запорука успішного лікування.</u></b>";
                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, "doctorhospital");
                        smtpClient.Send(mailMessage);
                    }
                    control = true;
                    Form1 frm = new Form1(l);
                    frm.Show();
                    this.Close();
                }
                else MessageBox.Show("Такий пацієнт вже існує!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Provider"))
                {
                    control = true;
                    MessageBox.Show("Не вдалось під'єднатись до бази даних. Будь ласка, зверніться до системного адміністратора", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    l.Show();
                }
                else MessageBox.Show("Не всі дані заповнено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();

            if (videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPatient();
            ErrorProvider.ReferenceEquals(textBox5, "");

            if (videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void Form3_1_FormClosing(object sender, FormClosingEventArgs e)
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

        private void Form3_1_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            videoSource = new VideoCaptureDevice(videoDevices[comboBox2.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
        }

        private void video_NewFrame (object sender, NewFrameEventArgs eventArgs)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Зберегти як ...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.FileName = maskedTextBox1.Text;
                savedialog.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
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

        private void maskedTextBox1_Enter(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "АА000000") maskedTextBox1.Clear();
        }

        private void maskedTextBox3_Enter(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "АА000000") maskedTextBox1.Clear();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }

        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            if (emailAddress.Length == 0)
            {
                errorMessage = "e-mail address is required.";
                return false;
            }

            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "e-mail address must be valid e-mail address format.\n" +
               "For example 'someone@example.com' ";
            return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                maskedTextBox1.Enabled = true;
                maskedTextBox4.Enabled = false;
            }
            else
            {
                maskedTextBox1.Enabled = false;
                maskedTextBox4.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                maskedTextBox1.Enabled = false;
                maskedTextBox4.Enabled = true;
            }
            else
            {
                maskedTextBox1.Enabled = false;
                maskedTextBox4.Enabled = false;
            }
        }

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
        }
    }
}
