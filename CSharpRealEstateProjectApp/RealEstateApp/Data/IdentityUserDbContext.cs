using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Models;

namespace RealEstateApp.Data
{
    public class IdentityUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> options) : base(options)
        {
        }
    }
}
