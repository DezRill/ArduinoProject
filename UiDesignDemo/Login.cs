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
using System.Runtime.Serialization.Formatters.Binary;

namespace UiDesignDemo
{
    public partial class Login : Form
    {
        public SqlConnection connection;
        public ActiveDoctor doc;
        public ConnectionSettings settings;

        private void LoadSettings()
        {
            settings = new ConnectionSettings();
            try
            {
                FileStream fs = new FileStream("settings.phz", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                settings = (ConnectionSettings)formatter.Deserialize(fs);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Не вдалося завантажити налаштування підключення. Будь ласка, зверніться до системного адміністратора.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConnectToDataBase()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = settings.DataSource;
            builder.InitialCatalog = settings.InitialCatalog;
            builder.UserID = settings.UserID;
            builder.Password = settings.Password;
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
            try
            {
                ConnectToDataBase();

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
                    doc.Id = Convert.ToInt32(table.Rows[0]["id"].ToString());
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
            catch
            {
                MessageBox.Show("Не вдалось під'єднатись до бази даних. Будь ласка, зверніться до системного адміністратора", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Login()
        {
            InitializeComponent();
            LoadSettings();
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

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form10 frm = new Form10(this);
            frm.Show();
            this.Hide();
        }
    }
}

