using System.Data;

namespace RM_API.Models.DTOs
{
    public class UserGetOwnDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserGetOwnDTO(User user)
        {
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}
