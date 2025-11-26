namespace FinalProyectApi.Models.Dto
{
    public class CreateCalendarDto
    {
        public int UserId { get; set; }
        public string CalendarName { get; set; } = string.Empty;
    }
}
