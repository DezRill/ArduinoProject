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
    public partial class Diagram : Form
    {
        Login l;
        int total;
        int orvi_count, measles_count, HIV_count, tuberculosis_count, AD_count;
        int other;

        int total_lm;
        int orvi_count_lm, measles_count_lm, HIV_count_lm, tuberculosis_count_lm, AD_count_lm;

        bool control = false;

        private void Diagram_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !control)
            {
                MessageBox.Show("Заборонено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        int other_lm;

        DateTime first;

        public Diagram(Login l)
        {
            InitializeComponent();
            this.l = l;
            first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            BuildDiagram();
            BuildDiagramLastMonth();
        }

        private void GetSickCount()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Total FROM dbo.history";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            total = Convert.ToInt32(table.Rows[0]["Total"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='ГРВІ'";
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            orvi_count = Convert.ToInt32(table.Rows[0]["Count"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='Кір'";
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            measles_count = Convert.ToInt32(table.Rows[0]["Count"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='ВІЧ'";
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            HIV_count = Convert.ToInt32(table.Rows[0]["Count"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='Туберкульоз'";
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            tuberculosis_count = Convert.ToInt32(table.Rows[0]["Count"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='ВСД'";
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            AD_count = Convert.ToInt32(table.Rows[0]["Count"].ToString());
        }

        private void GetSickCountLastMonth()
        {
            SqlCommand command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Total FROM dbo.history WHERE hos_begin>=@some_date";
            command.Parameters.AddWithValue("@some_date", first);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            total_lm = Convert.ToInt32(table.Rows[0]["Total"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='ГРВІ' AND hos_begin>=@some_date";
            command.Parameters.AddWithValue("@some_date", first);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            orvi_count_lm = Convert.ToInt32(table.Rows[0]["Count"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='Кір' AND hos_begin>=@some_date";
            command.Parameters.AddWithValue("@some_date", first);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            measles_count_lm = Convert.ToInt32(table.Rows[0]["Count"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='ВІЧ' AND hos_begin>=@some_date";
            command.Parameters.AddWithValue("@some_date", first);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            HIV_count_lm = Convert.ToInt32(table.Rows[0]["Count"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='Туберкульоз' AND hos_begin>=@some_date";
            command.Parameters.AddWithValue("@some_date", first);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            tuberculosis_count_lm = Convert.ToInt32(table.Rows[0]["Count"].ToString());

            command = l.connection.CreateCommand();
            command.CommandText = "SELECT COUNT(diagnosis) AS Count FROM dbo.history WHERE diagnosis='ВСД' AND hos_begin>=@some_date";
            command.Parameters.AddWithValue("@some_date", first);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            AD_count_lm = Convert.ToInt32(table.Rows[0]["Count"].ToString());
        }

        private void BuildDiagram()
        {
            GetSickCount();
            other = total - (orvi_count + measles_count + HIV_count + tuberculosis_count + AD_count);

            if (orvi_count != 0) chart1.Series["Ills"].Points.AddXY("ГРВІ - " + Math.Round((double.Parse(orvi_count.ToString()) / double.Parse(total.ToString()) * 100), 2) + "%", orvi_count);
            if (measles_count != 0) chart1.Series["Ills"].Points.AddXY("Кір - " + Math.Round((double.Parse(measles_count.ToString()) / double.Parse(total.ToString()) * 100), 2) + "%", measles_count);
            if (HIV_count != 0) chart1.Series["Ills"].Points.AddXY("ВІЧ - " + Math.Round((double.Parse(HIV_count.ToString()) / double.Parse(total.ToString()) * 100), 2) + "%", HIV_count);
            if (tuberculosis_count != 0) chart1.Series["Ills"].Points.AddXY("Туберкульоз - " + Math.Round((double.Parse(tuberculosis_count.ToString()) / double.Parse(total.ToString()) * 100), 2) + "%", tuberculosis_count);
            if (AD_count != 0) chart1.Series["Ills"].Points.AddXY("ВСД - " + Math.Round((double.Parse(AD_count.ToString()) / double.Parse(total.ToString()) * 100), 2) + "%", AD_count);
            if (other != 0) chart1.Series["Ills"].Points.AddXY("Інші - " + Math.Round((double.Parse(other.ToString()) / double.Parse(total.ToString()) * 100), 2) + "%", other);
        }

        private void BuildDiagramLastMonth()
        {
            GetSickCountLastMonth();
            other_lm = total_lm - (orvi_count_lm + measles_count_lm + HIV_count_lm + tuberculosis_count_lm + AD_count_lm);

            if (orvi_count_lm != 0) chart2.Series["Ills"].Points.AddXY("ГРВІ - " + Math.Round((double.Parse(orvi_count_lm.ToString()) / double.Parse(total_lm.ToString()) * 100), 2) + "%", orvi_count_lm);
            if (measles_count_lm != 0) chart2.Series["Ills"].Points.AddXY("Кір - " + Math.Round((double.Parse(measles_count_lm.ToString()) / double.Parse(total_lm.ToString()) * 100), 2) + "%", measles_count_lm);
            if (HIV_count_lm != 0) chart2.Series["Ills"].Points.AddXY("ВІЧ - " + Math.Round((double.Parse(HIV_count_lm.ToString()) / double.Parse(total_lm.ToString()) * 100), 2) + "%", HIV_count_lm);
            if (tuberculosis_count_lm != 0) chart2.Series["Ills"].Points.AddXY("Туберкульоз - " + Math.Round((double.Parse(tuberculosis_count_lm.ToString()) / double.Parse(total_lm.ToString()) * 100), 2) + "%", tuberculosis_count_lm);
            if (AD_count_lm != 0) chart2.Series["Ills"].Points.AddXY("ВСД - " + Math.Round((double.Parse(AD_count_lm.ToString()) / double.Parse(total_lm.ToString()) * 100), 2) + "%", AD_count_lm);
            if (other_lm != 0) chart2.Series["Ills"].Points.AddXY("Інші - " + Math.Round((double.Parse(other_lm.ToString()) / double.Parse(total_lm.ToString()) * 100), 2) + "%", other_lm);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            control = true;
            Form1 frm = new Form1(l);
            frm.Show();
            this.Close();
        }
    }
}
