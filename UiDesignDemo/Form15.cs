using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UiDesignDemo
{
    public partial class Form15 : Form
    {
        Login l;
        DataTable doctors;

        bool control = false;

        public Form15(Login l)
        {
            InitializeComponent();
            this.l = l;
            FillComboBox();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private DataTable GetDoctors()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT id, name FROM dbo.doctors";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        private void FillComboBox()
        {
            doctors = GetDoctors();
            for (int i = 0; i < doctors.Rows.Count; i++)
            {
                comboBox1.Items.Add(doctors.Rows[i]["name"].ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Form15_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar!=(Char)Keys.Back && e.KeyChar!=(Char)Keys.Space) e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back) e.Handled = true;
        }

        private int GetDoctorId(string name)
        {
            int id = -1;
            for (int i=0;i<doctors.Rows.Count;i++)
            {
                if (doctors.Rows[i]["name"].ToString()==name)
                {
                    id = Convert.ToInt32(doctors.Rows[i]["id"].ToString());
                    break;
                }
            }
            return id;
        }

        private void Done()
        {
            try
            {
                SqlCommand command = l.connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.directions(doctor_id, patient_name, patient_birth, room, date, time) VALUES (@doctor_id, @patient_name, @patient_birth, @room, @date, @time)";
                command.Parameters.AddWithValue("@doctor_id", GetDoctorId(comboBox1.SelectedItem.ToString()));
                command.Parameters.AddWithValue("@patient_name", textBox1.Text);
                command.Parameters.AddWithValue("@patient_birth", dateTimePicker1.Value.Date.ToString());
                command.Parameters.AddWithValue("@room", textBox4.Text);
                command.Parameters.AddWithValue("@date", dateTimePicker2.Value.Date.ToString());
                command.Parameters.AddWithValue("@time", maskedTextBox1.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("Успішно виконано!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Done();

            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }
    }
}
