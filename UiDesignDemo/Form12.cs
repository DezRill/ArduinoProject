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
    public partial class Form12 : Form
    {
        bool control = false;
        DataTable doctors = new DataTable();
        Login l;

        private void GetDoctors(DataTable table)
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT name, position, id FROM dbo.doctors";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
        }

        private void CreateNameLabel(int i, string name)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font("Microsoft Sans Serif", 12.25F);
            label.Location = new Point(2, 30 * i);
            label.Name = "name" + i.ToString();
            label.Text = name;
            panel1.Controls.Add(label);
        }

        private void CreatePositionLabel(int i, string position)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font("Microsoft Sans Serif", 12.25F);
            label.Location = new Point(335, 30 * i);
            label.Name = "position" + i.ToString();
            label.Text = position;
            panel1.Controls.Add(label);
        }

        private void CreateCommandButton(int i, string id)
        {
            Button button = new Button();
            button.Name = id;
            button.Text = "Переглянути/редагувати";
            button.Font = new Font("Microsoft Sans Serif", 11.25F);
            button.AutoSize = true;
            button.Location = new Point(515, 30 * i);
            button.Click += new EventHandler(OpenScheduleEvent);
            panel1.Controls.Add(button);

            void OpenScheduleEvent(object sender, EventArgs e)
            {
                control = true;
                try
                {
                    Form13 frm = new Form13(l, Convert.ToInt32(id));
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
        }

        private void RenderDoctors()
        {
            for (int i = 0; i < doctors.Rows.Count; i++)
            {
                CreateNameLabel(i, doctors.Rows[i]["name"].ToString());
                CreatePositionLabel(i, doctors.Rows[i]["position"].ToString());
                CreateCommandButton(i, doctors.Rows[i]["id"].ToString());
            }
        }

        private void RenderConcretteDoctors(string name)
        {
            int j = 0;
            for (int i = 0; i < doctors.Rows.Count; i++)
            {
                if (doctors.Rows[i]["name"].ToString().StartsWith(name))
                {
                    CreateNameLabel(j, doctors.Rows[i]["name"].ToString());
                    CreatePositionLabel(j, doctors.Rows[i]["position"].ToString());
                    CreateCommandButton(j, doctors.Rows[i]["id"].ToString());
                    j++;
                }
            }
        }

        private void RenderConcrettePositions(string position)
        {
            int j = 0;
            for (int i = 0; i < doctors.Rows.Count; i++)
            {
                if (doctors.Rows[i]["position"].ToString() == position)
                {
                    CreateNameLabel(j, doctors.Rows[i]["name"].ToString());
                    CreatePositionLabel(j, doctors.Rows[i]["position"].ToString());
                    CreateCommandButton(j, doctors.Rows[i]["id"].ToString());
                    j++;
                }
            }
        }

        public Form12(Login l)
        {
            InitializeComponent();
            this.l = l;
            GetDoctors(doctors);
            RenderDoctors();
            FillComboBox();
        }

        private void Form12_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                panel1.Controls.Clear();
                RenderConcretteDoctors(textBox1.Text);
            }
            else
            {
                panel1.Controls.Clear();
                RenderDoctors();
            }
        }

        private DataTable GetPositions()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT position FROM dbo.doctors";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        private void FillComboBox()
        {
            comboBox1.Items.Add("Всі");
            DataTable table = GetPositions();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox1.Items.Add(table.Rows[i]["position"].ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            if (comboBox1.SelectedIndex == 0) RenderDoctors();
            else RenderConcrettePositions(comboBox1.SelectedItem.ToString());
        }      
             

        private void label1_Layout(object sender, LayoutEventArgs e)
        {
            this.label1.BackColor = System.Drawing.Color.Transparent;
        }
    }
}
