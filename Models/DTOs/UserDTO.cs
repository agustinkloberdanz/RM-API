using System.Data;

namespace RM_API.Models.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<DeviceDTO> Devices { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            if (user.Devices != null) Devices = user.Devices.Select(d => new DeviceDTO(d)).ToList();
        }
    }
}
