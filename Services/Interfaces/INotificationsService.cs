using RM_API.Models;

namespace RM_API.Services.Interfaces
{
    public interface INotificationsService
    {
        Task<Response> NotifyUser(ICollection<Device> devices, string body);
    }
}
