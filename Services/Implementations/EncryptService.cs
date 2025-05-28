using RM_API.Services.Interfaces;
using System.Security.Cryptography;

namespace RM_API.Services.Implementations
{
    public class EncryptsService : IEncryptsService
    {
        public void EncryptPassword(string password, out byte[] salt, out byte[] hash)
        {
            using (var encrypter = new HMACSHA512())
            {
                salt = encrypter.Key;
                hash = encrypter.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPassword(string password, byte[] salt, byte[] hash)
        {
            using (var encrypter = new HMACSHA512(salt))
            {
                var compareHash = encrypter.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return compareHash.SequenceEqual(hash);
            }
        }
    }
}
