using FinalProyectApi.Models;
using FinalProyectApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProyectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarsController : ControllerBase
    {
        private readonly CalendarioDbContext _context; 

        public CalendarsController(CalendarioDbContext context)
        {
            _context = context;
        }

        //Get: api/Calendars/user/{userId}

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCalendarsByUserId(int userId)
        {
            var calendars = await _context.Calendars
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return Ok(calendars);
        }

        //Post: api/Calendars
        public class CreateCAlendarDto
        {
            public int UserId { get; set; }
            public string CalendarName { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCalendar([FromBody] CreateCAlendarDto dto)
        {
            if (!_context.Users.Any(u => u.UserId == dto.UserId))
            {
                return BadRequest(new { message = "El usuario no existe (Foreign Key inválida)" });
            }

            var calendar = new Calendar
            {
                UserId = dto.UserId,
                CalendarName = dto.CalendarName,
                CreatedAt = DateTime.UtcNow
            };

            _context.Calendars.Add(calendar);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Error al guardar el calendario", error = ex.Message });
            }

            return Ok(calendar);
        }

    }
}