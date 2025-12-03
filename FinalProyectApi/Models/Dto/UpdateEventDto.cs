namespace FinalProyectApi.Models.Dto
{
    public class UpdateEventDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }

    }
}
