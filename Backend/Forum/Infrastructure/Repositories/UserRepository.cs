using Application.Data;
using Domain.Entities.ApplicationUsers;

namespace Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private IForumDbContext _forumDbContext;

        public UserRepository(IForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task<ApplicationUser?> GetByIdASync(string id)
        {
            var user = await _forumDbContext.Users.FindAsync(id);

            return user;
        }

        public void RegisterUser(ApplicationUser user)
        {
            _forumDbContext.Users.Add(user);

        }

        public async void UnregisterUserAsync(ApplicationUser user)
        {
            var neededToRemove = await GetByIdASync(user.Id);

            if (neededToRemove is null)
            {
                return;
            }

            _forumDbContext.Users.Remove(neededToRemove);
        }

        public async void UpdateUser(ApplicationUser user)
        {
            var neededToUpdate = await GetByIdASync(user.Id);

            if (neededToUpdate is null)
            {
                return;
            }

            neededToUpdate.Email = user.Email;

            neededToUpdate.PhoneNumber = user.PhoneNumber;

            neededToUpdate.UserName = user.UserName;

        }
    }

}
