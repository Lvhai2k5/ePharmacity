using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaThuoc
{
    internal class DataConnection
    {
        public string connection = "Data Source=LEVUHAI;Initial Catalog=NhaThuocDB;User ID=sa;Password=123;Integrated Security=True";



        public DataTable GetData(string s, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.AddRange(parameters);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
