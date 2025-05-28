using RM_API.Models.DTOs;
using RM_API.Models;
using RM_API.Repositories.Interfaces;
using RM_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Net;

namespace RM_API.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptsService _encryptsService;
        private readonly IDeviceRepository _deviceRepository;
        private readonly INotificationsService _notificationsService;

        public UsersService(IUserRepository userRepository, IEncryptsService encryptsService,
            IDeviceRepository deviceRepository, INotificationsService notificationsService
            )
        {
            _userRepository = userRepository;
            _encryptsService = encryptsService;
            _deviceRepository = deviceRepository;
            _notificationsService = notificationsService;
        }

        public Response GetOwn(string sessionEmail)
        {
            User current = _userRepository.FindByEmail(sessionEmail);

            if (current == null)
                return new Response(404, "El usuario no existe en la base de datos", false);

            UserGetOwnDTO userGetOwnDTO = new UserGetOwnDTO(current);

            return new ResponseModel<UserGetOwnDTO>(200, "Ok", userGetOwnDTO);
        }

        public Response GetData(string sessionEmail)
        {
            User current = _userRepository.FindByEmail(sessionEmail);

            if (current == null)
                return new Response(404, "El usuario no existe en la base de datos", false);

            return new Response(200, "Ok");
        }

        public Response Register(UserRegisterDTO registerDTO)
        {
            if (registerDTO == null
                || string.IsNullOrEmpty(registerDTO.Email)
                || string.IsNullOrEmpty(registerDTO.FirstName)
                || string.IsNullOrEmpty(registerDTO.LastName)
                || string.IsNullOrEmpty(registerDTO.Password))
                return new Response(403, "Faltan campos", false);

            if (_userRepository.FindByEmail(registerDTO.Email) != null)
                return new Response(403, "El email ya está registrado en la base de datos", false);

            User newUser = new User(registerDTO);

            _encryptsService.EncryptPassword(registerDTO.Password, out byte[] salt, out byte[] hash);

            newUser.Salt = salt;
            newUser.Hash = hash;

            _userRepository.Save(newUser);

            return new Response(200, "Ok");
        }

        public Response DeleteUser(string sessionEmail, string email)
        {
            User current = _userRepository.FindByEmail(sessionEmail);

            if (email != sessionEmail)
            {
                Response response = new Response(200, "Ok");

                if (response.StatusCode != 200)
                    return response;
            }

            User userToDelete = _userRepository.FindByEmail(email);

            if (userToDelete == null)
                return new Response(404, "El usuario no existe en la base de datos", false);

            _userRepository.DeleteUser(userToDelete);

            return new Response(200, "Ok");
        }

        public Response GetAllUsers(string sessionEmail)
        {
            User current = _userRepository.FindByEmail(sessionEmail);

            Response response = new Response(200, "Ok");

            if (response.StatusCode != 200)
                return response;

            var users = _userRepository.GetAllUsers();

            if (users == null)
            {
                return new Response(404, "No hay usuarios en la base de datos", false);
            }

            var usersDTO = users.Select(u => new UserDTO(u)).ToList();

            return new ResponseCollection<UserDTO>(200, "Ok", usersDTO);
        }

        public Response GetUserByEmail(string sessionEmail, string email)
        {
            User current = _userRepository.FindByEmail(sessionEmail);

            Response response = new Response(200, "Ok");

            if (response.StatusCode != 200)
                return response;

            User userFound = _userRepository.FindByEmail(email);

            if (userFound == null)
                return new Response(404, "El usuario no existe en la base de datos", false);

            UserDTO userFoundDTO = new UserDTO(userFound);

            return new ResponseModel<UserDTO>(200, "Ok", userFoundDTO);
        }

        public Response AddDevice(string sessionEmail, DeviceDTO deviceDTO)
        {
            User current = _userRepository.FindByEmail(sessionEmail);

            if (current == null)
                return new Response(404, "El usuario no existe en la base de datos", false);


            if (string.IsNullOrEmpty(deviceDTO.Token))
                return new Response(403, "Faltan campos", false);

            var devices = _deviceRepository.GetDevicesByUserId(current.Id);

            foreach (var device in devices)
            {
                if (device.Token == deviceDTO.Token)
                    return new Response(200, "Device ya registrado");
            }

            Device deviceToAdd = new Device(deviceDTO, current.Id);

            _deviceRepository.Save(deviceToAdd);

            return new Response(200, "Ok");
        }

        public async Task<Response> NotifyUser(string sessionEmail, UserNotificationDTO userNotificationDTO)
        {
            Response response;

            User current = _userRepository.FindByEmail(sessionEmail);

            response = new Response(200, "Ok");

            if (response.StatusCode != 200)
                return response;

            if (string.IsNullOrEmpty(userNotificationDTO.Message))
                return new Response(403, "Faltan campos", false);

            string body = userNotificationDTO.Message;

            User toNotify = _userRepository.FindByEmail(userNotificationDTO.UserEmail);

            if (toNotify == null)
                return new Response(404, "El usuario no existe en la bdd", false);

            var devices = _deviceRepository.GetDevicesByUserId(toNotify.Id).ToList();

            if (devices == null)
                return new Response(404, "El usuario no tiene devices para notificar", false);

            response = await _notificationsService.NotifyUser(devices, body);

            Console.WriteLine(response.ToString());

            if (response.StatusCode != 200)
                return new Response(response.StatusCode, response.Message, false);

            return new Response(200, "OK");
        }
    }
}
