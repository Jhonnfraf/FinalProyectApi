using System.Security.Cryptography;
using System.Text;
using FinalProyectApi.Models;

namespace FinalProyectApi.Services
{
    public class PasswordService
    {
        public string GenerateSalt()
        {
            var salt = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
            
        }

        public string HashPassword(string password, string saltBase64)
        {
            var salt = Convert.FromBase64String(saltBase64);

            using var sha256 = SHA256.Create();

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var combined = passwordBytes.Concat(salt).ToArray();

            var hash = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hash);
        }
    }
}
