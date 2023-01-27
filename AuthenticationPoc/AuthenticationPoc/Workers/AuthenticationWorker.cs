using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Interfaces.Helpers;
using AuthenticationPoc.Interfaces.Workers;
using AuthenticationPoc.Models;
using AuthenticationPoc.Models.Responses;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationPoc.Workers
{
    public class AuthenticationWorker: IAuthenticationWorker
    {
        private User User = new();
        private readonly IJwtTokenManager _jwtTokenManager;

        public AuthenticationWorker(IJwtTokenManager jwtTokenManager)
        {
            _jwtTokenManager = jwtTokenManager;
        }

        

        public async Task<LoginResponse> Login(UserDto userDto) 
        {
            //Don't remove the async clause because, after we are gonna need to call some repository methods;

            if (User.Username != userDto.Email || !PasswordHashMatches(userDto.Password, User.PasswordHash, User.PasswordSalt))
                return new LoginResponse()
                {
                    Success = false,
                    AuthenticationToken = string.Empty
                };

            return new LoginResponse()
            {
                Success = true,
                Token = _jwtTokenManager.GenerateJwtToken(userDto)
            };
        }

        public async Task<User> RegisterNewUser(UserDto userDto)
        {
            //Don't remove the async clause because, after we are gonna need to call some repository methods
            var hashResult = CreatePasswordHash(userDto.Password);

            User = new User()
            {
                PasswordHash = hashResult.PasswordHash,
                PasswordSalt = hashResult.PasswordSalt,
                Username = userDto.Email
            };

            return User;
        }

        private static PassordEncryptionResponse CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            return new PassordEncryptionResponse(
                   passwordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                   passwordSalt: hmac.Key
               );
        }
       
        private static bool PasswordHashMatches(string rawPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var newHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
            return newHash.SequenceEqual(passwordHash);
        }
    }
}
