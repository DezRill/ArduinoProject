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

        public Form4_2(Form4 f)
        {
            InitializeComponent();
            this.f = f;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now.Date;
            dateTimePicker4.Value = DateTime.Now.Date;
        }

        private void SaveSession()
        {
            SqlCommand command = f.l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.sessions(doctor_id, patient_id, session_begin, session_end, date) VALUES (@doctor_id, @patient_id, @session_begin, @session_end, @date)";

            command.Parameters.AddWithValue("@doctor_id", f.l.doc.Id);
            command.Parameters.AddWithValue("@patient_id", f.patient.Id);
            command.Parameters.AddWithValue("@session_begin", dateTimePicker1.Value.ToString("HH:mm"));
            command.Parameters.AddWithValue("@session_end", dateTimePicker2.Value.ToString("HH:mm"));
            command.Parameters.AddWithValue("@date", DateTime.Now.Date.ToString());

            command.ExecuteNonQuery();
        }

        private void SaveHistory()
        {
            SqlCommand command = f.l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.history(patient_id, temperature, oxygen, pressure, growth, weight, symptoms, recommendations, diagnosis, hos_begin, hos_end) VALUES (@patient_id, @temperature, @oxygen, @pressure, @growth, @weight, @symptoms, @recommendations, @diagnosis, @hos_begin, @hos_end)";

            command.Parameters.AddWithValue("@patient_id", f.patient.Id);
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
            f.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSession();
                SaveHistory();

                MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                f.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не всі поля заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9(this);
            frm.Show();
        }
    }
}
