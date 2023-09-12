namespace Domain.Entities.ApplicationUsers
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdASync(string id);
        void RegisterUser(ApplicationUser user);
        void UnregisterUserAsync(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
    }
}