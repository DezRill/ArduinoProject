using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;

namespace UiDesignDemo
{
    public partial class WebCamera : Form
    {

        private FilterInfoCollection videoDevices; //отримання списоку пристороїв камер 
        private VideoCaptureDevice videoSource; // 

        public WebCamera()
        {
            InitializeComponent();
        }

        private void WebCamera_Load(object sender, EventArgs e)
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            videoSource = new VideoCaptureDevice(videoDevices[lbCams.SelectedIndex].MonikerString); //вибір камери зі списку
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame); // обробка відео
            videoSource.Start();
        }

        private void video_NewFrame (object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pbWebCamPreview.Image = bitmap;

        }

        private void WebCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // pbWebCamPreview.Image = (Image)Clipboard.GetImage().Clone();
            //pbWebCamPreview.Image = Clipboard.GetImage();
            //pbWebCamPreview.Invalidate();
            this.pbWebCamPreview.Image.Save("123");
            videoSource.SignalToStop();
            videoSource.WaitForStop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pbWebCamPreview.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Зберенти як ....";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                savedialog.FileName = textBox1.Text; 
                //список форматов файла, отображаемый в поле "Тип файла"
                savedialog.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                //отображается ли кнопка "Справка" в диалоговом окне
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        pbWebCamPreview.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
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
