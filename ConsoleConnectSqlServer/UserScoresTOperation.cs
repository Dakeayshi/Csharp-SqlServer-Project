using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class UserScoresTOperation
    {
        public static List<UserScoresTModel> GetUserScoresByUserName(string username)
        {
            string sqlUserScores = $"SELECT * FROM UserScoresT WHERE UserName = '{username}';";
            DataTable tableUserScores = SqlHelper.SelectData(sqlUserScores);

            //Stroe tables in list
            List<UserScoresTModel> users = new List<UserScoresTModel>();
            for (int i = 0; i < tableUserScores.Rows.Count; i++)
            {
                UserScoresTModel model = UserScoresTOperation.DataRowToUserScoresTModel(tableUserScores.Rows[i]);
                users.Add(model);
            }
            return users;
        }

        public static UserScoresTModel GetNewestUserScore(string username)
        {
            string sqlUserScores = $"SELECT TOP 1 * FROM UserScoresT WHERE UserName = '{username}' ORDER BY RecordTime DESC;";
            DataTable table = SqlHelper.SelectData(sqlUserScores);
            if (table.Rows.Count <= 0)
                return null;

            UserScoresTModel model = UserScoresTOperation.DataRowToUserScoresTModel(table.Rows[0]);
            return model;
        }



        public static UserScoresTModel DataRowToUserScoresTModel(DataRow currentRow)
        {
            UserScoresTModel model = new UserScoresTModel();
            model.UserName = currentRow["UserName"].ToString();
            model.Chinese = float.Parse(currentRow["Chinese"].ToString());
            model.English = float.Parse(currentRow["English"].ToString());
            model.Math = float.Parse(currentRow["Math"].ToString());
            model.RecordTime= DateTime.Parse(currentRow["RecordTime"].ToString());

            return model;
        }
    }
}
