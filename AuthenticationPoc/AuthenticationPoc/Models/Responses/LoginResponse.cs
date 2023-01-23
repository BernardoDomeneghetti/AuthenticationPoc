namespace AuthenticationPoc.Models.Responses
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
