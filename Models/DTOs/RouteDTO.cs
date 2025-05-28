namespace RM_API.Models.DTOs
{
    public class RouteDTO
    {
        public long Id { get; set; }
        public DateTime DrivingDate { get; set; }
        public ICollection<LocationDTO> Locations { get; set; }

        public RouteDTO() { }

        public RouteDTO(Route route)
        {
            Id = route.Id;
            DrivingDate = route.DrivingDate;
            if (route.Locations != null) Locations = route.Locations.Select(l => new LocationDTO(l)).ToList();
        }
    }
}
