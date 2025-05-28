    using System.Data;
using RM_API.Models.DTOs;

namespace RM_API.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public ICollection<Device> Devices { get; set; }

        public User() { }

        public User(UserDTO userDTO)
        {
            Email = userDTO.Email;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
            if (userDTO.Devices != null) Devices = userDTO.Devices.Select(d => new Device(d)).ToList();

        }

        public User(UserRegisterDTO registerDTO)
        {
            Email = registerDTO.Email;
            FirstName = registerDTO.FirstName;
            LastName = registerDTO.LastName;
        }
    }
}
