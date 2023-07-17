using Microsoft.EntityFrameworkCore;
using RealEstateApp.Models;

namespace RealEstateApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<Photo> Photos { get; set; }

    }
}
