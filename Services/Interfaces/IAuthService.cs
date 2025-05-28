using RM_API.Models.DTOs;
using RM_API.Models;

namespace RM_API.Services.Interfaces
{
    public interface IAuthService
    {
        Response Login(UserLoginDTO userLoginDTO);
        string MakeToken(User user);
    }
}

