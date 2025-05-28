using Microsoft.IdentityModel.Tokens;
using RM_API.Models.DTOs;
using RM_API.Models;
using RM_API.Repositories.Interfaces;
using RM_API.Services.Interfaces;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace RM_API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptsService _encryptsService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IEncryptsService encryptsService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _encryptsService = encryptsService;
            _configuration = configuration;
        }

        public Response Login(UserLoginDTO userLoginDTO)
        {
            if (string.IsNullOrEmpty(userLoginDTO.Email) || string.IsNullOrEmpty(userLoginDTO.Password))
                return new Response(400, "Faltan campos", false);

            User user = _userRepository.FindByEmail(userLoginDTO.Email);

            if (user == null || !_encryptsService.VerifyPassword(userLoginDTO.Password, user.Salt, user.Hash))
                return new Response(401, "Credenciales inválidas", false);

            string token = MakeToken(user);

            return new ResponseModel<string>(200, "Ok", token);
        }

        public string MakeToken(User user)
        {
            var sessionTime = 10;

            var claims = new List<Claim>();

            claims.Add(new Claim("user", user.Email));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(sessionTime),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
