namespace RM_API.Models.DTOs
{
    public class LocationDTO
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public int Order { get; set; }
        public string UserEmail { get; set; }
        public bool isNotified { get; set; }

        public LocationDTO() { }
        public LocationDTO(Location location)
        {
            Id = location.Id;
            Address = location.Address;
            Order = location.Order;
            UserEmail = location.UserEmail;
            isNotified = location.IsNotified;
        }

    }
}
