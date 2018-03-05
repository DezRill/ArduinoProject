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
    public partial class Form6 : Form
    {
        Login l;

        public Form6(Login l)
        {
            InitializeComponent();
            this.l = l;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void SaveSession()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.sessions(doctor_id, session_begin, session_end, date) VALUES (@doctor_id, @session_begin, @session_end, @date)";

            command.Parameters.AddWithValue("@doctor_id", l.doc.Id);
            command.Parameters.AddWithValue("@session_begin", dateTimePicker1.Value.ToString("HH:mm"));
            command.Parameters.AddWithValue("@session_end", dateTimePicker2.Value.ToString("HH:mm"));
            command.Parameters.AddWithValue("@date", DateTime.Now.Date.ToString());

            command.ExecuteNonQuery();
        }

        private void SaveHistory()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.history(name, birth, email, gender, temperature, oxygen, pressure, growth, weight, symptoms, recommendations, diagnosis) VALUES (@name, @birth, @email, @gender, @temperature, @oxygen, @pressure, @growth, @weight, @symptoms, @recommendations, @diagnosis)";

            command.Parameters.AddWithValue("@name", textBox11.Text);
            command.Parameters.AddWithValue("@birth", dateTimePicker3.Value.Date.ToString());
            command.Parameters.AddWithValue("@email", textBox13.Text);
            command.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@temperature", textBox7.Text);
            command.Parameters.AddWithValue("@oxygen", textBox6.Text);
            command.Parameters.AddWithValue("@pressure", textBox4.Text);
            command.Parameters.AddWithValue("@growth", textBox10.Text);
            command.Parameters.AddWithValue("@weight", textBox8.Text);
            command.Parameters.AddWithValue("@symptoms", textBox2.Text);
            command.Parameters.AddWithValue("@recommendations", textBox3.Text);
            command.Parameters.AddWithValue("diagnosis", textBox9.Text);

            command.ExecuteNonQuery();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSession();
                SaveHistory();

                MessageBox.Show("Успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form1 frm = new Form1(l);
                frm.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не всі поля заповнено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9();
            frm.Show();
            this.Close();
        }
    }
}
