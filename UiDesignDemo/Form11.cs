using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

namespace UiDesignDemo
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

   

        private void textBox1_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // kuzichkaa @gmail.com"
            //ankuzmynykh @gmail.com
            //olusjkalike@gmail.com
            MailAddress fromMailAddress = new MailAddress("hospitalmaindoctor@gmail.com");
            //   MailAddress toAddress = new MailAddress(f.patient.Mail);
            MailAddress toAddress = new MailAddress("kuzmynykh98@gmail.com");

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toAddress))
            using (SmtpClient smtpClient = new SmtpClient())
            {
               
                mailMessage.Subject = "Рекомендації від приватній клініці Hospital";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<h1>This is a test email</h1>";
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, "doctorhospital");
                smtpClient.Send(mailMessage);
            }
        }
    }
}
