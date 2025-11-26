namespace FinalProyectApi.Models.Dto
{
    public class EventResponseDto
    {
        public int EventId { get; set; }
        public int CalendarId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
