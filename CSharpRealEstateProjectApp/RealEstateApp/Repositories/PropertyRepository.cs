using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using RealEstateApp.Models.DTOs;
using RealEstateApp.Models.Enums;
using RealEstateApp.Repositories.RepositoryInterfaces;

namespace RealEstateApp.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _appDbContext;

        public PropertyRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync(SearchPropertyDto propertyDto)
        {
            bool isCurrentIsForSaleNotAll = true;
            bool isCountyNotNull = true;
            bool isDistrictNotNull = true;
            bool isCityNameNotWhiteSpace = true;
            bool currentIsForSale = true;

            if (propertyDto.isForSale == "all")
            {
                isCurrentIsForSaleNotAll = false;
            }
            else
            {
                if(propertyDto.isForSale == "false")
                {
                    currentIsForSale = false;
                }
            }
            if (propertyDto.County is null)
            {
                isCountyNotNull = false;
                propertyDto.County = "Other";
            }
            if (propertyDto.District is null)
            {
                isDistrictNotNull = false;
                propertyDto.District = "Other";
            }
            if (propertyDto.CityName == string.Empty)
            {
                isCityNameNotWhiteSpace = false;
            }
            if (propertyDto.MinPropertySize < 0)
            {
                propertyDto.MinPropertySize = 0;
            }
            if (propertyDto.MaxPropertySize < 1)
            {
                propertyDto.MaxPropertySize = int.MaxValue;
            }
            if (propertyDto.MinNumberOfRooms < 0)
            {
                propertyDto.MinNumberOfRooms = 0;
            }
            if (propertyDto.MaxNumberOfRooms < 1)
            {
                propertyDto.MaxNumberOfRooms = int.MaxValue;
            }
            if (propertyDto.MinPrice < 0)
            {
                propertyDto.MinPrice = 0;
            }
            if (propertyDto.MaxPrice < 1)
            {
                propertyDto.MaxPrice = int.MaxValue;
            }

            SearchPropertyShadowDto shadowDto = new SearchPropertyShadowDto
            {
                County = Enum.Parse<CountyType>(propertyDto.County),
                District = Enum.Parse<DistrictType>(propertyDto.District),
                CityName = propertyDto.CityName,
                isForSale = currentIsForSale,
                MinNumberOfRooms = propertyDto.MinNumberOfRooms,
                MaxNumberOfRooms = propertyDto.MaxNumberOfRooms,
                MinPropertySize = propertyDto.MinPropertySize,
                MaxPropertySize = propertyDto.MaxPropertySize,
                MaxPrice = propertyDto.MaxPrice,
                MinPrice = propertyDto.MinPrice
            };

            var properties = await _appDbContext.Properties
                .Where(x => x.IsDeleted == false)
                .Where(x => (isCountyNotNull)? x.County == shadowDto.County : true)
                .Where(x => (isDistrictNotNull)? x.District == shadowDto.District : true)
                .Where(x => (isCityNameNotWhiteSpace)? x.CityName.ToLower() == shadowDto.CityName.ToLower() : true)
                .Where(x => (isCurrentIsForSaleNotAll)? x.IsForSale == shadowDto.isForSale : true)
                .Where(x => x.PropertySize >= shadowDto.MinPropertySize && x.PropertySize <= shadowDto.MaxPropertySize)
                .Where(x => x.NumberOfRooms >= shadowDto.MinNumberOfRooms && x.NumberOfRooms <= shadowDto.MaxNumberOfRooms)
                .Where(x => x.Price >= shadowDto.MinPrice && x.Price <= shadowDto.MaxPrice)
                .Include(x => x.User)
                .ToListAsync();
                
            if (properties.Any())
            {
                return properties;
            }
            else
            {
                throw new InvalidOperationException("No result found. Please extend the filtering.");
            }
        }

        public async Task SavePropertyAsync(Property property)
        {
            await _appDbContext.Properties.AddAsync(property);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(string userId)
        {
            User? user = await _appDbContext.Users.FindAsync(userId);
            if (user is null)
                throw new Exception("There is no user with this Id");

            return user;
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            var properties = await _appDbContext.Properties
                .Where(x => x.IsDeleted == false)
                .Include(x => x.User)
                .ToListAsync();

            Property? result = properties.SingleOrDefault(property => property.Id == id);

            if (result is not null)
            {
                return result;
            }
            else
            {
                throw new ArgumentNullException($"There is no property with this Id: {id}");
            }
        }

        public async Task<List<Property>> GetSelectedProperties(string sortProperty, SortOrder sortOrder)
        {
            List<Property> properties = await _appDbContext.Properties.ToListAsync();

            if (sortProperty.ToLower() == "cityName")
            {
                if (sortOrder == SortOrder.Ascending)
                    properties = properties.OrderBy(p => p.CityName).ToList();
                else
                    properties = properties.OrderByDescending(p => p.CityName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    properties = properties.OrderBy(p => p.Price).ToList();
                else
                    properties = properties.OrderByDescending(p => p.Price).ToList();
            }
            return properties;
        }
    }
}
