namespace RM_API.Services.Interfaces
{
    public interface IEncryptsService
    {
        void EncryptPassword(string password, out byte[] salt, out byte[] hash);
        bool VerifyPassword(string password, byte[] salt, byte[] hash);
    }
}
