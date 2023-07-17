using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RealEstate.Models;
using RealEstateApp.Controllers;
using RealEstateApp.Data;
using RealEstateApp.Models;
using RealEstateApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateUnitTests.Repositories
{
    [TestClass]
    public class UserTests
    {
        private IdentityUserDbContext _userDbContext;
        private UserManager<ApplicationUser> _userManager;
        private UsersController _usersController;

        [TestInitialize]
        public void Init()
        {
            DbContextOptionsBuilder<IdentityUserDbContext> builder = new DbContextOptionsBuilder<IdentityUserDbContext>();
            builder.UseSqlServer("Server=localhost;Database=RealEstateIdentityDb;User Id=sa;Password=Password123!;TrustServerCertificate=True");
            _userDbContext = new IdentityUserDbContext(builder.Options);
        }

        [TestMethod]
        public void RegisterUserTest()
        {
            ApplicationUser newUser = new ApplicationUser
            {
                UserName = "username",
                Email = "email@email.com",
            };
            string password = "Password1!";
            _userDbContext.Users.Add(newUser);
            _userDbContext.SaveChanges();

            ApplicationUser? user = _userDbContext.Users.SingleOrDefault(u => u.UserName == newUser.UserName);

            Assert.AreEqual("username", user!.UserName);
            Assert.AreEqual("email@email.com", user!.Email);
        }

        [TestMethod]
        public void ClearDatabase()
        {
            SqlCommand info = new SqlCommand();
            info.Connection = new SqlConnection("Server = localhost; Database = RealEstateIdentityDb; User Id = sa; Password = Password123!; TrustServerCertificate = True");
            info.CommandType = CommandType.Text;
            info.CommandText = "DELETE FROM AspNetUsers";
            info.Connection.Open();
            info.ExecuteNonQuery();
        }
    }
}
