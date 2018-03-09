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
        }

        private void SaveSession()
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

        private void SaveHistory()
        {
            SqlCommand command = f.l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.history(patient_id, doctor_name, temperature, oxygen, pressure, growth, weight, symptoms, recommendations, diagnosis, hos_begin, hos_end) VALUES (@patient_id, @doctor_name, @temperature, @oxygen, @pressure, @growth, @weight, @symptoms, @recommendations, @diagnosis, @hos_begin, @hos_end)";

            command.Parameters.AddWithValue("@patient_id", f.patient.Id);
            command.Parameters.AddWithValue("@doctor_name", f.l.doc.Name);
            command.Parameters.AddWithValue("@temperature", Convert.ToDouble(textBox7.Text));
            command.Parameters.AddWithValue("@oxygen", Convert.ToDouble(textBox6.Text));
            command.Parameters.AddWithValue("@pressure", Convert.ToDouble(textBox4.Text));
            command.Parameters.AddWithValue("@growth", Convert.ToDouble(textBox10.Text));
            command.Parameters.AddWithValue("weight", Convert.ToDouble(textBox8.Text));
            command.Parameters.AddWithValue("@symptoms", textBox1.Text);
            command.Parameters.AddWithValue("@recommendations", textBox5.Text);
            command.Parameters.AddWithValue("@diagnosis", textBox9.Text);
            command.Parameters.AddWithValue("@hos_begin", dateTimePicker3.Value.Date.ToString());
            command.Parameters.AddWithValue("@hos_end", dateTimePicker4.Value.Date.ToString());

            command.ExecuteNonQuery();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                end = DateTime.Now.ToString("HH:mm");
                SaveSession();
                SaveHistory();

                MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                control = true;
                f.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не всі поля заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
