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
                command.Parameters.AddWithValue("@passport", maskedTextBox1.Text);
                command.Parameters.AddWithValue("@name", textBox6.Text);
                command.Parameters.AddWithValue("@birth", dateTimePicker1.Value.Date.ToString());
                command.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
                command.Parameters.AddWithValue("@town", textBox10.Text);
                command.Parameters.AddWithValue("@adress", textBox8.Text);
                command.Parameters.AddWithValue("@phone", maskedTextBox2.Text);
                command.Parameters.AddWithValue("@mail", textBox5.Text);
                command.Parameters.AddWithValue("@photo", ConvertImageToBinary(pictureBox2.Image));
                command.Parameters.AddWithValue("@reg_date", dateTimePicker2.Value.Date.ToString());

                command.ExecuteNonQuery();
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
                    mailMessage.Body = "<h1>Дякую, що Ви обрали нас.</h1>" +
                        "<p>ВІДПОВІДАЛЬНІСТЬ - ЦЕ ГОЛОВНЕ КРЕДО КОЛЕКТИВУ «HOSPITAL» І МИ ГОТОВІ ЇЇ НЕСТИ!</p>" +
                        "<h1>ЧОМУ САМЕ HOSPITAL?</h1>" +
                        "<b>ДЦ «HOSPITAL» почав свою діяльність 7 липня 2009 року. За роки своєї роботи наша клініка значно збільшила свої виробничі можливості, завдяки чому стало можливим виконання широкого спектру лабораторних досліджень. Високі виробничі потужності дозволили створити широку мережу маніпуляційних кабінетів для забору біоматеріалу для лабораторного обстеження. Девізом ДЦ «Hospital» є: «Якість у нас в крові». <u>Якісне лікування – запорука успішного лікування.</u></b>";
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

            if (videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            UploadImage();
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
                
             comboBox2.SelectedIndex = 1;
            }
            else 
                MessageBox.Show("Немає підключених камер!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (pictureBox2.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Зберегти як ...";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                savedialog.FileName = maskedTextBox1.Text;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //string strIn = @"89154246837,9154246837, +79154246837, 8 915 4246837, 8(915)424-68-37, 8915 424 68-37, 8915 424 68 37, 8-9-1-5-4-2-4-6-8-3-7, 9-1-5-4-2-4-6-8-3-7";
            //string strPattern = @"(\+7|8|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)";
            //MatchCollection mm = Regex.Matches(strIn, strPattern);
            //foreach (Match m in mm)
            //{
            //    Console.WriteLine("{0} ==> {1}", m.Value, Regex.Replace(m.Value, "^" + strPattern + "$", "+7($2$3$4)$5$6$7-$8$9-$10$11"));
            //}

            ////const string myReg1 = @"((\+38|8)[ ]?)?([(]?\d{3}[)]?[\- ]?)?[\d\-]{6,14}";


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
    }
}
