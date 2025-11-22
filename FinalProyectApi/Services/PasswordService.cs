using System.Security.Cryptography;
using System.Text;
using FinalProyectApi.Models;

namespace FinalProyectApi.Services
{
    public class PasswordService
    {
        public byte[] GenerateSalt()
        {
            var salt = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
            
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            using var sha256 = SHA256.Create();
            
            //convertir el password a bytes
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            //Combinar password y salt
            var passwordWithSalt = passwordBytes.Concat(salt).ToArray();

            return sha256.ComputeHash(passwordWithSalt);
        }
    }
}
