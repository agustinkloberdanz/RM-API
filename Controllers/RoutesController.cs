using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using RM_API.Models;
using RM_API.Models.DTOs;
using RM_API.Services.Interfaces;

namespace RM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRoutesService _routesService;

        public RoutesController(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        [Authorize]
        [HttpPost("addRoute")]
        public ActionResult<AnyType> AddRoute([FromBody] AddRouteDTO addRouteDTO)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.AddRoute(sessionEmail, addRouteDTO);

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
        [HttpPost("updateRoute")]
        public ActionResult<AnyType> UpdateRoute([FromBody] RouteDTO routeDTO)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.UpdateRoute(sessionEmail, routeDTO);

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
        [HttpGet("deleteRoute/{id}")]
        public ActionResult<AnyType> DeleteRoute(long id)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.DeleteRoute(sessionEmail, id);
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
        [HttpGet("getAllRoutes")]
        public ActionResult<AnyType> GetAllRoutes()
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.GetAllRoutes(sessionEmail);
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
        [HttpGet("getRoute/{id}")]
        public ActionResult<AnyType> GetRouteById(long id)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.GetRouteById(sessionEmail, id);
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
        [HttpGet("getRoutesByDrivingDate/{drivingDateString}")]
        public ActionResult<AnyType> GetRoutesByDrivingDate(string drivingDateString)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.GetRoutesByDrivingDate(sessionEmail, drivingDateString);
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
        [HttpPost("location/addLocation")]
        public ActionResult<AnyType> AddLocation([FromBody] AddLocationDTO locationDTO)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.AddLocation(sessionEmail, locationDTO);

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
        [HttpGet("location/deleteLocation/{id}")]
        public ActionResult<AnyType> DeleteLocation(long id)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.DeleteLocation(sessionEmail, id);

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
        [HttpGet("location/deleteAllLocations/{routeId}")]
        public ActionResult<AnyType> DeleteAllLocationsByRouteId(long routeId)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.DeleteAllLocationsByRouteId(sessionEmail, routeId);

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
        [HttpGet("location/notifyLocation/{id}")]
        public ActionResult<AnyType> NotifyLocation(long id)
        {
            string sessionEmail = User.FindFirst("user") != null ? User.FindFirst("user").Value : string.Empty;

            Response response = new Response();
            try
            {
                response = _routesService.IsLocationNotified(sessionEmail, id);

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
