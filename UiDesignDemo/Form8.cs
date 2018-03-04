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

        public Form8(Login l)
        {
            InitializeComponent();
            this.l = l;
            if (l.doc.Position == "Головний лікар")
            {
                label3.Visible = true;
                button1.Visible = true;
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
                Form5 frm = new Form5(l, id);
                frm.Show();
                this.Close();
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
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "DELETE FROM dbo.doctors WHERE id=@id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Успішно видалено", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (table.Rows[i]["position"].ToString()=="Головний лікар") CreateCommandButtons(i, Convert.ToInt32(table.Rows[i]["id"].ToString()), true);
                    else CreateCommandButtons(i, Convert.ToInt32(table.Rows[i]["id"].ToString()), false);
                }
            }

            //for (int i = 3; i < 50; i++)
            //{
            //    Label name = new Label();
            //    name.AutoSize = true;
            //    name.Font = new Font("Microsoft Sans Serif", 11.25F);
            //    name.Location = new Point(17, 60 + 30 * i);
            //    name.Size = new Size(46, 18);
            //    name.Name = "name" + i.ToString();
            //    name.Text = "test test test test test test test test test test test";
            //    panel4.Controls.Add(name);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (l.doc.Position == "Головний лікар")
            {
                Form5 frm = new Form5(l);
                frm.Show();
                this.Close();
            }
            else MessageBox.Show("У вас немає права додавати лікарів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }
    }
}
