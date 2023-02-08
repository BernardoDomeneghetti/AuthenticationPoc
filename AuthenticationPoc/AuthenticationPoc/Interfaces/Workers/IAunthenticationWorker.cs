using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Models;
using AuthenticationPoc.Models.Responses;

namespace AuthenticationPoc.Interfaces.Workers
{
    public interface IAuthenticationWorker
    {
        Task RegisterNewUser(RegisterDto userDto);
        Task<LoginResponse> Login(LoginDto userDto);
    }
}
