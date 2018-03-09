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
        Form4 f;

        bool control = false;

        public Form7(Form4 f)
        {
            InitializeComponent();
            this.f = f;
            GetHistory(f.patient.Id);
        }

        private void GetHistory(int id)
        {
            SqlCommand command = f.l.connection.CreateCommand();
            command.CommandText = "SELECT temperature AS Температура, oxygen AS [Рівень кисню], pressure AS [Артеріальний тиск], growth AS Ріст, weight AS Вага, symptoms AS Симптоми, diagnosis AS Діагноз, recommendations AS Рекомендації, doctor_name AS [Лікар, який поставив діагноз], date AS [Дата, коли був поставлений діагноз], hos_begin AS [Початок лікарняного], hos_end AS [Кінець лікарняного] FROM dbo.history INNER JOIN dbo.sessions ON dbo.history.patient_id=dbo.sessions.patient_id WHERE dbo.history.patient_id=@patient_id";
            command.Parameters.AddWithValue("@patient_id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DG1.DataSource = table;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            control = true;
            this.Close();
            f.Show();
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
