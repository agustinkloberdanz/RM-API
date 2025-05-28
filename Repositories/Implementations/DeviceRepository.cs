using RM_API.Models;
using RM_API.Repositories.Interfaces;

namespace RM_API.Repositories.Implementations
{
    public class DeviceRepository : RepositoryBase<Device>, IDeviceRepository
    {
        public DeviceRepository(RM_APIContext repositoryContext) : base(repositoryContext) { }

        public void DeleteDevice(Device device)
        {
            Delete(device);
            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }

        public Device GetDeviceById(long id)
        {
            return FindByCondition(d => d.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Device> GetDevicesByUserId(long userId)
        {
            return FindAll()
                .Where(d => d.UserId == userId)
                .ToList();
        }

        public void Save(Device device)
        {
            if (device.Id == 0) Create(device);
            else Update(device);

            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }
    }
}
