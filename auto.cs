using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apotek
{
    class auto
    {
        protected SqlConnection Getconn()
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = "Data Source=LAPTOP-OBQM3SQ3\\MSSQLSERVER2022;Initial Catalog=resto;Integrated Security=True";
            return Conn;
        }

        public DataSet getData(String query)
        {
            SqlConnection con = Getconn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        public void setData(String query, String msg)
        {
            SqlConnection con = Getconn();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(msg, "informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        //berdasarkan module : fungsi

        private SqlConnection connection = new SqlConnection("Data Source=LAPTOP-OBQM3SQ3\\MSSQLSERVER2022;Initial Catalog=resto;Integrated Security=True");

        public SqlConnection GetCon()
        {
            return connection;
        }

        public void OpenCon()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseCon()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // get data date

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source = LAPTOP - OBQM3SQ3\\MSSQLSERVER2022; Initial Catalog = resto; Integrated Security = True";

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
    }
}