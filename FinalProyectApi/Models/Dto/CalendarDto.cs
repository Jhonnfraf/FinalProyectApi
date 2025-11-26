namespace FinalProyectApi.Models.Dto
{
    public class CalendarDto
    {
        public int CalendarId { get; set; }
        public string CalendarName { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }
}
