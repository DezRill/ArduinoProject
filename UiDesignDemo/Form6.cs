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
            ControlExtension.Draggable(this, true);
        }

        private void GetDoctors()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT position FROM dbo.doctors";
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
            try
            {
                SqlCommand command = l.connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.sessions(doctor_id, session_begin, session_end, date) VALUES (@doctor_id, @session_begin, @session_end, @date)";

                command.Parameters.AddWithValue("@doctor_id", l.doc.Id);
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
                    l.Show();
                }
                else MessageBox.Show("Не всі обов'язкові поля заповнено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveHistory()
        {
            try
            {
                SqlCommand command = l.connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.history(doctor_name, doctor_position, name, birth, email, gender, temperature, oxygen, pressure, growth, weight, symptoms, recommendations, diagnosis, date, inspection, direction) VALUES (@doctor_name, @doctor_position, @name, @birth, @email, @gender, @temperature, @oxygen, @pressure, @growth, @weight, @symptoms, @recommendations, @diagnosis, @date, @inspection, @direction)";

                command.Parameters.AddWithValue("@doctor_name", l.doc.Name);
                command.Parameters.AddWithValue("@doctor_position", l.doc.Position);
                command.Parameters.AddWithValue("@name", textBox11.Text);
                command.Parameters.AddWithValue("@birth", dateTimePicker3.Value.Date.ToString());
                command.Parameters.AddWithValue("@email", textBox13.Text);
                command.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
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
                command.Parameters.AddWithValue("@symptoms", textBox2.Text);
                command.Parameters.AddWithValue("@recommendations", textBox3.Text);
                command.Parameters.AddWithValue("diagnosis", textBox9.Text);
                command.Parameters.AddWithValue("@date", DateTime.Now.Date.ToString());
                command.Parameters.AddWithValue("@inspection", textBox1.Text);
                if (checkBox1.Checked) command.Parameters.AddWithValue("@direction", comboBox2.SelectedItem.ToString());
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
                    l.Show();
                }
                else MessageBox.Show("Не всі обов'язкові поля заповнено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '/') && (textBox4.Text.IndexOf("/") == -1) && (textBox4.Text.Length != 0)))
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

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }
    }
}
