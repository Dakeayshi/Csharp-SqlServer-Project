using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer.EF6
{
    [Table("UserT")]

    public class UserTModelForEF
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
    }

    public class UserTModelForEFNew
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
    }
}
