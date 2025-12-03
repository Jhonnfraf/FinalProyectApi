using FinalProyectApi.Models;
using FinalProyectApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProyectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly CalendarioDbContext _context;

        public EventsController(CalendarioDbContext context)
        {
            _context = context;
        }

        // POST api/events
        [HttpPost]
        public async Task<ActionResult<EventResponseDto>> CreateEvent(CreateEventDto dto)
        {
            var calendar = await _context.Calendars.FindAsync(dto.CalendarId);
            if (calendar == null)
                return NotFound("Calendar not found");

            var newEvent = new Event
            {
                CalendarId = dto.CalendarId,
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreatedAt = DateTime.UtcNow
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            var response = new EventResponseDto
            {
                EventId = newEvent.EventId,
                CalendarId = newEvent.CalendarId,
                Title = newEvent.Title,
                Description = newEvent.Description ?? "",
                StartDate = newEvent.StartDate,
                EndDate = newEvent.EndDate ?? DateTime.UtcNow,
            };

            return Ok(response);
        }



        // GET api/events/calendar/5
        [HttpGet("calendar/{calendarId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents(int calendarId)
        {
            // Asegura que la tabla Events no sea null
            if (_context.Events == null)
                return Problem("La tabla Events no está configurada en el DbContext.");

            // Consulta segura
            var events = await _context.Events
                .Where(e => e.CalendarId == calendarId)
                .OrderBy(e => e.StartDate)
                .Select(e => new Event
                {
                    EventId = e.EventId,
                    CalendarId = e.CalendarId,
                    Title = e.Title ?? "",
                    Description = e.Description ?? "",
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    CreatedAt = e.CreatedAt ?? DateTime.UtcNow,
                    Calendar = null! // no se toca, EF decide
                })
                .ToListAsync();

            return Ok(events);
        }

        [HttpDelete("calendar/{calendarId}/{eventId}")]
        public async Task<IActionResult> DeleteEvent(int calendarId, int eventId)
        {
            var ev = await _context.Events
                .FirstOrDefaultAsync(e => e.CalendarId == calendarId && e.EventId == eventId);

            if (ev == null)
                return NotFound("El evento no existe.");

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("calendar/{calendarId}/{eventId}")]
        public async Task<IActionResult> UpdateEvent(int calendarId, int eventId, [FromBody] UpdateEventDto dto)
        {
            var ev = await _context.Events
                .FirstOrDefaultAsync(e => e.CalendarId == calendarId && e.EventId == eventId);

            if (ev == null)
                return NotFound("El evento no existe o es de un calendario incorrecto.");
            //Actualizar los datos
            ev.Title = dto.Title;
            ev.Description = dto.Description;
            ev.StartDate = dto.StartDate;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Evento actualizado correctamente." });
        
        }
    }
}
