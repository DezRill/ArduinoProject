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
    public partial class Form13 : Form
    {
        Login l;
        DataTable table = new DataTable();
        int id;
        bool control = false;

        public void GetSchedule(DataTable table)
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT date, time_begin, time_end FROM dbo.schedule WHERE doctor_id=@doctor_id";
            command.Parameters.AddWithValue("@doctor_id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
        }

        static DateTime GetMonday(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday) date = date.AddDays(-1);
            return date;
        }

        static DateTime GetFriday(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Friday) date = date.AddDays(1);
            return date;
        }

        void RenderThisWeek()
        {
            DG1.Rows.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (Convert.ToDateTime(table.Rows[i]["date"].ToString()).Date >= GetMonday(DateTime.Now.Date) && Convert.ToDateTime(table.Rows[i]["date"].ToString()).Date <= GetFriday(DateTime.Now.Date))
                {
                    DG1.Rows.Add(table.Rows[i]["date"].ToString(), table.Rows[i]["time_begin"].ToString(), table.Rows[i]["time_end"].ToString());
                }
            }
        }

        void RenderAllTime()
        {
            DG1.Rows.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DG1.Rows.Add(table.Rows[i]["date"].ToString(), table.Rows[i]["time_begin"].ToString(), table.Rows[i]["time_end"].ToString());
            }
        }

        public Form13(Login l, int id)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            this.l = l;
            this.id = id;
            GetSchedule(table);
            RenderThisWeek();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form12 frm = new Form12(l);
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            control = true;
            Form14 frm = new Form14(l, id);
            frm.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) RenderThisWeek();
            else RenderAllTime();
        }

        private void Form13_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
