using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using RealEstateApp.Repositories.RepositoryInterfaces;
using RealEstateApp.Services.ServiceInterfaces;

namespace RealEstateUnitTests.Services
{
    [TestClass]
    public class PropertyTests
    {
        private AppDbContext? _appDbContext;
        private User _user;
        private IPropertyService _propertyService;
        private IPropertyRepository _propertyRepository;
        private IUserRepository _userRepository;

        [TestInitialize]
        public void Init()
        {
            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer("Server=localhost;Database=RealEstateDb;User Id=sa;Password=Password123!;TrustServerCertificate=True");
            _appDbContext = new AppDbContext(builder.Options);
            _appDbContext.Database.OpenConnection();
            _appDbContext.Database.Migrate();
        }
        [TestCleanup]
        public void TestCleanup()
        {
            _appDbContext?.Dispose();
        }

        //[TestMethod]
        //public void CreatePropertyTest()
        //{
        //    _appDbContext.Users.AddRange(
        //        new User 
        //        {
        //            Id = "1",
        //            Properties = null
        //        });

        //    _appDbContext.Properties.AddRange(
        //        new Property
        //        {
        //            CityName = "Kecskemét",
        //            County = Enum.Parse<CountyType>("Bacs"),
        //            District = Enum.Parse<DistrictType>("Other"),
        //            Comfort = Enum.Parse<ComfortType>("Comfort"),
        //            Condition = Enum.Parse<ConditionType>("Renovated"),
        //            Description = "Small house in Kecskemét.",
        //            GroundSize = 350,
        //            PropertySize = 80,
        //            Heat = Enum.Parse<HeatType>("Electric"),
        //            IsDeleted = false,
        //            IsForSale = true,
        //            IsHandicapped = false,
        //            IsSold = false,
        //            NumberOfFloors = Enum.Parse<FloorType>("Basement"),
        //            NumberOfHalfRooms = 1,
        //            NumberOfRooms = 3,
        //            Parking = Enum.Parse<ParkingType>("Simple"),
        //            Price = 150000,
        //            Type = Enum.Parse<PropertyType>("Office"),
        //            YearOfBuild = 2004,
        //            Street = "Petőfi Street",
        //            StreetNumber = 1,
        //            CreatedAt = DateTime.Now,
        //            IsAirConditionered = true,
        //            Id = 2,
        //        });
        //    _appDbContext.SaveChangesAsync();

        //    var propertyService = new PropertyService(_propertyRepository, _userRepository);
        //    var property = propertyService.GetPropertyByIdAsync(2);

        //    Assert.AreEqual("Petőfi Street", property.Result.Street);
        //    Assert.AreEqual(150000, property.Result.Price);
        //    Assert.AreEqual(true, property.Result.IsForSale);
        //    //_user = new User
        //    //{
        //    //    Id = "202",
        //    //    Properties = null
        //    //};
        //    //_appDbContext!.Users.Add(_user);
        //    //_appDbContext.SaveChanges();
        //    //Thread.Sleep(2000);
        //    //CreatePropertyDto propertyDto = new CreatePropertyDto
        //    //{
        //    //    CityName = "Kecskemét",
        //    //    County = "Bacs",
        //    //    District = "Other",
        //    //    Comfort = "Comfort",
        //    //    Condition = "Renovated",
        //    //    Description = "Small house in Kecskemét.",
        //    //    GroundSize = 350,
        //    //    PropertySize = 80,
        //    //    Heat = "Electric",
        //    //    IsDeleted = false,
        //    //    IsForSale = true,
        //    //    IsHandicapped = false,
        //    //    IsSold = false,
        //    //    NumberOfFloors = "Basement",
        //    //    NumberOfHalfRooms = 1,
        //    //    NumberOfRooms = 3,
        //    //    Parking = "Simple",
        //    //    Price = 150000,
        //    //    Type = "Office",
        //    //    YearOfBuild = 2004,
        //    //    Street = "Petőfi Street",
        //    //    StreetNumber = 1,
        //    //    IsAirConditioned = false,
        //    //    UserId = _user.Id,
        //    //    PropertyId = 1,
        //    //    CreatedAt = DateTime.Now.ToString(),

