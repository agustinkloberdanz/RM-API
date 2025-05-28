using RM_API.Models;

namespace RM_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User FindById(long id);
        User FindByEmail(string email);
        void Save(User user);
        void DeleteUser(User user);
    }
}
