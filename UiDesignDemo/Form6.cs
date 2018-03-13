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
        string begin, end;

        bool control = false;

        public Form6(Login l)
        {
            InitializeComponent();
            this.l = l;
            begin = DateTime.Now.ToString("HH:mm");
            GetDoctors();
        }

        private void GetDoctors()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT position FROM dbo.doctors GROUP BY position";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox2.Items.Add(table.Rows[i]["position"].ToString());
            }
            comboBox2.SelectedIndex = 0;
        }

        private void SaveSession()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.sessions(doctor_id, session_begin, session_end, date) VALUES (@doctor_id, @session_begin, @session_end, @date)";

            command.Parameters.AddWithValue("@doctor_id", l.doc.Id);
            command.Parameters.AddWithValue("@session_begin", begin);
            command.Parameters.AddWithValue("@session_end", end);
            command.Parameters.AddWithValue("@date", DateTime.Now.Date.ToString());

            command.ExecuteNonQuery();
        }

        private void SaveHistory()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.history(doctor_name, doctor_position, name, birth, email, gender, temperature, oxygen, pressure, growth, weight, symptoms, recommendations, diagnosis, inspection, direction) VALUES (@doctor_name, @doctor_position, @name, @birth, @email, @gender, @temperature, @oxygen, @pressure, @growth, @weight, @symptoms, @recommendations, @diagnosis, @inspection, @direction)";

            command.Parameters.AddWithValue("@doctor_name", l.doc.Name);
            command.Parameters.AddWithValue("@doctor_position", l.doc.Position);
            command.Parameters.AddWithValue("@name", textBox11.Text);
            command.Parameters.AddWithValue("@birth", dateTimePicker3.Value.Date.ToString());
            command.Parameters.AddWithValue("@email", textBox13.Text);
            command.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
            if (textBox7.Text != "") command.Parameters.AddWithValue("@temperature", Convert.ToDouble(textBox7.Text));
            else command.Parameters.AddWithValue("@temperature", "");
            if (textBox6.Text != "") command.Parameters.AddWithValue("@oxygen", Convert.ToDouble(textBox6.Text));
            else command.Parameters.AddWithValue("@oxygen", "");
            if (textBox4.Text != "") command.Parameters.AddWithValue("@pressure", Convert.ToDouble(textBox4.Text));
            else command.Parameters.AddWithValue("@pressure", "");
            if (textBox10.Text != "") command.Parameters.AddWithValue("@growth", Convert.ToDouble(textBox10.Text));
            else command.Parameters.AddWithValue("@growth", "");
            if (textBox8.Text != "") command.Parameters.AddWithValue("weight", Convert.ToDouble(textBox8.Text));
            else command.Parameters.AddWithValue("weight", "");
            command.Parameters.AddWithValue("@symptoms", textBox2.Text);
            command.Parameters.AddWithValue("@recommendations", textBox3.Text);
            command.Parameters.AddWithValue("diagnosis", textBox9.Text);
            command.Parameters.AddWithValue("@inspection", textBox1.Text);
            if (checkBox1.Checked) command.Parameters.AddWithValue("@direction", comboBox2.SelectedItem.ToString());
            else command.Parameters.AddWithValue("@direction", "");

            command.ExecuteNonQuery();

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
                Form1 frm = new Form1(l);
                frm.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не всі обов'язкові поля заповнено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) comboBox2.Enabled = true;
            else comboBox2.Enabled = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }
    }
}
