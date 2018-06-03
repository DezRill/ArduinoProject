using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UiDesignDemo;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        SqlConnection connection;

        void Connect()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "arduino.zapto.org";
            builder.InitialCatalog = "Hospital";
            builder.UserID = "admin";
            builder.Password = "admin";
            connection = new SqlConnection(builder.ToString());
            connection.Open();
        }

        [TestMethod]
        public void TestConnection()
        {
            Connect();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.doctors";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            Assert.IsNotNull(table);
        }

        [TestMethod]
        public void TestConvertBinaryToImage()
        {
            Connect();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.doctors";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            Assert.IsNotNull(Login.ConvertBinaryToImage((byte[])table.Rows[0]["photo"]));
        }

        [TestMethod]
        public void TestConvertImageToBinary()
        {
            Connect();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.doctors";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            Image img = Image.FromFile(@"D:\Programs\C#\ArduinoProject\UiDesignDemo\Resources\2_43.png");
            Assert.IsNotNull(Form3_1.ConvertImageToBinary(img));
        }
    }
}
