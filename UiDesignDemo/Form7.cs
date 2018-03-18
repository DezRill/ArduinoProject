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
    public partial class Form7 : Form
    {
        Form4 f4;
        Form1 f1;
        bool form4;

        bool control = false;

        public Form7(Form4 f)
        {
            InitializeComponent();
            this.f4 = f;
            GetHistory(f.patient.Id);
            form4 = true;
        }

        public Form7(Form1 f, int action)
        {
            InitializeComponent();
            this.f1 = f;
            if (action == 1)
            {
                GetExpressHistory();
                this.Width += 100;
                DG1.Width += 100;
                button5.Location = new Point(button5.Location.X + 100, button5.Location.Y);
                label1.Location = new Point(label1.Location.X + 100, label1.Location.Y);
                label1.Text = "Всі експрес-огляди";
            }
            else if (action == 2)
            {
                GetAllPatients();
                this.Width -= 400;
                DG1.Width -= 400;
                button5.Location = new Point(button5.Location.X - 400, button5.Location.Y);
                label1.Location = new Point(label1.Location.X - 150, label1.Location.Y);
                label1.Text = "Всі пацієнти";
            }
            form4 = false;
        }

        private void GetHistory(int id)
        {
            SqlCommand command = f4.l.connection.CreateCommand();
            command.CommandText = "SELECT temperature AS Температура, oxygen AS [Рівень кисню], pressure AS [Артеріальний тиск], growth AS Ріст, weight AS Вага, symptoms AS Симптоми, diagnosis AS Діагноз, recommendations AS Рекомендації, doctor_name AS [Лікар, який поставив діагноз], doctor_position AS [Посада лікаря], date AS [Дата, коли був поставлений діагноз], hos_begin AS [Початок лікарняного], hos_end AS [Кінець лікарняного] FROM dbo.history INNER JOIN dbo.sessions ON dbo.history.patient_id=dbo.sessions.patient_id WHERE dbo.history.patient_id=@patient_id";
            command.Parameters.AddWithValue("@patient_id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DG1.DataSource = table;
        }

        private void GetExpressHistory()
        {
            SqlCommand command = f1.l.connection.CreateCommand();
            command.CommandText = "SELECT name AS ПІБ, birth AS [Дата народження], email AS [Електронна пошта], gender AS Стать, temperature AS Температура, oxygen AS [Рівень кисню], pressure AS [Артеріальний тиск], growth AS Ріст, weight AS Вага, symptoms AS Симптоми, diagnosis AS Діагноз, recommendations AS Рекомендації, doctor_name AS [Лікар, який поставив діагноз], doctor_position AS [Посада лікаря] FROM dbo.history WHERE name IS NOT NULL";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DG1.DataSource = table;
        }

        private void GetAllPatients()
        {
            SqlCommand command = f1.l.connection.CreateCommand();
            command.CommandText = "SELECT passport AS Паспорт, name AS ПІБ, birth AS [Дата народження], gender AS Стать, phone AS [Номер телефону], mail AS [Електронна пошта], town AS [Місто проживання], adress AS [Адреса проживання], reg_date AS [Дата реєстрації] FROM dbo.patients";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DG1.DataSource = table;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            control = true;
            this.Close();
            if (form4) f4.Show();
            else f1.Show();
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
