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
    public partial class Form16 : Form
    {
        Login l;

        DataTable directions = new DataTable();

        bool control = false;

        public Form16(Login l)
        {
            InitializeComponent();
            this.l = l;
            comboBox1.SelectedIndex = 0;
            GetDirections();
            SelectAll();
        }

        private void GetDirections()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT patient_name, patient_birth, date, time FROM dbo.directions WHERE doctor_id=@doctor_id";
            command.Parameters.AddWithValue("@doctor_id", l.doc.Id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(directions);
        }

        private void SelectAll()
        {
            DG1.Rows.Clear();
            for (int i = 0; i < directions.Rows.Count; i++)
            {
                DG1.Rows.Add(directions.Rows[i]["patient_name"].ToString(), directions.Rows[i]["patient_birth"].ToString(), directions.Rows[i]["date"].ToString(), directions.Rows[i]["time"].ToString());
            }
        }

        private void SelectPastDays()
        {
            DG1.Rows.Clear();
            for (int i = 0; i < directions.Rows.Count; i++)
            {
                if (Convert.ToDateTime(directions.Rows[i]["date"].ToString()).Date < DateTime.Now.Date)
                {
                    DG1.Rows.Add(directions.Rows[i]["patient_name"].ToString(), directions.Rows[i]["patient_birth"].ToString(), directions.Rows[i]["date"].ToString(), directions.Rows[i]["time"].ToString());
                }
            }
        }

        private void SelectThisDay()
        {
            DG1.Rows.Clear();
            for (int i = 0; i < directions.Rows.Count; i++)
            {
                if (Convert.ToDateTime(directions.Rows[i]["date"].ToString()).Date == DateTime.Now.Date)
                {
                    DG1.Rows.Add(directions.Rows[i]["patient_name"].ToString(), directions.Rows[i]["patient_birth"].ToString(), directions.Rows[i]["date"].ToString(), directions.Rows[i]["time"].ToString());
                }
            }
        }

        private void SelectFutureDays()
        {
            DG1.Rows.Clear();
            for (int i = 0; i < directions.Rows.Count; i++)
            {
                if (Convert.ToDateTime(directions.Rows[i]["date"].ToString()).Date > DateTime.Now.Date)
                {
                    DG1.Rows.Add(directions.Rows[i]["patient_name"].ToString(), directions.Rows[i]["patient_birth"].ToString(), directions.Rows[i]["date"].ToString(), directions.Rows[i]["time"].ToString());
                }
            }
        }

        private void Form16_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            control = true;
            All_doctors frm = new All_doctors(l);
            frm.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) SelectAll();
            else if (comboBox1.SelectedIndex == 1) SelectPastDays();
            else if (comboBox1.SelectedIndex == 2) SelectThisDay();
            else if (comboBox1.SelectedIndex == 3) SelectFutureDays();
        }
    }
}
