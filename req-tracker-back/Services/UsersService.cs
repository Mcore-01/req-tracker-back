using Microsoft.IdentityModel.Tokens;
using req_tracker_back.Models;
using req_tracker_back.Repositories;
using req_tracker_back.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace req_tracker_back.Services
{
    public class UsersService(UsersRepository repository)
    {
        private readonly UsersRepository _repository = repository;

        public async Task<UserDTO> Login(string login, string password)
        {
            User? user = await _repository.GetUser(login) ?? throw new Exception("Такой логин не найден");

            if (user.Password != password)
                throw new Exception("Неправильный пароль!");

            return await GetUserDTO(user);
        }

        private Task<UserDTO> GetUserDTO(User user)
        {
            return Task.Run(() =>
            {
                return new UserDTO
                {
                    UserID = user.Id,
                    UserName = user.Nickname,
                    Token = CreateJWT(user.Nickname)
                };
            });
        }

        private string CreateJWT(string username)
        {
            var claims = new List<Claim> { new(ClaimTypes.Name, username) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public IEnumerable<ViewUserDTO> GetAll(string? filter)
        {
            return _repository.GetAll(filter).Select(GetViewUserDTO);
        }

        private ViewUserDTO GetViewUserDTO(User user) {
            return new ViewUserDTO
            {
                Id = user.Id,
                Name = user.Nickname,
            };
        }
    }
}