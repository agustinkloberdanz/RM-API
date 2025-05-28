using RM_API.Models;

namespace RM_API.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        IEnumerable<Device> GetDevicesByUserId(long userId);
        Device GetDeviceById(long id);
        void Save(Device device);
        void DeleteDevice(Device device);
    }
}
