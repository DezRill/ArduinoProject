using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UiDesignDemo
{
    public partial class Form14 : Form
    {
        Login l;
        int id;
        bool control = false;

        bool CheckDate()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.schedule WHERE doctor_id=@doctor_id AND date=@date";
            command.Parameters.AddWithValue("@doctor_id", id);
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count != 0) return false;
            else return true;
        }

        void AddDay()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.schedule(doctor_id, date, time_begin, time_end) VALUES (@doctor_id, @date, @time_begin, @time_end)";
            command.Parameters.AddWithValue("@doctor_id", id);
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
            command.Parameters.AddWithValue("@time_begin", dateTimePicker2.Value.ToString("HH:mm"));
            command.Parameters.AddWithValue("@time_end", dateTimePicker3.Value.ToString("HH:mm"));

            command.ExecuteNonQuery();
        }

        public Form14(Login l, int id)
        {
            InitializeComponent();
            this.l = l;
            this.id = id;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form13 frm = new Form13(l, id);
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDate())
                {
                    AddDay();
                    MessageBox.Show("Успішно збережено!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Графік роботи в цей день вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form14_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
