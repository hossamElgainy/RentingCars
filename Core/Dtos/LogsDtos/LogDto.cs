namespace Core.Dtos.Logs
{
    public class LogDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Solved { get; set; }
    }
}
