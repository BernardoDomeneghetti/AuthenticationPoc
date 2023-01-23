namespace AuthenticationPoc.Models.Responses
{
    public class PassordEncryptionResponse
    {
        public PassordEncryptionResponse(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
