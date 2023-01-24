namespace AuthenticationPoc.Models.Responses
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string AuthenticationToken { get; set; } = string.Empty;
    }
}
