namespace Core.Dtos.Logs
{
    public class LogFilterDto
    {
        public string? Level { get; set; }
        public bool? Solved { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? SearchTerm { get; set; }
    }
}
