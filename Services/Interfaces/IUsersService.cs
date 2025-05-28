using RM_API.Models;
using RM_API.Models.DTOs;

namespace RM_API.Services.Interfaces
{
    public interface IUsersService
    {
        Response GetOwn(string sessionEmail);
        Response GetData(string sessionEmail);
        Response GetAllUsers(string sessionEmail);
        Response GetUserByEmail(string sessionEmail, string email);
        Response Register(UserRegisterDTO registerDTO);
        Response DeleteUser(string sessionEmail, string email);
        Response AddDevice(string sessionEmail, DeviceDTO deviceDTO);
        Task<Response> NotifyUser(string sessionEmail, UserNotificationDTO userNotificationDTO);
    }
}
