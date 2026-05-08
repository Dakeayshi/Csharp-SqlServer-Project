using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class UserTOperation
    {
        public static UserTModel Login(string username, string pwd)
        {
            string sql = $"SELECT * FROM UserT WHERE UserName = '{username}' AND Password = '{pwd}';";
            DataTable table = SqlHelper.SelectData(sql);
            if (table.Rows.Count <= 0)
            {
                return null;
            }

            UserTModel model = DataRowToUserTModel(table.Rows[0]);

            return model;
        }

        public static List<UserTModel> AllUsers()
        {
            string sqlAllUsers = "SELECT * FROM UserT;";
            DataTable tableAllUsers = SqlHelper.SelectData(sqlAllUsers);

            //Stroe tables in list
            List<UserTModel> users = new List<UserTModel>();
            for (int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                UserTModel model = UserTOperation.DataRowToUserTModel(tableAllUsers.Rows[i]);
                users.Add(model);
            }
            return users;
        }

        public static UserTModel DataRowToUserTModel(DataRow currentRow)
        {
            UserTModel model = new UserTModel();
            model.UserName = currentRow["UserName"].ToString();
            model.Password = currentRow["Password"].ToString(); 
            model.NickName= currentRow["NickName"].ToString();
            model.Gender = currentRow["Gender"].ToString();

            return model;
        }
    }
}
