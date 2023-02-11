namespace AuthenticationPoc.Models.Enums
{
    public interface IAccessRole
    {
        public string Value { get; }
    }

    public class AdminRole: IAccessRole
    {
        public string Value { get { return "Admin"; } }
    }
    public class SellerRole : IAccessRole
    {
        public string Value { get { return "Seller"; } }
    }
    public class UserRole : IAccessRole
    {
        public string Value { get { return "User"; } }
    }
}
