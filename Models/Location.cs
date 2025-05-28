using RM_API.Models.DTOs;

namespace RM_API.Models
{
    public class Location
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public int Order { get; set; }
        public string UserEmail { get; set; }
        public bool IsNotified { get; set; }
        public Route Route { get; set; }
        public long RouteId { get; set; }


        public Location() { }
        public Location(AddLocationDTO AddLocationDTO)
        {
            Address = AddLocationDTO.Address;
            Order = AddLocationDTO.Order;
            UserEmail = AddLocationDTO.UserEmail;
            IsNotified = AddLocationDTO.IsNotified;
            RouteId = AddLocationDTO.RouteId;
        }

        public Location(LocationDTO LocationDTO)
        {
            Id = LocationDTO.Id;
            Address = LocationDTO.Address;
            Order = LocationDTO.Order;
            UserEmail = LocationDTO.UserEmail;
            IsNotified = LocationDTO.isNotified;
        }
    }
}
