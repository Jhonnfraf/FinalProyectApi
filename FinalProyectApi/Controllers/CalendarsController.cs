using Microsoft.AspNetCore.Mvc;
using FinalProyectApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProyectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarsController : ControllerBase
    {
        private readonly CalendarioDbContext _context; // Reemplaza con el nombre de tu DbContext

        public CalendarsController(CalendarioDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los calendarios de un usuario específico
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetCalendarsByUserId(int userId)
        {
            try
            {
                var calendars = await _context.Calendars
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                if (calendars == null || calendars.Count == 0)
                {
                    return Ok(new List<Calendar>()); // Retorna lista vacía si no hay calendarios
                }

                return Ok(calendars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener calendarios", error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene un calendario específico por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar>> GetCalendar(int id)
        {
            var calendar = await _context.Calendars
                .FirstOrDefaultAsync(c => c.CalendarId == id);

            if (calendar == null)
            {
                return NotFound();
            }

            return Ok(calendar);
        }

        /// <summary>
        /// Crea un nuevo calendario
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Calendar>> CreateCalendar([FromBody] Calendar calendar)
        {
            if (calendar == null)
            {
                return BadRequest();
            }

            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCalendar), new { id = calendar.CalendarId }, calendar);
        }

        /// <summary>
        /// Actualiza un calendario existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCalendar(int id, [FromBody] Calendar calendar)
        {
            if (id != calendar.CalendarId)
            {
                return BadRequest();
            }

            _context.Entry(calendar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Elimina un calendario
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }

            _context.Calendars.Remove(calendar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalendarExists(int id)
        {
            return _context.Calendars.Any(e => e.CalendarId == id);
        }
    }
}