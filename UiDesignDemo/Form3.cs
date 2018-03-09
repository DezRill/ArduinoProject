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
using System.IO;

namespace UiDesignDemo
{
    public partial class Form3 : Form
    {
        public Login l;
        private Patient patient;

        bool control = false;

        public Form3(Login l)
        {
            InitializeComponent();
            this.l = l;
        }

        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        private void FindPatient()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.patients WHERE passport=@passport";
            command.Parameters.AddWithValue("@passport", textBox1.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count!=0)
            {
                patient = new Patient();
                patient.Id = Convert.ToInt32(table.Rows[0]["id"].ToString());
                patient.Passport = table.Rows[0]["passport"].ToString();
                patient.Name = table.Rows[0]["name"].ToString();
                patient.Birth = Convert.ToDateTime(table.Rows[0]["birth"].ToString());
                patient.Gender = table.Rows[0]["gender"].ToString();
                patient.Town = table.Rows[0]["town"].ToString();
                patient.Phone = table.Rows[0]["phone"].ToString();
                patient.Mail = table.Rows[0]["mail"].ToString();
                patient.Adress = table.Rows[0]["adress"].ToString();
                patient.Photo = ConvertBinaryToImage((byte[])table.Rows[0]["photo"]);
                patient.Reg_Date = Convert.ToDateTime(table.Rows[0]["reg_date"].ToString());
                adapter.Dispose();
                table.Dispose();
                control = true;
                Form4 frm = new Form4(l, patient);
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Такого пацієнта не існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindPatient();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            control = true;
            Form3_1 frm = new Form3_1(l);
            frm.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
