using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Interfaces.Helpers;
using AuthenticationPoc.Interfaces.Repositories;
using AuthenticationPoc.Interfaces.Workers;
using AuthenticationPoc.Models;
using AuthenticationPoc.Models.Enums;
using AuthenticationPoc.Models.Responses;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationPoc.Workers
{
    public class AuthenticationWorker: IAuthenticationWorker
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly IUserRepository _userRepository;

        public AuthenticationWorker(IJwtTokenManager jwtTokenManager, IUserRepository userRepository)
        {
            _jwtTokenManager = jwtTokenManager;
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> Login(LoginDto userDto) 
        {
            var databaseUser = (await _userRepository.Select(u => u.Email == userDto.Email))[0];
            
            if (databaseUser == null || !PasswordHashMatches(userDto.Password, databaseUser.PasswordHash, databaseUser.PasswordSalt))
                return new LoginResponse()
                {
                    Success = false,
                    AuthenticationToken = string.Empty
                };

            return new LoginResponse()
            {
                Success = true,
                AuthenticationToken = _jwtTokenManager.GenerateJwtToken(userDto, GetRoleObject(databaseUser.Role))
            };
        }

        private static IAccessRole GetRoleObject(string role)
        {
            switch (role)
            {
                case "Admin":{ return new AdminRole(); }
                case "Seller": { return new SellerRole(); }
                case "User": { return new UserRole(); }
                default: 
                    {
                        throw new Exception("Invalid role, the possible roles are \"Admin\",\"Seller\" and \"User\"");
                    }
            }
        }

        public async Task RegisterNewUser(RegisterDto registerDto)
        {
            var hashResult = CreatePasswordHash(registerDto.Password);

            await _userRepository.Insert(
                new User()
                {
                    Username = registerDto.Username,
                    Email = registerDto.Email,
                    PasswordHash = hashResult.PasswordHash,
                    PasswordSalt = hashResult.PasswordSalt,
                    Role = GetRoleObject(registerDto.Role).Value
                }
            );
        }

        private static PasswordEncryptionResponse CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            return new PasswordEncryptionResponse(
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
