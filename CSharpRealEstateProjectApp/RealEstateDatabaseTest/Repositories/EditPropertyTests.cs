using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models.Enums;
using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateUnitTests.Repositories
{
    [TestClass]
    public class EditPropertyTests
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
        public void EditPropertyTest()
        {
            User user = new User
            {
                Id = "999254",
                Properties = null
            };

            _appDbContext!.Users.Add(user);
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

            Property? dbProperty = _appDbContext.Properties.SingleOrDefault(p => p.CityName == "Kecskemét");

            dbProperty!.Street = "Madara Street";
            dbProperty!.Price = 200000;
            dbProperty!.IsForSale = false;
            _appDbContext.SaveChangesAsync();

            Thread.Sleep(1000);

            Property? updatedProperty = _appDbContext.Properties.SingleOrDefault(p => p.CityName == "Kecskemét");
            Assert.AreEqual("Madara Street", updatedProperty!.Street);
            Assert.AreEqual(200000, updatedProperty!.Price);
            Assert.AreEqual(false, updatedProperty!.IsForSale);
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
        public void ClearDatabaseUsers()
        {
            SqlCommand info = new SqlCommand();
            info.Connection = new SqlConnection("Server = localhost; Database = RealEstateDb; User Id = sa; Password = Password123!; TrustServerCertificate = True");
            info.CommandType = CommandType.Text;
            info.CommandText = "DELETE FROM Users";
            info.Connection.Open();
            info.ExecuteNonQuery();
        }
        [TestMethod]
        public void DbIdentityInsertOn()
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
