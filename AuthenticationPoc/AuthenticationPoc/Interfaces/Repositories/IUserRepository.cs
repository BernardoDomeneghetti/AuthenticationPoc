using AuthenticationPoc.Models;

namespace AuthenticationPoc.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task Insert(User user);
        public Task Update(User user);
        public Task Delete(User user);
        public Task<List<User>> Select(Func<User, bool> wherefilter);
    }
}
