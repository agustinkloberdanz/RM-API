using RM_API.Models.DTOs;
using RM_API.Models;
using RM_API.Repositories.Interfaces;
using System.Globalization;
using RM_API.Services.Interfaces;
using Route = RM_API.Models.Route;

namespace RM_API.Services.Implementations
{
    public class RoutesService : IRoutesService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;

        public RoutesService(ILocationRepository locationRepository, IUserRepository userRepository, IRouteRepository routeRepository)
        {
            _locationRepository = locationRepository;
            _userRepository = userRepository;
            _routeRepository = routeRepository;
        }

        public Response AddLocation(string sessionEmail, AddLocationDTO locationDTO)
        {
            if (string.IsNullOrEmpty(locationDTO.Order.ToString()) || string.IsNullOrEmpty(locationDTO.Address) || string.IsNullOrEmpty(locationDTO.RouteId.ToString()))
                return new Response(404, "Campos incompletos", false);

            if (locationDTO.Order <= 0)
                return new Response(404, "El numero de orden no puede ser menor a 1", false);

            var route = _routeRepository.FindRouteById(locationDTO.RouteId);

            foreach (var location in route.Locations)
            {
                if (locationDTO.Order == location.Order)
                {
                    return new Response(404, "El numero de orden ya esta tomado por otra direccion", false);
                }
            }

            Location newLocation = new Location();

            newLocation.Address = locationDTO.Address;
            newLocation.Order = locationDTO.Order;
            newLocation.RouteId = locationDTO.RouteId;
            newLocation.UserEmail = locationDTO.UserEmail;

            _locationRepository.Save(newLocation);

            return new Response(200, "Ok");
        }

        public Response DeleteLocation(string sessionEmail, long id)
        {
            Location locationToDelete = _locationRepository.GetLocationById(id);

            if (locationToDelete == null)
                return new Response(404, "El lugar no existe en la base de datos", false);

            var locationsToReorder = _locationRepository.GetLocationsAfterOrder(locationToDelete.Order);

            _locationRepository.DeleteLocation(locationToDelete);

            foreach (var location in locationsToReorder)
            {
                location.Order--;
                _locationRepository.Save(location);
            }

            return new Response(200, "Ok");
        }

        public Response DeleteAllLocationsByRouteId(string sessionEmail, long routeId)
        {
            var locationToDelete = _locationRepository.GetAllLocations();

            if (locationToDelete == null)
                return new Response(404, "El lugar no existe en la base de datos", false);

            foreach (Location location in locationToDelete)
            {
                if (location.RouteId == routeId)
                {
                    _locationRepository.DeleteLocation(location);
                }
            }

            return new Response(200, "Ok");
        }

        public Response IsLocationNotified(string sessionEmail, long id)
        {
            Location locationToUpdate = _locationRepository.GetLocationById(id);

            if (locationToUpdate == null)
                return new Response(404, "El lugar buscado no existe en la base de datos", false);

            locationToUpdate.IsNotified = true;

            _locationRepository.Save(locationToUpdate);

            return new Response(200, "Ok");
        }

        public Response AddRoute(string sessionEmail, AddRouteDTO addRouteDTO)
        {
            if (string.IsNullOrEmpty(addRouteDTO.DrivingDate.ToString()) || addRouteDTO.DrivingDate < DateTime.UtcNow)
            {
                return new Response(404, "Fecha incorrecta", false);
            }

            Route newRoute = new Route();

            newRoute.DrivingDate = addRouteDTO.DrivingDate;

            _routeRepository.Save(newRoute);

            return new Response(200, "Ok");
        }

        public Response UpdateRoute(string sessionEmail, RouteDTO routeDTO)
        {
            var routeToUpdate = _routeRepository.FindRouteById(routeDTO.Id);

            if (routeToUpdate == null)
                return new Response(404, "La ruta no existe en la base de datos", false);


            Route newRoute = new Route();
            newRoute.Id = routeDTO.Id;
            newRoute.DrivingDate = routeDTO.DrivingDate;

            var addLocationDTOs = new List<AddLocationDTO>();
            if (routeDTO.Locations != null)
            {
                foreach (var locationDTO in routeDTO.Locations)
                {
                    addLocationDTOs.Add(new AddLocationDTO
                    {
                        Address = locationDTO.Address,
                        Order = locationDTO.Order,
                        UserEmail = locationDTO.UserEmail,
                        IsNotified = locationDTO.isNotified,
                        RouteId = newRoute.Id
                    });
                }
            }

            _locationRepository.DeleteAllLocationsByRouteId(routeDTO.Id);

            newRoute.Locations = addLocationDTOs.Select(l => new Location(l)).ToList();

            _routeRepository.Save(newRoute);

            return new Response(200, "Ok");
        }

        public Response DeleteRoute(string sessionEmail, long id)
        {
            Route routeToDelete = _routeRepository.FindRouteById(id);

            if (routeToDelete == null)
                return new Response(404, "La ruta no existe en la base de datos", false);

            _routeRepository.DeleteRoute(routeToDelete);

            return new Response(200, "Ok");
        }

        public Response GetRouteById(string sessionEmail, long id)
        {
            var route = _routeRepository.FindRouteById(id);

            if (route == null)
                return new Response(404, "No existe en la base de datos", false);

            RouteDTO routeDTO = new RouteDTO(route);

            return new ResponseModel<RouteDTO>(200, "Ok", routeDTO);
        }

        public Response GetAllRoutes(string sessionEmail)
        {
            var routes = _routeRepository.GetAllRoutes();

            var routesDTO = routes.Select(u => new RouteDTO(u)).ToList();

            return new ResponseCollection<RouteDTO>(200, "Ok", routesDTO);
        }

        public Response GetRoutesByDrivingDate(string sessionEmail, string drivingDateString)
        {
            if (!DateTime.TryParseExact(drivingDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime drivingDate))
            {
                return new Response(400, "Formato de fecha incorrecto. Utiliza el formato yyyy-MM-dd.", false);
            }

            string formattedDrivingDate = drivingDate.ToString("yyyy-MM-dd");

            var routes = _routeRepository.FindRouteByDrivingDate(drivingDate);

            if (routes == null || !routes.Any())
                return new Response(404, "No se encontraron rutas para la fecha especificada", false);

            var routesDTO = routes.Select(u => new RouteDTO(u)).ToList();

            return new ResponseCollection<RouteDTO>(200, "Ok", routesDTO);
        }
    }
}
