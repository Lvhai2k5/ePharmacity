using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NhaThuoc.GiaoDien
{
    public class DatabaseConnection
    {
        public string ConnectionString = "Data Source=LEVUHAI;Initial Catalog=NhaThuocDB;User ID=sa;Password=123;Integrated Security=True";
        public DataTable GiveData(string s,SqlParameter[] parameter)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(s, connection);
            command.Parameters.AddRange(parameter);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GiveDataNoParameter(string s)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(s, connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