        //    //CityName = "Kecskemét",
        //    //        County = Enum.Parse<CountyType>("Bacs"),
        //    //        District = Enum.Parse<DistrictType>("Other"),
        //    //        Comfort = Enum.Parse<ComfortType>("Comfort"),
        //    //        Condition = Enum.Parse<ConditionType>("Renovated"),
        //    //        Description = "Small house in Kecskemét.",
        //    //        GroundSize = 350,
        //    //        PropertySize = 80,
        //    //        Heat = Enum.Parse<HeatType>("Electric"),
        //    //        IsDeleted = false,
        //    //        IsForSale = true,
        //    //        IsHandicapped = false,
        //    //        IsSold = false,
        //    //        NumberOfFloors = Enum.Parse<FloorType>("Basement"),
        //    //        NumberOfHalfRooms = 1,
        //    //        NumberOfRooms = 3,
        //    //        Parking = Enum.Parse<ParkingType>("Simple"),
        //    //        Price = 150000,
        //    //        Type = Enum.Parse<PropertyType>("Office"),
        //    //        YearOfBuild = 2004,
        //    //        Street = "Petőfi Street",
        //    //        StreetNumber = 1,
        //    //        CreatedAt = DateTime.Now,
        //    //        IsAirConditionered = true,
        //    //        Id = 2,
        //    //};
        //    //Thread.Sleep(3000);
        //    //_propertyService.CreatePropertyAsync(propertyDto, _user.Id);

        //    //Property? dbProperty = _appDbContext.Properties.SingleOrDefault(p => p.YearOfBuild == 2004);

        //    //Assert.AreEqual("Petőfi Street", dbProperty!.Street);
        //    //Assert.AreEqual(150000, dbProperty!.Price);
        //    //Assert.AreEqual(true, dbProperty!.IsForSale);
        //}
        //[TestMethod]
        //public void EditProperty()
        //{
        //    Thread.Sleep(8000);
        //    Property? property = _appDbContext.Properties.SingleOrDefault(p => p.YearOfBuild == 2004);

        //    UpdatePropertyDto propertyDto = new UpdatePropertyDto
        //    {
        //        Comfort = "Double",
        //        Condition = "Normal",
        //        Description = "Description changed",
        //        Heat = "Gas",
        //        IsAirConditioned = true,
        //        IsForSale = true,
        //        NumberOfHalfRooms = 1,
        //        NumberOfRooms = 1,
        //        Parking = "Street",
        //        Price = 1,
        //        PropertySize = 1,
        //    };
            
        //    _propertyService.UpdatePropertyById(property.Id, propertyDto);

        //    Property? dbProperty = _appDbContext.Properties.SingleOrDefault(p => p.Id == property.Id);

        //    Assert.AreEqual(1, dbProperty!.NumberOfRooms);
        //    Assert.AreEqual(1, dbProperty!.Price);
        //    Assert.AreEqual("Description changed", dbProperty!.Description);
        //}
        //[TestMethod]
        //public void PropertyDelete()
        //{
        //    Property? property = _appDbContext.Properties.SingleOrDefault(p => p.YearOfBuild == 2004);

        //    _propertyService.DeletePropertyByIdAsync(property.Id, "202");
        //}

        //[TestMethod]
        //public void ClearDatabaseProperties()
        //{
        //    SqlCommand info = new SqlCommand();
        //    info.Connection = new SqlConnection("Server = localhost; Database = RealEstateDb; User Id = sa; Password = Password123!; TrustServerCertificate = True");
        //    info.CommandType = CommandType.Text;
        //    info.CommandText = "DELETE FROM Properties";
        //    info.Connection.Open();
        //    info.ExecuteNonQuery();
        //}
        //[TestMethod]
        //public void ClearDatabaseUsers()
        //{
        //    SqlCommand info = new SqlCommand();
        //    info.Connection = new SqlConnection("Server = localhost; Database = RealEstateDb; User Id = sa; Password = Password123!; TrustServerCertificate = True");
        //    info.CommandType = CommandType.Text;
        //    info.CommandText = "DELETE FROM Users";
        //    info.Connection.Open();
        //    info.ExecuteNonQuery();
        //}

    }
}
