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

using System.Net;
using System.Net.Mail;

namespace UiDesignDemo
{
    public partial class Form4_2 : Form
    {
        Form4 f;
        string begin, end;

        bool control = false;

        public Form4_2(Form4 f)
        {
            InitializeComponent();
            this.f = f;

            dateTimePicker3.Value = DateTime.Now.Date;
            dateTimePicker4.Value = DateTime.Now.Date;
            begin = DateTime.Now.ToString("HH:mm");
            textBox2.Text = f.patient.Mail;
            GetDoctors();
            ControlExtension.Draggable(this, true);
        }

        private void SaveSession()
        {
            try
            {
                SqlCommand command = f.l.connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.sessions(doctor_id, patient_id, session_begin, session_end, date) VALUES (@doctor_id, @patient_id, @session_begin, @session_end, @date)";

                command.Parameters.AddWithValue("@doctor_id", f.l.doc.Id);
                command.Parameters.AddWithValue("@patient_id", f.patient.Id);
                command.Parameters.AddWithValue("@session_begin", begin);
                command.Parameters.AddWithValue("@session_end", end);
                command.Parameters.AddWithValue("@date", DateTime.Now.Date.ToString());

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Provider"))
                {
                    control = true;
                    MessageBox.Show("Не вдалось під'єднатись до бази даних. Будь ласка, зверніться до системного адміністратора", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    f.l.Show();
                }
                else MessageBox.Show("Не всі обов'язкові поля заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetDoctors()
        {
            SqlCommand command = f.l.connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT position FROM dbo.doctors";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);


            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox1.Items.Add(table.Rows[i]["position"].ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void SaveHistory()
        {
            try
            {
                SqlCommand command = f.l.connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.history(patient_id, doctor_name, doctor_position, temperature, oxygen, pressure, growth, weight, symptoms, recommendations, diagnosis, date, hos_begin, hos_end, inspection, direction) VALUES (@patient_id, @doctor_name, @doctor_position, @temperature, @oxygen, @pressure, @growth, @weight, @symptoms, @recommendations, @diagnosis, @date, @hos_begin, @hos_end, @inspection, @direction)";

                command.Parameters.AddWithValue("@patient_id", f.patient.Id);
                command.Parameters.AddWithValue("@doctor_name", f.l.doc.Name);
                command.Parameters.AddWithValue("@doctor_position", f.l.doc.Position);
                if (textBox7.Text != "") command.Parameters.AddWithValue("@temperature", textBox7.Text);
                else command.Parameters.AddWithValue("@temperature", "");
                if (textBox6.Text != "") command.Parameters.AddWithValue("@oxygen", textBox6.Text);
                else command.Parameters.AddWithValue("@oxygen", "");
                if (textBox4.Text != "") command.Parameters.AddWithValue("@pressure", textBox4.Text);
                else command.Parameters.AddWithValue("@pressure", "");
                if (textBox10.Text != "") command.Parameters.AddWithValue("@growth", textBox10.Text);
                else command.Parameters.AddWithValue("@growth", "");
                if (textBox8.Text != "") command.Parameters.AddWithValue("weight", textBox8.Text);
                else command.Parameters.AddWithValue("weight", "");
                command.Parameters.AddWithValue("@symptoms", textBox1.Text);
                command.Parameters.AddWithValue("@recommendations", textBox5.Text);
                command.Parameters.AddWithValue("@diagnosis", textBox9.Text);
                command.Parameters.AddWithValue("@date", DateTime.Now.Date.ToString());
                command.Parameters.AddWithValue("@hos_begin", dateTimePicker3.Value.Date.ToString());
                command.Parameters.AddWithValue("@hos_end", dateTimePicker4.Value.Date.ToString());
                command.Parameters.AddWithValue("@inspection", textBox3.Text);
                if (checkBox1.Checked) command.Parameters.AddWithValue("@direction", comboBox1.SelectedItem.ToString());
                else command.Parameters.AddWithValue("@direction", "");

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Provider"))
                {
                    control = true;
                    MessageBox.Show("Не вдалось під'єднатись до бази даних. Будь ласка, зверніться до системного адміністратора", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    f.l.Show();
                }
                else MessageBox.Show("Не всі обов'язкові поля заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            f.Show();
            this.Close();
        }

        private void Form4_2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox1.Enabled = true;
            }
            else comboBox1.Enabled = false;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == ',') && (textBox7.Text.IndexOf(",") == -1) && (textBox7.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back) e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == ',') && (textBox6.Text.IndexOf(",") == -1) && (textBox6.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back) e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == ',') && (textBox10.Text.IndexOf(",") == -1) && (textBox10.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back) e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == ',') && (textBox8.Text.IndexOf(",") == -1) && (textBox8.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back) e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '/') && (textBox7.Text.IndexOf("/") == -1) && (textBox7.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back) e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Print print = new Print(f.l.doc.Name, textBox3.Text, textBox9.Text, textBox5.Text);
            print.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MailAddress fromMailAddress = new MailAddress("hospitalmaindoctor@gmail.com");
            MailAddress toAddress = new MailAddress(textBox2.Text);

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toAddress))
            using (SmtpClient smtpClient = new SmtpClient())
            {
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Рекомендації від приватній клініці Hospital";
                mailMessage.Body = "<b>РЕКОМЕНДАЦЇ ЛІКАРЯ:</b>" +
                "<br></br>" + textBox5.Text;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, "doctorhospital");
                smtpClient.Send(mailMessage);
            }
            end = DateTime.Now.ToString("HH:mm");
            SaveSession();
            SaveHistory();

            MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

            control = true;
            f.Show();
            this.Close();
        }
    }
}
