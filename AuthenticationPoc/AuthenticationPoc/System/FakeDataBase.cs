using AuthenticationPoc.Models;

namespace AuthenticationPoc.System
{
    public static class FakeDataBase
    {
        public static List<User> Users { get; set; } = new();
    }
}
