using RealEstateApp.Models;

namespace RealEstateApp.Repositories.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task DeleteUserAsync(ApplicationUser user);
        Task<ApplicationUser> GetApplicationUserDetailsByIdAsync(string userId);
        Task<User> GetUserDetailsByIdAsync(string id);
        Task AddUserAsync(User user);
        Task<string> GetRoleByIdAsync(string userId);
        Task SaveChangesAsync();
    }
}
