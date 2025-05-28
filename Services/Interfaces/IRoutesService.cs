using RM_API.Models;
using RM_API.Models.DTOs;

namespace RM_API.Services.Interfaces
{
    public interface IRoutesService
    {
        Response AddLocation(string sessionEmail, AddLocationDTO locationDTO);
        Response DeleteLocation(string sessionEmail, long id);
        Response DeleteAllLocationsByRouteId(string sessionEmail, long routeId);
        Response IsLocationNotified(string sessionEmail, long id);
        Response AddRoute(string sessionEmail, AddRouteDTO addRouteDTO);
        Response UpdateRoute(string sessionEmail, RouteDTO routeDTO);
        Response DeleteRoute(string sessionEmail, long id);
        Response GetAllRoutes(string sessionEmail);
        Response GetRouteById(string sessionEmail, long id);
        Response GetRoutesByDrivingDate(string sessionEmail, string drivingDateString);
    }
}
