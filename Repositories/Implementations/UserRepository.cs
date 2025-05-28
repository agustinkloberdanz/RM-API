using Microsoft.EntityFrameworkCore;
using RM_API.Models;
using RM_API.Repositories.Interfaces;

namespace RM_API.Repositories.Implementations
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RM_APIContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .Include(u => u.Devices)
                .ToList();
        }

        public User FindById(long id)
        {
            return FindByCondition(u => u.Id == id)
                .Include(u => u.Devices)
                .FirstOrDefault();
        }

        public User FindByEmail(string email)
        {
            return FindByCondition(u => u.Email == email)
                .Include(u => u.Devices)
                .FirstOrDefault();
        }

        public void Save(User user)
        {
            if (user.Id == 0) Create(user);
            else Update(user);

            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }
        public void DeleteUser(User user)
        {
            Delete(user);
            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }
    }
}
