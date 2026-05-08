using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class SqlHelper
    {
        public static int EditData(string sql)
        {

            int count = -1;

            try
            {
                // Connect to SQL
                SqlConnection conn = new SqlConnection();
                // server, data file, connect method
                conn.ConnectionString = "Server=localhost; DataBase=TestDB; Trusted_Connection=true";
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EditData error: " + ex.Message);
            }

            return count;
        }

        public static DataTable SelectData(string sql)
        {
            DataTable table = null;

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=localhost; DataBase=TestDB; Trusted_Connection=true";
                conn.Open(); // Now inside try/catch — exceptions will print

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                conn.Close();
                table = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine("SelectData error: " + ex.Message);
            }

            return table;
        }
    }
}
