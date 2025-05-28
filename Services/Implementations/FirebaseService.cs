using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using RM_API.Models;

namespace RM_API.Services.Implementations
{
    public class FirebaseService
    {
        public string title = "Routing Maps";
        public string clickAction = "click_action";

        public FirebaseService()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("serviceAccountKey.json"),
            });
        }

        public async Task<Response> SendNotification(string token, string body)
        {
            try
            {
                var message = new Message()
                {
                    Token = token,
                    Notification = new Notification()
                    {
                        Title = this.title,
                        Body = body,
                    },
                    Data = new Dictionary<string, string>()
                {
                    { "click_action", this.clickAction },
                },
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

                return new ResponseModel<string>(200, "Cliente notificado", token);
            }
            catch (Exception ex)
            {

                return new ResponseModel<string>(500, ex.Message, false, token);
            }
        }
    }
}
