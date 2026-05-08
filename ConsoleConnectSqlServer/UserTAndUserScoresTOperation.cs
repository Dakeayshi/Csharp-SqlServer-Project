using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class UserTAndUserScoresTOperation
    {
        public static List<UserTAndUserScoresTModel> GetAllData()
        {
            string sqlAllUsers = "SELECT U.UserName, U.Password, U.NickName, U.Gender, S.Chinese, S.English, S.Math, S.RecordTime FROM UserT as U LEFT JOIN UserScoresT as S ON U.UserName = S.UserName;";
            DataTable tableAllUsers = SqlHelper.SelectData(sqlAllUsers);

            //Stroe tables in list
            List<UserTAndUserScoresTModel> users = new List<UserTAndUserScoresTModel>();
            for (int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                UserTAndUserScoresTModel model = UserTAndUserScoresTOperation.DataRowToTModel(tableAllUsers.Rows[i]);
                users.Add(model);
            }
            return users;
        }

        public static List<UserTAndUserScoresTModel> GetTop1Data()
        {
            string sqlAllUsers = "SELECT U.UserName, U.Password, U.NickName, U.Gender, S.Chinese, S.English, S.Math, S.RecordTime \r\nFROM UserT as U \r\nLEFT JOIN\r\n(SELECT UserScoresT. * \r\n FROM UserScoresT \r\n INNER JOIN\r\n (SELECT UserName, Max(RecordTime) AS RecordTime \r\n FROM UserScoresT\r\n GROUP BY UserName) as groupt\r\n ON UserScoresT.UserName = groupt.UserName AND UserScoresT.RecordTime = groupt.RecordTime) AS s\r\n ON U.UserName = s.UserName;";
            DataTable tableAllUsers = SqlHelper.SelectData(sqlAllUsers);

            //Stroe tables in list
            List<UserTAndUserScoresTModel> users = new List<UserTAndUserScoresTModel>();
            for (int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                UserTAndUserScoresTModel model = UserTAndUserScoresTOperation.DataRowToTModel(tableAllUsers.Rows[i]);
                users.Add(model);
            }
            return users;
        }

        public static UserTAndUserScoresTModel DataRowToTModel(DataRow currentRow)
        {
            UserTAndUserScoresTModel model = new UserTAndUserScoresTModel();
            model.UserName = currentRow["UserName"].ToString();
            model.Password = currentRow["Password"].ToString();
            model.NickName = currentRow["NickName"].ToString();
            model.Gender = currentRow["Gender"].ToString();
            model.Chinese = string.IsNullOrEmpty(currentRow["Chinese"].ToString()) ? 0 : float.Parse(currentRow["Chinese"].ToString());
            model.English = string.IsNullOrEmpty(currentRow["English"].ToString()) ? 0 : float.Parse(currentRow["English"].ToString());
            model.Math = string.IsNullOrEmpty(currentRow["Math"].ToString()) ? 0 : float.Parse(currentRow["Math"].ToString());

            //model.RecordTime = string.IsNullOrEmpty(currentRow["RecordTime"].ToString()) ? -1 : DateTime.Parse(currentRow["RecordTime"].ToString());


            if (string.IsNullOrEmpty(currentRow["RecordTime"].ToString()))
                model.RecordTime = null;
            else
                model.RecordTime = DateTime.Parse(currentRow["RecordTime"].ToString());

            return model;
        }
    }
}

