using AuthenticationPoc.Interfaces.Repositories;
using AuthenticationPoc.Models;
using AuthenticationPoc.System;

namespace AuthenticationPoc.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task Delete(User user)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(User user)
        {
            FakeDataBase.Users.Add(user);
        }

        public async Task<List<User>> Select(Func<User, bool> wherefilter)
        {
            return FakeDataBase.Users.Where(wherefilter).ToList();
        }

        public async Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
