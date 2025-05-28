using RM_API.Models.DTOs;

namespace RM_API.Models
{
    public class Device
    {
        public long Id { get; set; }
        public string DeviceKey { get; set; }
        public string DeviceId { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }

        public Device() { }

        public Device(DeviceDTO deviceDTO)
        {
            DeviceKey = deviceDTO.DeviceKey;
            DeviceId = deviceDTO.DeviceId;
            Token = deviceDTO.Token;
        }

        public Device(DeviceDTO deviceDTO, long userId)
        {
            DeviceKey = deviceDTO.DeviceKey;
            DeviceId = deviceDTO.DeviceId;
            Token = deviceDTO.Token;
            UserId = userId;
        }
    }
}
