using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using RM_API.Models;
using RM_API.Models.DTOs;
using RM_API.Services.Interfaces;

namespace RM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult<AnyType> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            Response response = new Response();
            try
            {
                response = _authService.Login(userLoginDTO);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;

                return new JsonResult(response);
            }
        }
    }
}
