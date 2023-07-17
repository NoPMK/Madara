using RealEstateApp.Exceptions;
using RealEstateApp.Models;
using RealEstateApp.Models.DTOs.Details;
using RealEstateApp.Models.DTOs.Update;
using RealEstateApp.Repositories.RepositoryInterfaces;
using RealEstateApp.Services.ServiceInterfaces;

namespace RealEstateApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<AdminUserDetailsDto> AdminGetUserByIdAsync(string userId) 
        {
            ApplicationUser appUser = await _userRepository.GetApplicationUserDetailsByIdAsync(userId);
            string role = await _userRepository.GetRoleByIdAsync(userId);
            User user = await _userRepository.GetUserDetailsByIdAsync(userId);
            AdminUserDetailsDto userDetails = new AdminUserDetailsDto
            {
                UserName = appUser.UserName!,
                Role = role,
                Properties = user.Properties
            };

            if (userDetails is not null)
            {
                return userDetails;
            }
            else
            {
                throw new ArgumentNullException($"There is no user with Id: {userId}");
            }
        }

        public async Task<User> GetUserByIdAsync(ApplicationUser appUser, string id)
        {
            appUser = await _userRepository.GetApplicationUserDetailsByIdAsync(id);
            if (appUser is not null)
            {
                User user = await _userRepository.GetUserDetailsByIdAsync(id);
                return user;
            }
            else
            {
                throw new ArgumentNullException($"There is no user with Id: {id}");
            }
        }

        public async Task SaveUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            ApplicationUser appUser = await _userRepository.GetApplicationUserDetailsByIdAsync(id);

            if (appUser is not null)
            {
                await _userRepository.DeleteUserAsync(appUser);
            }
            else
            {
                throw new ArgumentNullException($"There is no User with this Id: {id}");
            }
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(string userId)
        {
            return await _userRepository.GetApplicationUserDetailsByIdAsync(userId);
        }

        public async Task<string> GetApplicationUserRoleById(string id)
        {
            return await _userRepository.GetRoleByIdAsync(id);
        }

        public async Task<UserDetailsDto> GetUserByIdAsync(string id)
        {
            ApplicationUser applicationUser = await _userRepository.GetApplicationUserDetailsByIdAsync(id);
            User user = await _userRepository.GetUserDetailsByIdAsync(id);

            if (user is null || applicationUser is null)
            {
                throw new ArgumentNullException($"There is no user with this Id: {id}");
            }
            else
            {
                UserDetailsDto userDetails = new UserDetailsDto
                {
                    UserName = applicationUser.UserName!,
                    UserEmail = applicationUser.Email!,
                    UserPhone = applicationUser.PhoneNumber ?? string.Empty,
                    Properties = user.Properties
                };
                return userDetails;
            }
        }

        public async Task UpdateUserByIdAsync(string id, UpdateUserDetailsDto updateUserDetails)
        {
            ApplicationUser applicationUser = await _userRepository.GetApplicationUserDetailsByIdAsync(id);

            applicationUser.UserName = updateUserDetails.UserName;
            applicationUser.Email = updateUserDetails.UserEmail;
            applicationUser.PhoneNumber = updateUserDetails.UserPhone;
            if (updateUserDetails.NewPassword != updateUserDetails.NewPasswordAgain)
            {
                throw new InvalidPasswordException("New password must match.");
            }
            await _userRepository.SaveChangesAsync();
        }
    }
}
