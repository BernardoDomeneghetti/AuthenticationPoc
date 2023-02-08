using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Models.Enums;

namespace AuthenticationPoc.Interfaces.Helpers
{
    public interface IJwtTokenManager
    {
        string GenerateJwtToken(LoginDto userDto, IAccessRole accessRole);
    }
}