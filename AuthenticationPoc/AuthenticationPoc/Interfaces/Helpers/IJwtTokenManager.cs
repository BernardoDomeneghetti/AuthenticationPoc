using AuthenticationPoc.DataTransferObjects;

namespace AuthenticationPoc.Interfaces.Helpers
{
    public interface IJwtTokenManager
    {
        string GenerateJwtToken(UserDto userDto);
    }
}