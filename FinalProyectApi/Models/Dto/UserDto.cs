namespace FinalProyectApi.Models.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public List<CalendarDto> Calendars { get; set; } = new();
    }
}
