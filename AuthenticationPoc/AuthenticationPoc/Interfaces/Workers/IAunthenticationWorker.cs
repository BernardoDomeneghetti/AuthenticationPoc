using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Models;
using AuthenticationPoc.Models.Responses;

namespace AuthenticationPoc.Interfaces.Workers
{
    public interface IAuthenticationWorker
    {
        Task<User> RegisterNewUser(UserDto userDto);
        Task<LoginResponse> Login(UserDto userDto);
    }
}
