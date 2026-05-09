using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer.EF6
{
    [Table("UserScoresT")]
    public class UserScoresTModelForEF
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public double Chinese { get; set; }
        public double English { get; set; }
        public double Math { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
