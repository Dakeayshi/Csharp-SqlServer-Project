using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer.EF6
{
    public class MyDBContext:DbContext
    {
        public  MyDBContext(): base("Server=localhost; DataBase=TestDB; Trusted_Connection=true;")
        {
            
        }

        public DbSet<UserTModelForEF> UserT { get; set; }

        public DbSet<UserScoresTModelForEF> UserScoresT { get; set; }
    }
}
