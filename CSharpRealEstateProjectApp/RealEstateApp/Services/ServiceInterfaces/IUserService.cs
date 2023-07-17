using RealEstateApp.Models;
using RealEstateApp.Models.DTOs.Details;
using RealEstateApp.Models.DTOs.Update;

namespace RealEstateApp.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Task DeleteUserAsync(string id);
        Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);
        Task<AdminUserDetailsDto> AdminGetUserByIdAsync(string id);
        Task SaveUserAsync(User user);
        Task<string> GetApplicationUserRoleById(string id);
        Task<UserDetailsDto> GetUserByIdAsync(string id);
        Task UpdateUserByIdAsync(string id, UpdateUserDetailsDto updateUserDetails);
    }
}
