using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Project
{
    internal class machine
    {
        public string blankImage = @"C:\Users\HP\source\repos\Restaurant-Project\assets\img\blank-image.jpeg";
        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=LAPTOP-OBQM3SQ3\\MSSQLSERVER2022;Initial Catalog=resto;Integrated Security=True";

            return connection;
        }

        public DataSet GetData(String query)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataSet data = new DataSet();
            sqlDataAdapter.Fill(data);

            return data;
        }

        public DataTable GetOneData(String query)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);

            return data;
        }

        public void SetData(String query, String message)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("" + message + "", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SetDataWithOutMessageBox(String query)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public DataTable Login(String username, String password)
        {
            SqlConnection connection = GetConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select kdPegawai, nama, lvl from pegawai where username='" + username + "' and pass='" + password + "'", connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            return dt;
        }

        public void LogActivity(String logActivity)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into logact(pegawaiKd, aktifitas) values('" + FormLogin.kode_pegawai + "', '" + logActivity + "')";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void FillAllFields()
        {
            MessageBox.Show("Fill All Fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void SelectDataToEdit()
        {
            MessageBox.Show("Choose data to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void SelectDataToDelete()
        {
            MessageBox.Show("Choose data to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void ValidateCash()
        {
            MessageBox.Show("The money cannot be less than the total bill.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public String GenerateCode(String column, String table, String serial, int deleteIndexSerial)
        {
            DataTable countDB = GetOneData("select count(" + column + ") from " + table + "");
            int count = int.Parse(countDB.Rows[0][0].ToString());
            int initialValue = 000;

            if (countDB.Rows.Count != 0)
            {
                int lastValueCount = initialValue + count + 1;
                String codeValue = serial + "-" + lastValueCount.ToString("D5");
                return codeValue.Remove(deleteIndexSerial, 1);
            }
            else
            {
                int lastValueCount = initialValue + 1;
                String codeValue = serial + "-" + lastValueCount.ToString("D5");
                return codeValue.Remove(deleteIndexSerial, 1);
            }
        }

        public int GetDataCount(String query)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count;
        }
    }
}

