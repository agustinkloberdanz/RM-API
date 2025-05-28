namespace RM_API.Models.DTOs
{
    public class DeviceDTO
    {
        public string DeviceKey { get; set; }
        public string DeviceId { get; set; }
        public string Token { get; set; }

        public DeviceDTO() { }
        public DeviceDTO(Device device)
        {
            DeviceKey = device.DeviceKey;
            DeviceId = device.DeviceId;
            Token = device.Token;
        }
    }
}
