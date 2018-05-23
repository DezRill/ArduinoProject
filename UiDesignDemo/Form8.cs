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
    public partial class Form8 : Form
    {
        public Login l;

        bool control = false;

        public Form8(Login l)
        {
            InitializeComponent();
            this.l = l;
            if (l.doc.Position == "Головний лікар")
            {
                label3.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
            }
            GetDoctors();
        }

        private void CreateNameLabel(int i, string text)
        {
            Label name = new Label();
            name.AutoSize = true;
            name.Font = new Font("Microsoft Sans Serif", 11.25F);
            name.Location = new Point(17, 30 * i);
            name.Size = new Size(46, 18);
            name.Name = "name" + i.ToString();
            name.Text = text;
            panel4.Controls.Add(name);
        }

        private void CreatePositionLabel(int i, string text)
        {
            Label position = new Label();
            position.AutoSize = true;
            position.Font = new Font("Microsoft Sans Serif", 11.25F);
            position.Location = new Point(401, 30 * i);
            position.Size = new Size(46, 18);
            position.Name = "position" + i.ToString();
            position.Text = text;
            panel4.Controls.Add(position);
        }

        private void CreateCommandButtons(int i, int id, bool main)
        {
            Button edit = new Button();
            Button delete = new Button();

            edit.Name = "e" + id.ToString();
            edit.Text = "✎";
            edit.Font = new Font("Microsoft Sans Serif", 11.25F);
            edit.Width = 27;
            edit.Height = 27;
            edit.Location = new Point(782, 30 * i);
            edit.Click += new EventHandler(editEvent);
            panel4.Controls.Add(edit);

            if (!main)
            {
                delete.Name = "d" + id.ToString();
                delete.Text = "×";
                delete.Font = new Font("Microsoft Sans Serif", 11.25F);
                delete.Width = 27;
                delete.Height = 27;
                delete.Location = new Point(815, 30 * i);
                delete.Click += new EventHandler(deleteEvent);
                panel4.Controls.Add(delete);
            }

            void editEvent(object sender, EventArgs e)
            {
                control = true;
                try
                {
                    Form5 frm = new Form5(l, id);
                    frm.Show();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Не вдалось під'єднатись до бази даних. Будь ласка, зверніться до системного адміністратора", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    l.Show();
                }
            }

            void deleteEvent(object sender, EventArgs e)
            {
                var message = MessageBox.Show("Ви дійсно хочете видалити лікаря?", "Попередження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (message == DialogResult.Yes)
                {
                    DeleteDoctor(id);
                    panel4.Controls.Clear();
                    GetDoctors();
                }
                else return;
            }
        }

        private void DeleteDoctor(int id)
        {
            try
            {
                SqlCommand command = l.connection.CreateCommand();
                command.CommandText = "DELETE FROM dbo.doctors WHERE id=@id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                MessageBox.Show("Успішно видалено", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                control = true;
                MessageBox.Show("Не вдалось під'єднатись до бази даних. Будь ласка, зверніться до системного адміністратора", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                l.Show();
            }
        }

        private void GetDoctors()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT name, position, id FROM dbo.doctors";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                CreateNameLabel(i, table.Rows[i]["name"].ToString());
                CreatePositionLabel(i, table.Rows[i]["position"].ToString());
                if (l.doc.Position == "Головний лікар")
                {
                    if (table.Rows[i]["position"].ToString() == "Головний лікар") CreateCommandButtons(i, Convert.ToInt32(table.Rows[i]["id"].ToString()), true);
                    else CreateCommandButtons(i, Convert.ToInt32(table.Rows[i]["id"].ToString()), false);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            control = true;
            Form5 frm = new Form5(l);
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2),
                (ScreenHeight / 2) - (this.Height / 2));
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
