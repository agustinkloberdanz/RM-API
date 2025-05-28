using RM_API.Models.DTOs;

namespace RM_API.Models
{
    public class Route
    {
        public long Id { get; set; }
        public DateTime DrivingDate { get; set; }
        public ICollection<Location> Locations { get; set; }

        public Route() { }

        public Route(RouteDTO routeDTO)
        {
            Id = routeDTO.Id;
            DrivingDate = routeDTO.DrivingDate;
            if (routeDTO.Locations != null) Locations = routeDTO.Locations.Select(l => new Location(l)).ToList();
        }
    }
}
