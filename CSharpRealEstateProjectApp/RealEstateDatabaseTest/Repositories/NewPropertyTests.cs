using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Controllers;
using RealEstateApp.Data;
using RealEstateApp.Models;
using RealEstateApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateUnitTests.Repositories
{
    [TestClass]
    public class NewPropertyTests
    {
        private AppDbContext _appDbContext;

        [TestInitialize]
        public void Init()
        {
            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer("Server=localhost;Database=RealEstateDb;User Id=sa;Password=Password123!;TrustServerCertificate=True");
            _appDbContext = new AppDbContext(builder.Options);

        }

        [TestMethod]
        public void NewPropertyAndUserTest()
        {
            User user = new User
            {
                Id = "8887",
                Properties = null
            };

            User userTwo = new User
            {
                Id = "9997",
                Properties = null
            };

            _appDbContext!.Users.Add(user);
            _appDbContext!.Users.Add(userTwo);
            _appDbContext.SaveChanges();

            Property property = new Property
            {
                CityName = "Kecskemét",
                County = Enum.Parse<CountyType>("Bacs"),
                District = Enum.Parse<DistrictType>("Other"),
                Comfort = Enum.Parse<ComfortType>("Double"),
                Condition = Enum.Parse<ConditionType>("Normal"),
                CreatedAt = DateTime.Now,
                Description = "Small house in Kecskemét.",
                GroundSize = 350,
                PropertySize = 80,
                Heat = Enum.Parse<HeatType>("Gas"),
                IsAirConditionered = true,
                IsDeleted = false,
                IsForSale = true,
                IsHandicapped = false,
                IsSold = false,
                NumberOfFloors = Enum.Parse<FloorType>("GroundFloor"),
                NumberOfHalfRooms = 1,
                NumberOfRooms = 3,
                Parking = Enum.Parse<ParkingType>("Street"),
                Price = 150000,
                Type = Enum.Parse<PropertyType>("House"),
                User = user,
                YearOfBuild = 2004,
                Street = "Petőfi Street",
                StreetNumber = 1,
            };

            _appDbContext!.Properties.Add(property);
            _appDbContext.SaveChanges();

            List<User> dbUsers = _appDbContext.Users.ToList();
            Property? dbProperty = _appDbContext.Properties.SingleOrDefault(p => p.Id == property.Id);

            Assert.AreEqual(2, dbUsers.Count());
            Assert.AreEqual("Petőfi Street", dbProperty!.Street);
            Assert.AreEqual(150000, dbProperty!.Price);
            Assert.AreEqual(true, dbProperty!.IsForSale);
        }

        [TestMethod]
        public void ClearDatabaseProperties()
        {
            SqlCommand info = new SqlCommand();
            info.Connection = new SqlConnection("Server = localhost; Database = RealEstateDb; User Id = sa; Password = Password123!; TrustServerCertificate = True");
            info.CommandType = CommandType.Text;
            info.CommandText = "DELETE FROM Properties";
            info.Connection.Open();
            info.ExecuteNonQuery();
        }
        [TestMethod]
        public void ClearDatabaseUser()
        {
            SqlCommand info = new SqlCommand();
            info.Connection = new SqlConnection("Server = localhost; Database = RealEstateDb; User Id = sa; Password = Password123!; TrustServerCertificate = True");
            info.CommandType = CommandType.Text;
            info.CommandText = "DELETE FROM Users";
            info.Connection.Open();
            info.ExecuteNonQuery();
        }
        [TestMethod]
        public void IdentityInsertOn()
        {
            SqlCommand info = new SqlCommand();
            info.Connection = new SqlConnection("Server = localhost; Database = RealEstateDb; User Id = sa; Password = Password123!; TrustServerCertificate = True");
            info.CommandType = CommandType.Text;
            info.CommandText = "SET IDENTITY_INSERT Properties ON";
            info.Connection.Open();
            info.ExecuteNonQuery();
        }
        [TestMethod]
        public void SetIdentityInsertOff()
        {
            SqlCommand info = new SqlCommand();
            info.Connection = new SqlConnection("Server = localhost; Database = RealEstateDb; User Id = sa; Password = Password123!; TrustServerCertificate = True");
            info.CommandType = CommandType.Text;
            info.CommandText = "SET IDENTITY_INSERT Properties OFF";
            info.Connection.Open();
            info.ExecuteNonQuery();
        }
    }
}
