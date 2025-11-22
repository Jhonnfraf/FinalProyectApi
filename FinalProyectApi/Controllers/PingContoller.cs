using Microsoft.AspNetCore.Mvc;

namespace FinalProyectApi.Controllers
{
    [ApiController]
    [Route("api/Ping")]
    public class PingContoller: ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}
