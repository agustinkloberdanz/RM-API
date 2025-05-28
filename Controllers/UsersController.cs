using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using RM_API.Models.DTOs;
using RM_API.Models;
using RM_API.Services.Interfaces;

namespace RM_API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [Authorize]
        [HttpGet("getOwn")]
        public ActionResult<AnyType> GetOwn()
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();

            try
            {
                response = _usersService.GetOwn(sessionEmail);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;

                return new JsonResult(response);
            }
        }

        [Authorize]
        [HttpGet("getData")]
        public ActionResult<AnyType> GetData()
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();

            try
            {
                response = _usersService.GetData(sessionEmail);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;

                return new JsonResult(response);
            }
        }

        [Authorize]
        [HttpGet("getAll")]
        public ActionResult<AnyType> GetAllUsers()
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _usersService.GetAllUsers(sessionEmail);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;

                return new JsonResult(response);
            }
        }

        [Authorize]
        [HttpGet("getUserByEmail/{email}")]
        public ActionResult<AnyType> GetUserByEmail(string email)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _usersService.GetUserByEmail(sessionEmail, email);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;

                return new JsonResult(response);
            }
        }


        [HttpPost("register")]
        public ActionResult<AnyType> Register([FromBody] UserRegisterDTO registerDTO)
        {
            Response response = new Response();
            try
            {
                response = _usersService.Register(registerDTO);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;

                return new JsonResult(response);
            }
        }

        [Authorize]
        [HttpPost("deleteUser/{email}")]
        public ActionResult<AnyType> DeleteUser(string email)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _usersService.DeleteUser(sessionEmail, email);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;

                return new JsonResult(response);
            }
        }

        [Authorize]
        [HttpPost("device/addDevice")]
        public ActionResult<AnyType> AddDevice([FromBody] DeviceDTO deviceDTO)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _usersService.AddDevice(sessionEmail, deviceDTO);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;

                return new JsonResult(response);
            }
        }

        [Authorize]
        [HttpPost("notifications")]
        public async Task<ActionResult<AnyType>> NotifyDriver([FromBody] UserNotificationDTO userNotificationDTO)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = await _usersService.NotifyUser(sessionEmail, userNotificationDTO);

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

