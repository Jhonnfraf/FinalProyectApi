using Microsoft.AspNetCore.Mvc;
using FinalProyectApi.Models;
using FinalProyectApi.Models.Dto;
using FinalProyectApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinalProyectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly CalendarioDbContext _db;
        private readonly PasswordService _passwordService;

        public UsersController(CalendarioDbContext db, PasswordService passwordService)
        {
            _db = db;
            _passwordService = passwordService;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto dto)
        {
            Console.WriteLine("📌 LLEGÓ DTO:");
            Console.WriteLine($"Username: {dto.Username}");
            Console.WriteLine($"Email: {dto.Email}");
            Console.WriteLine($"Password: {dto.Password}");

            //validacion inicial
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Usuario y contraseña son obligatorios");

            //Verificar si ya existe
            if (_db.Users.Any(u => u.Username == dto.Username))
                return Conflict("El nombre de usuario ya existe.");

            if (_db.Users.Any(u => u.Email == dto.Email))
                return Conflict("El correo electronico ya esta registrado.");

            //generamos salt
            var salt = _passwordService.GenerateSalt();
            var hash = _passwordService.HashPassword(dto.Password, salt);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok(new
            {
                message = "Usuario registrado exitosamente",
                userId = user.UserId
            });

        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            // 1. Validación básica
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Usuario y contraseña son obligatorios");

            // 2. Buscar usuario
            var user = _db.Users.FirstOrDefault(u => u.Username == dto.Username);
            if (user == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            // 3. Verificar contraseña (hash + salt)
            var hashedPassword = _passwordService.HashPassword(dto.Password, user.PasswordSalt);
            if (hashedPassword != user.PasswordHash)
                return Unauthorized("Usuario o contraseña incorrectos");

            // 4. Login exitoso
            return Ok(new
            {
                message = "Login exitoso",
                userId = user.UserId,
                username = user.Username
            });
        }
    }
}
