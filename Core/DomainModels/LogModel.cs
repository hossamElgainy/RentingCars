using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DomaimModels
{
    [Table("LogModel", Schema = "dbo")]
    public class LogModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Solved { get; set; }
        public string Exception { get; set; }
    }
}
