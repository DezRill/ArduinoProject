using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace UiDesignDemo
{
    public partial class Form10 : Form
    {
        Login f;

        public Form10(Login f)
        {
            InitializeComponent();
            this.f = f;

            textBox1.Text = f.settings.DataSource;
            textBox2.Text = f.settings.InitialCatalog;
            textBox3.Text = f.settings.UserID;
            textBox4.Text = f.settings.Password;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
            f.Show();
        }

        private void SaveSettings()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "") MessageBox.Show("Не всі поля заповнено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                f.settings.DataSource = textBox1.Text;
                f.settings.InitialCatalog = textBox2.Text;
                f.settings.UserID = textBox3.Text;
                f.settings.Password = textBox4.Text;
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    FileStream fs = new FileStream("settings.phz", FileMode.Create);
                    formatter.Serialize(fs, f.settings);
                    fs.Close();
                    this.Close();
                    f.Show();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Не вдалося зберігти налаштування", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
