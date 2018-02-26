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
    public partial class Login : Form
    {
        public SqlConnection connection;
        public ActiveDoctor doc;

        private void connectToDataBase()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "arduino.zapto.org";
            builder.InitialCatalog = "Hospital";
            builder.UserID = "admin";
            builder.Password = "admin";
            connection = new SqlConnection(builder.ToString());
            connection.Open();
        }

        private void singIn(string login, string password)
        {
            bool isFound = false;
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.doctors";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            adapter.Fill(table);
            for (int i=0;i<table.Rows.Count;i++)
            {
                if (login==table.Rows[i]["login"].ToString() && password==table.Rows[i]["password"].ToString())
                {
                    doc = new ActiveDoctor();
                    doc.Id = Convert.ToInt32(table.Rows[i]["id"].ToString());
                    doc.Name = table.Rows[i]["name"].ToString();
                    doc.Birth = Convert.ToDateTime(table.Rows[i]["birth"].ToString());
                    doc.Gender = table.Rows[i]["gender"].ToString();
                    doc.Town = table.Rows[i]["town"].ToString();
                    doc.Phone = table.Rows[i]["phone"].ToString();
                    doc.Mail = table.Rows[i]["mail"].ToString();
                    doc.Adress = table.Rows[i]["adress"].ToString();
                    doc.Passport = table.Rows[i]["passport"].ToString();
                    doc.Diploma = Convert.ToInt32(table.Rows[i]["diploma_num"].ToString());
                    doc.Specialty = table.Rows[i]["specialty"].ToString();
                    doc.Position = table.Rows[i]["position"].ToString();
                    doc.Invite = Convert.ToDateTime(table.Rows[i]["invite_date"].ToString());
                    doc.Characteristic = table.Rows[i]["short_char"].ToString();
                    isFound = true;
                    Form1 frm = new Form1(this);
                    frm.Show();
                    this.Hide();
                    break;
                }
            }
            if (!isFound)
            {
                MessageBox.Show("Помилка", "Невірний логін або пароль", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Login()
        {
            InitializeComponent();
            try
            {
                connectToDataBase();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(this);
            frm.Show();
            this.Hide();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Username") textBox1.Clear();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            if (textBox2.Text == "Password") textBox2.Clear();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

