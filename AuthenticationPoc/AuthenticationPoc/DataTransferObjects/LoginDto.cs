using Microsoft.AspNetCore.Identity;

namespace AuthenticationPoc.DataTransferObjects
{
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
