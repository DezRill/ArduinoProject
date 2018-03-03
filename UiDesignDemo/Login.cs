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
    public partial class Login : Form
    {
        public SqlConnection connection;
        public ActiveDoctor doc;

        private void ConnectToDataBase()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "arduino.zapto.org";
            builder.InitialCatalog = "Hospital";
            builder.UserID = "admin";
            builder.Password = "admin";
            connection = new SqlConnection(builder.ToString());
            connection.Open();
        }

        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        private void SingIn(string login, string password)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.doctors WHERE login=@login AND password=@password";
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count != 0)
            {
                doc = new ActiveDoctor();
                doc.Name = table.Rows[0]["name"].ToString();
                doc.Birth = Convert.ToDateTime(table.Rows[0]["birth"].ToString());
                doc.Gender = table.Rows[0]["gender"].ToString();
                doc.Town = table.Rows[0]["town"].ToString();
                doc.Phone = table.Rows[0]["phone"].ToString();
                doc.Mail = table.Rows[0]["mail"].ToString();
                doc.Adress = table.Rows[0]["adress"].ToString();
                doc.Photo = ConvertBinaryToImage((byte[])table.Rows[0]["photo"]);
                doc.Passport = table.Rows[0]["passport"].ToString();
                doc.Diploma = table.Rows[0]["diploma_num"].ToString();
                doc.Specialty = table.Rows[0]["specialty"].ToString();
                doc.Position = table.Rows[0]["position"].ToString();
                doc.Invite = Convert.ToDateTime(table.Rows[0]["invite_date"].ToString());
                doc.Characteristic = table.Rows[0]["short_char"].ToString();
                adapter.Dispose();
                table.Dispose();
                Form1 frm = new Form1(this);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Login()
        {
            InitializeComponent();
            try
            {
                ConnectToDataBase();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SingIn(textBox1.Text, textBox2.Text);
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

        private void Login_Load(object sender, EventArgs e)
        {
            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2),
                (ScreenHeight / 2) - (this.Height / 2));
        }
    }
}

