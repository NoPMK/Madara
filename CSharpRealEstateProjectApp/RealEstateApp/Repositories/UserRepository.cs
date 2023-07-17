using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using RealEstateApp.Repositories.RepositoryInterfaces;

namespace RealEstateApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityUserDbContext _identityDbContext;
        private readonly AppDbContext _appDbContext;

        public UserRepository(IdentityUserDbContext identityDbContext, AppDbContext appDbContext)
        {
            _identityDbContext = identityDbContext;
            _appDbContext = appDbContext;
        }

        public async Task<ApplicationUser> GetApplicationUserDetailsByIdAsync(string userId)
        {
            var userDetails = await _identityDbContext.Users
            .Where(user => user.Id == userId)
            .SingleAsync();

            if (userDetails is not null)
            {
                return userDetails;
            }
            else
            {
                throw new ArgumentNullException($"There is no user with id: {userId}");
            }
        }

        public async Task AddUserAsync(User user)
        {
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            _identityDbContext.Remove(user);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserDetailsByIdAsync(string id)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(userId => userId.Id == id) ?? throw new Exception($"There is no user with id: {id}.");
        }

        public async Task<string> GetRoleByIdAsync(string userId)
        {
            IdentityUserRole<string> identityUserRole = await _identityDbContext.UserRoles.FirstOrDefaultAsync(user => user.UserId == userId)
                ?? throw new ArgumentNullException("There is no role id with this user id.");

            string roleId = identityUserRole.RoleId;

            IdentityRole<string> identityRole = await _identityDbContext.Roles.FindAsync(roleId) 
                ?? throw new ArgumentNullException("There is no role with this role id.");

            return identityRole.Name ?? throw new Exception("There is no role for this user.");
        }

        public async Task SaveChangesAsync()
        {
            await _identityDbContext.SaveChangesAsync();
        }
    }
}