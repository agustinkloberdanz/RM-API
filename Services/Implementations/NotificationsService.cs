using RM_API.Models;
using RM_API.Services.Interfaces;

namespace RM_API.Services.Implementations
{
    public class NotificationsService : INotificationsService
    {
        private readonly FirebaseService _firebaseService;

        public NotificationsService(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public async Task<Response> NotifyUser(ICollection<Device> devices, string body)
        {
            ResponseCollection<Response> response = new ResponseCollection<Response>();
            bool isNotified = false;

            foreach (var device in devices)
            {
                Response deviceResponse = new Response();
                deviceResponse = await _firebaseService.SendNotification(device.Token, body);

                response.Collection.Add(deviceResponse);

                if (deviceResponse.IsSuccess && !isNotified)
                {
                    response.StatusCode = 200;
                    response.Message = deviceResponse.Message;

                    isNotified = true;
                }
            }

            if (!isNotified)
            {
                response.StatusCode = 400;
                response.Message = "El cliente no ha podido ser notificado";
            }

            return response;
        }
    }
}
