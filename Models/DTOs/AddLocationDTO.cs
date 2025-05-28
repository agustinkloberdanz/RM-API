namespace RM_API.Models.DTOs
{
    public class AddLocationDTO
    {
        public string Address { get; set; }
        public int Order { get; set; }
        public string UserEmail { get; set; }
        public bool IsNotified { get; set; }
        public long RouteId { get; set; }

        public AddLocationDTO() { }
        public AddLocationDTO(Location location)
        {
            Address = location.Address;
            Order = location.Order;
            RouteId = location.RouteId;
            UserEmail = location.UserEmail;
            IsNotified = location.IsNotified;
        }
    }
}
