using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    internal class InfoHelper
    {
        public static string Gender1 { get; set; }
        public static string Gender2 { get; set; }
        public static string Info1 { get; set; }
        public static string Info2 { get; set; }
        public static string Info3 { get; set; }
        public static string Info4 { get; set; }
        public static string Info5 { get; set; }
        public static string Info6 { get; set; }
        public static string Info7 { get; set; }
        public static string Info8 { get; set; }
        public static string Info9 { get; set; }
        public static string Info10 { get; set; }
        public static string Info11 { get; set; }
        public static string Info12 { get; set; }

        public static void ChangeLanguage()
        {
            //Choose display language
            Console.WriteLine("1. English 2:中文 3: Bahasa Melayu");
            string inputLang = Console.ReadLine();

            if (inputLang == "2")
            {
                Gender1 = "男";
                Gender2 = "女";
                Info1 = "欢迎登录!";
                Info2 = "请在下方输入您的用户名和密码";
                Info3 = "请输入用户名: ";
                Info4 = "请输入密码: ";
                Info6 = "欢迎, @NickName";
                Info7 = "请选择性别";
                Info8 = "选项: 1: 男性 2: 女性";
                Info9 = "@Username的性别更新失败";
                Info10 = "@Username的性别更新成功";
                Info11 = "用户名 昵称 性别 中文成绩 英语成绩 数学成绩";
            }
            else if (inputLang == "3")
            {
                Gender1 = "Lelaki";
                Gender2 = "Perempuan";
                Info1 = "Selamat datang log masuk!";
                Info2 = "Sila masukkan nama pengguna dan kata laluan anda di bawah";
                Info3 = "Masukkan nama pengguna anda: ";
                Info4 = "Masukkan kata laluan anda: ";
                Info5 = "Sama ada nama pengguna atau kata laluan anda salah";
                Info6 = "Selamat datang, @NickName";
                Info7 = "Please select your gender";
                Info8 = "Pilihan: 1: Lelaki 2: Perempuan";
                Info9 = "Gagal memuatkan jantina ke @Username";
                Info10 = "Berjaya mengemas kini jantina @Username";
                Info11 = "Nama Pengguna Nama Panggilan Jantina Gred Cina Gred Bahasa Inggeris Gred Matematik";
            }
            else
            {
                Gender1 = "Male";
                Gender2 = "Female";
                Info1 = "Welcome login!";
                Info2 = "Please enter your username and password below";
                Info3 = "Enter your username: ";
                Info4 = "Enter your password: ";
                Info5 = "Either your username or password is incorrect";
                Info6 = "Welcom, @NickName";
                Info7 = "Please select your gender";
                Info8 = "Option: 1: Male 2: Female";
                Info9 = "Failed to load gender to @Username";
                Info10 = "Successed to update gender of @Username";
                Info11 = "Username Nickname Gender Chinese Grade English Grade Math Grade";
            }
        }
    }
}
