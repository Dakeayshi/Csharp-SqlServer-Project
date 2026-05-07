using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Choose display language
            Console.WriteLine("1. English 2:中文");
            string inputLang = Console.ReadLine();
            int lang = 1;
            if (inputLang == "2")
                lang = 2;


            //Print system infomation
            Console.WriteLine("Welcom to XXX system!");
            Console.WriteLine("Please enter your username and password below");

            DataTable table = null;
            while (true)
            {
                Console.Write("Enter your username: ");
                string inputName = Console.ReadLine();

                Console.Write("Enter your password: ");
                string inputPwd = Console.ReadLine();

                //Verify user's input
                string sql = $"SELECT * FROM UserT WHERE UserName = '{inputName}' AND Password = '{inputPwd}';";
                table = SelectData(sql);
                if (table.Rows.Count <= 0)
                {
                    Console.WriteLine("Either your username or password is incorrect");
                    continue;
                }

                break;
            }

            //Welcoming page
            Console.WriteLine($"Welcome, {table.Rows[0]["NickName"]}");

            //Fetch gender info, ask untill value is not null or empty
            string gender = table.Rows[0]["Gender"].ToString();
            if (string.IsNullOrEmpty(gender))
            {
                Console.WriteLine("Please provide your gender information");

                while (true)
                {
                    Console.WriteLine("Option: 1: Male 2: Female");
                    string inputGender = Console.ReadLine();
                    if (inputGender != "1" && inputGender != "2")
                        continue;

                    //Update data to database
                    string updateGenderSql = $"UPDATE UserT SET Gender='{inputGender}' WHERE UserName = '{table.Rows[0]["UserName"]}';";
                    int i = EditData(updateGenderSql);
                    if (i < 0)
                    {
                        Console.WriteLine($"Failed to load gender to {table.Rows[0]["UserName"]}");
                    }
                    else
                    {
                        Console.WriteLine($"Successed to update gender of {table.Rows[0]["UserName"]}");
                    }
                    break;
                }

            }

            //Display user list
            string sqlAllUsers = "SELECT * FROM UserT;";
            DataTable tableAllUsers = SelectData(sqlAllUsers);

            Console.WriteLine("Username Nickname Gender");
            for (int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                string genderForSql = tableAllUsers.Rows[i]["Gender"].ToString();
                if(lang==1)
                {
                    if (genderForSql == "1")
                        genderForSql = "Male";
                    else if (genderForSql == "2")
                        genderForSql = "Female";
                }
                if(lang==2)
                {
                    if (genderForSql == "Male")
                        genderForSql = "男";
                    else if (genderForSql == "Female") 
                        genderForSql = "女";
                }

                Console.WriteLine($"Username: {tableAllUsers.Rows[i]["UserName"]}; {tableAllUsers.Rows[i]["NickName"]}; {genderForSql}");
            }

            Console.ReadKey();
        }

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
            catch(Exception ex)
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
