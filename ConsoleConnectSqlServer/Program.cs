using ConsoleConnectSqlServer.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

            using (MyDBContext myDB = new MyDBContext())
            {
                DbSet<UserTModelForEF> listQuery = myDB.UserT;

                List<UserTModelForEF> list = listQuery.ToList();

                var query = from usert in myDB.UserT
                            select new UserTModelForEFNew
                            {
                                UserName = usert.UserName,
                                NickName = usert.NickName,
                                Password = usert.Password
                            };


                List<UserTModelForEFNew> lst = query.ToList();
            }

            //Set system language
            InfoHelper.ChangeLanguage();

            //Print system infomation
            Console.WriteLine(InfoHelper.Info1);
            Console.WriteLine(InfoHelper.Info2);

            //UserTModel userTM = new UserTModel();
            UserTModelForEF userTM = new UserTModelForEF();

            while (true)
            {
                Console.Write(InfoHelper.Info3);
                string inputName = Console.ReadLine();

                
                Console.Write(InfoHelper.Info4);
                string inputPwd = Console.ReadLine();

                //userTM = UserTOperation.Login(inputName, inputPwd);
                using (MyDBContext myDB = new MyDBContext())
                {
                    userTM = myDB.UserT.FirstOrDefault(e => e.UserName == inputName && e.Password == inputPwd);
                }


                    //Verify user's input
                    if (userTM == null)
                    {
                        Console.WriteLine(InfoHelper.Info5);
                        continue;
                    }
                break;
            }

            //Welcoming page
            InfoHelper.Info6 = InfoHelper.Info6.Replace("@NickName",userTM.NickName);
            Console.WriteLine(InfoHelper.Info6);

            //Fetch gender info, ask untill value is not null or empty
            if (string.IsNullOrEmpty(userTM.Gender))
            {
                Console.WriteLine(InfoHelper.Info7);

                while (true)
                {
                    Console.WriteLine(InfoHelper.Info8);
                    string inputGender = Console.ReadLine();
                    if (inputGender != "1" && inputGender != "2")
                        continue;

                    using (MyDBContext myDB = new MyDBContext())
                    {
                        myDB.UserT.Attach(userTM);
                        myDB.Entry(userTM).State = System.Data.Entity.EntityState.Modified;

                        userTM.Gender = inputGender;

                        myDB.SaveChanges();
                    }

                        ////Update data to database
                        //string updateGenderSql = $"UPDATE UserT SET Gender='{inputGender}' WHERE UserName = userTM.UserName;";
                        //int i = SqlHelper.EditData(updateGenderSql);
                        //if (i < 0)
                        //{
                        //    InfoHelper.Info9 = InfoHelper.Info9.Replace("@Username", userTM.UserName);
                        //    Console.WriteLine(InfoHelper.Info9);                    
                        //}
                        //else
                        //{
                        //    InfoHelper.Info10 = InfoHelper.Info10.Replace("@Username", userTM.UserName);
                        //    Console.WriteLine(InfoHelper.Info10);                    
                        //}
                        break;
                }

            }

            using (MyDBContext myDB = new MyDBContext())
            {
                List<UserTModelForEF> lstAllUsers = myDB.UserT.ToList();

                for(int i = 0; i < lstAllUsers.Count; i++)
                {
                    string genderForSql = lstAllUsers[i].Gender;
                    if (lstAllUsers[i].Gender == "1")
                        lstAllUsers[i].Gender = InfoHelper.Gender1;
                    else if (lstAllUsers[i].Gender == "2")
                        lstAllUsers[i].Gender = InfoHelper.Gender2;

                    string currentUserName = lstAllUsers[i].UserName;

                    UserScoresTModelForEF currentScore = myDB.UserScoresT.Where(e => e.UserName == currentUserName).OrderByDescending(e => e.RecordTime).First();



                    Console.WriteLine($"{lstAllUsers[i].UserName} {lstAllUsers[i].NickName} {lstAllUsers[i].Gender} {currentScore.Chinese} {currentScore.English} {currentScore.Math} {currentScore.RecordTime}");

                    //List<UserScoresTModelForEF> lstAllUserScores = myDB.UserScoresT.Where(e => e.UserName == currentUserName).ToList();
                    //foreach(UserScoresTModelForEF item in lstAllUserScores)
                    //{
                    //    Console.WriteLine($"{lstAllUsers[i].UserName} {lstAllUsers[i].NickName} {lstAllUsers[i].Gender} {item.Chinese} {item.English} {item.Math} {item.RecordTime}");
                    //}
                }
            }

            using (MyDBContext myDB = new MyDBContext())
            {
                var query = from usert in myDB.UserT
                            join userscorest in myDB.UserScoresT
                            on usert.UserName equals userscorest.UserName 
                            into utus
                            from userscorest in utus.OrderByDescending(e => e.RecordTime).Take(1).DefaultIfEmpty()
                            select new UserTAndUserScoresTModelForEF {
                            UserName = usert.UserName,
                            NickName = usert.NickName,
                            Gender = usert.Gender,
                            Password = usert.Password,
                            Chinese = userscorest.Chinese,
                            English = userscorest.English,
                            Math = userscorest.Math,
                            RecordTime = userscorest.RecordTime
                            };

                List<UserTAndUserScoresTModelForEF> lst = query.ToList();
            }

            //List <UserTAndUserScoresTModel > LstData = UserTAndUserScoresTOperation.GetTop1Data();
            //for(int i = 0; i < LstData.Count; i++)
            //{
            //    string genderForSql = LstData[i].Gender;
            //    if (LstData[i].Gender == "1")
            //        LstData[i].Gender = InfoHelper.Gender1;
            //    else if (LstData[i].Gender == "2")
            //        LstData[i].Gender = InfoHelper.Gender2;

            //    Console.WriteLine($"{LstData[i].UserName} {LstData[i].NickName} {LstData[i].Gender} {LstData[i].Chinese} {LstData[i].English} {LstData[i].Math} {LstData[i].RecordTime}");
            //}


            //List<UserTModel> users = UserTOperation.AllUsers();

            //Console.WriteLine(InfoHelper.Info11);

            //for(int i = 0; i < users.Count; i++)
            //{

            //    //List<UserScoresTModel> lstScores =  UserScoresTOperation.GetUserScoresByUserName(users[i].UserName);
            //    UserScoresTModel model = UserScoresTOperation.GetNewestUserScore(users[i].UserName);

            //    if (users[i].Gender == "1")
            //        users[i].Gender = InfoHelper.Gender1;
            //    else if (users[i].Gender == "2")
            //        users[i].Gender = InfoHelper.Gender2;

                

            //    if(model == null)
            //    {
            //        Console.WriteLine($"{users[i].UserName} {users[i].NickName} {users[i].Gender} 0 0 0");
            //        continue;
            //    }

            //    Console.WriteLine($"{users[i].UserName} {users[i].NickName} {users[i].Gender} {model.Chinese} {model.English} {model.Math} {model.RecordTime}");


            //    //for (int j = 0; j < lstScores.Count; j++)
            //    //{
            //    //    Console.WriteLine($"{users[i].UserName} {users[i].NickName} {users[i].Gender} {lstScores[j].Chinese} {lstScores[j].English} {lstScores[j].Math} {lstScores[j].RecordTime}");
            //    //}

            //}
            Console.ReadKey();
        }

 
    }   
}
