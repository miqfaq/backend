using System.Security.Cryptography;
using System.Text;

namespace WebAPIBackend.Utils
{
    public static class AuthUtils
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashbytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashbytes);
            }
        }

        public static bool VerifyPassword(string password, string hashedpassword)
        { 
            var hashedpasswordbytes = Convert.FromBase64String(hashedpassword);
            using (var sha256 = SHA256.Create())
            {
                var computedhashbytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return hashedpasswordbytes.SequenceEqual(computedhashbytes);
            }
        }
    }
}