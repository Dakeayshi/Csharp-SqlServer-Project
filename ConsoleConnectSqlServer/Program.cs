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
            Console.WriteLine("1. English 2:中文 3: Bahasa Melayu");
            string inputLang = Console.ReadLine();

            if (inputLang == "2")
            {
                InfoHelper.Gender1 = "男";
                InfoHelper.Gender2 = "女";
                InfoHelper.Info1 = "欢迎登录!";
                InfoHelper.Info2 = "请在下方输入您的用户名和密码";
                InfoHelper.Info3 = "请输入用户名: ";
                InfoHelper.Info4 = "请输入密码: ";
                InfoHelper.Info6 = "欢迎, @NickName";
                InfoHelper.Info7 = "请选择性别";
                InfoHelper.Info8 = "选项: 1: 男性 2: 女性";
                InfoHelper.Info9 = "@Username的性别更新失败";
                InfoHelper.Info10 = "@Username的性别更新成功";
                InfoHelper.Info11 = "用户名 昵称 性别";
            }
            else if (inputLang == "3")
            {
                InfoHelper.Gender1 = "Lelaki";
                InfoHelper.Info1 = "Selamat datang log masuk!";
                InfoHelper.Info2 = "Sila masukkan nama pengguna dan kata laluan anda di bawah";
                InfoHelper.Info3 = "Masukkan nama pengguna anda: ";
                InfoHelper.Info4 = "Masukkan kata laluan anda: ";
                InfoHelper.Info5 = "Sama ada nama pengguna atau kata laluan anda salah";
                InfoHelper.Info6 = "Selamat datang, @NickName";
                InfoHelper.Info7 = "Please select your gender";
                InfoHelper.Info8 = "Pilihan: 1: Lelaki 2: Perempuan";
                InfoHelper.Info9 = "Gagal memuatkan jantina ke @Username";
                InfoHelper.Info10 = "Berjaya mengemas kini jantina @Username";
                InfoHelper.Info11 = "Nama Pengguna Nama Panggilan Jantina";
            }
            else
            {
                InfoHelper.Gender1 = "Male";
                InfoHelper.Gender2 = "Female";
                InfoHelper.Info1 = "Welcome login!";
                InfoHelper.Info2 = "Please enter your username and password below";
                InfoHelper.Info3 = "Enter your username: ";
                InfoHelper.Info4 = "Enter your password: ";
                InfoHelper.Info5 = "Either your username or password is incorrect";
                InfoHelper.Info6 = "Welcom, @NickName";
                InfoHelper.Info7 = "Please select your gender";
                InfoHelper.Info8 = "Option: 1: Male 2: Female";
                InfoHelper.Info9 = "Failed to load gender to @Username";
                InfoHelper.Info10 = "Successed to update gender of @Username";
                InfoHelper.Info11 = "Username Nickname Gender";
            }

            //Print system infomation
            Console.WriteLine(InfoHelper.Info1);
            Console.WriteLine(InfoHelper.Info2);

            DataTable table = null;
            while (true)
            {
                Console.Write(InfoHelper.Info3);
                string inputName = Console.ReadLine();

                
                Console.Write(InfoHelper.Info4);
                string inputPwd = Console.ReadLine();

                //Verify user's input
                string sql = $"SELECT * FROM UserT WHERE UserName = '{inputName}' AND Password = '{inputPwd}';";
                table = SelectData(sql);
                if (table.Rows.Count <= 0)
                {
                    Console.WriteLine(InfoHelper.Info5);
                    continue;
                }

                break;
            }

            //Welcoming page
            InfoHelper.Info6 = InfoHelper.Info6.Replace("@NickName",table.Rows[0]["NickName"].ToString());
            Console.WriteLine(InfoHelper.Info6);

            //Fetch gender info, ask untill value is not null or empty
            string gender = table.Rows[0]["Gender"].ToString();
            if (string.IsNullOrEmpty(gender))
            {
                Console.WriteLine(InfoHelper.Info7);

                while (true)
                {
                    Console.WriteLine(InfoHelper.Info8);
                    string inputGender = Console.ReadLine();
                    if (inputGender != "1" && inputGender != "2")
                        continue;

                    //Update data to database
                    string updateGenderSql = $"UPDATE UserT SET Gender='{inputGender}' WHERE UserName = '{table.Rows[0]["UserName"]}';";
                    int i = EditData(updateGenderSql);
                    if (i < 0)
                    {
                        InfoHelper.Info9 = InfoHelper.Info9.Replace("@Username", table.Rows[0]["UserName"].ToString());
                        Console.WriteLine(InfoHelper.Info9);
                        //Console.WriteLine($"Failed to load gender to {table.Rows[0]["UserName"]}");
                    }
                    else
                    {
                        InfoHelper.Info10 = InfoHelper.Info10.Replace("@Username", table.Rows[0]["UserName"].ToString());
                        Console.WriteLine(InfoHelper.Info10);
                        //Console.WriteLine($"Successed to update gender of {table.Rows[0]["UserName"]}");
                    }
                    break;
                }

            }

            //Display user list
            string sqlAllUsers = "SELECT * FROM UserT;";
            DataTable tableAllUsers = SelectData(sqlAllUsers);

            Console.WriteLine(InfoHelper.Info11);
            for (int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                string genderForSql = tableAllUsers.Rows[i]["Gender"].ToString();
                if (genderForSql == "1")
                    genderForSql = InfoHelper.Gender1;
                else if (genderForSql == "2")
                    genderForSql = InfoHelper.Gender2; 

                Console.WriteLine($"{tableAllUsers.Rows[i]["UserName"]}; {tableAllUsers.Rows[i]["NickName"]}; {genderForSql}");
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
