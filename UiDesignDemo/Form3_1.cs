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
namespace UiDesignDemo
{
    public partial class Form3_1 : Form
    {
        Login l;

        private FilterInfoCollection videoDevices; //отримання списоку пристороїв камер 
        private VideoCaptureDevice videoSource; // 

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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form3_1_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                foreach (FilterInfo device in videoDevices)
                {
                    lbCams.Items.Add(device.Name);
                }
                lbCams.SelectedIndex = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            videoSource = new VideoCaptureDevice(videoDevices[lbCams.SelectedIndex].MonikerString); //вибір камери зі списку
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame); // обробка відео
            videoSource.Start();
        }
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox2.Image = bitmap;

        }

        private void Form3_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.pictureBox2.Image.Save("123");
            videoSource.SignalToStop();
            videoSource.WaitForStop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Зберенти як ....";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                savedialog.FileName = textBox6.Text;
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
    }
}
