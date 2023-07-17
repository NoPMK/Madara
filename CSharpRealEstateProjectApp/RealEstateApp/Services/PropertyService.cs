using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Extensions;
using RealEstateApp.Controllers;
using RealEstateApp.Exceptions;
using RealEstateApp.Models;
using RealEstateApp.Models.DTOs;
using RealEstateApp.Models.DTOs.Create;
using RealEstateApp.Models.DTOs.Details;
using RealEstateApp.Models.DTOs.Update;
using RealEstateApp.Models.Enums;
using RealEstateApp.Repositories.RepositoryInterfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RealEstateApp.Services.ServiceInterfaces
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;

        public PropertyService(IPropertyRepository propertyRepository, IUserRepository userRepository)
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
        }

        public async Task CreatePropertyAsync(CreatePropertyDto createPropertyDto, string userId)
        {
            User user = await _propertyRepository.GetUserAsync(userId);

            if(createPropertyDto.District is null)
            {
                createPropertyDto.District = "Other";
            };

            Property property = new Property
            {
                User = user,
                Comfort = Enum.Parse<ComfortType>(createPropertyDto.Comfort),
                Condition = Enum.Parse<ConditionType>(createPropertyDto.Condition),
                CreatedAt = DateTime.UtcNow,
                Description = createPropertyDto.Description,
                GroundSize = createPropertyDto.GroundSize,
                Heat = Enum.Parse<HeatType>(createPropertyDto.Heat),
                IsAirConditionered = createPropertyDto.IsAirConditioned,
                IsForSale = createPropertyDto.IsForSale,
                IsHandicapped = createPropertyDto.IsHandicapped,
                NumberOfFloors = Enum.Parse<FloorType>(createPropertyDto.NumberOfFloors),
                NumberOfHalfRooms = createPropertyDto.NumberOfHalfRooms,
                NumberOfRooms = createPropertyDto.NumberOfRooms,
                CityName = createPropertyDto.CityName,
                County = Enum.Parse<CountyType>(createPropertyDto.County),
                Street = createPropertyDto.Street,
                District = Enum.Parse<DistrictType>(createPropertyDto.District),
                StreetNumber = createPropertyDto.StreetNumber,
                Parking = Enum.Parse<ParkingType>(createPropertyDto.Parking),
                Price = createPropertyDto.Price,
                PropertySize = createPropertyDto.PropertySize,
                Type = Enum.Parse<PropertyType>(createPropertyDto.Type),
                YearOfBuild = createPropertyDto.YearOfBuild,
            };

            await _propertyRepository.SavePropertyAsync(property);
        }

        public async Task<IEnumerable<PropertyListingDto>> GetAllPropertiesAsync(SearchPropertyDto propertyDto)
        {
            
            IEnumerable<Property> properties = await _propertyRepository.GetAllPropertiesAsync(propertyDto);

            IEnumerable<PropertyListingDto> result = properties.Select(property => new PropertyListingDto
            {
                Id = property.Id,
                District = property.District.GetType().GetMember(property.District.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                CityName = property.CityName,
                County = property.County.GetType().GetMember(property.County.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                IsForSale = property.IsForSale,
                NumberOfRooms = property.NumberOfRooms,
                Price = property.Price,
                PropertySize = property.PropertySize,
                Street = property.Street,
                StreetNumber = property.StreetNumber,
                UserId = property.User.Id
            });

            return result;
        }

        public async Task<PropertyDetailsDto> GetPropertyByIdAsync(int id)
        {
            Property property = await _propertyRepository.GetPropertyByIdAsync(id);

            PropertyDetailsDto result = new PropertyDetailsDto
            {
                Comfort = property.Comfort.GetType().GetMember(property.Comfort.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                Id = property.Id,
                CityName = property.CityName,
                Condition = property.Condition.GetType().GetMember(property.Condition.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                County = property.County.GetType().GetMember(property.County.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                CreatedAt = property.CreatedAt,
                Description = property.Description,
                District = property.District.GetType().GetMember(property.District.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                GroundSize = property.GroundSize,
                Heat = property.Heat.GetType().GetMember(property.Heat.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                IsAirConditionered = property.IsAirConditionered,
                IsForSale = property.IsForSale,
                IsHandicapped = property.IsHandicapped,
                NumberOfFloors = property.NumberOfFloors.GetType().GetMember(property.NumberOfFloors.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                NumberOfHalfRooms = property.NumberOfHalfRooms,
                NumberOfRooms = property.NumberOfRooms,
                Parking = property.Parking.GetType().GetMember(property.Parking.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                UserId = property.User.Id,
                Price = property.Price,
                PropertySize = property.PropertySize,
                Street = property.Street,
                StreetNumber = property.StreetNumber,
                Type = property.Type.GetType().GetMember(property.Type.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                YearOfBuild = property.YearOfBuild
            };

            if (property is not null)
            {
                return result;
            }
            else
            {
                throw new ArgumentNullException($"There is no Property with this Id: {id}");
            }
        }

        public async Task<List<Property>> GetSelectedProperties(string sortProperty, SortOrder sortOrder)
        {
            return await _propertyRepository.GetSelectedProperties(sortProperty, sortOrder);
        }

        public PropertyFormInitDataDto GetFormInitData(string lang)
        {
            string cultureId = "en-US";
            if (lang == "hu")
            {
                cultureId = "hu";
            }
            var culture = new System.Globalization.CultureInfo(cultureId);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            PropertyFormInitDataDto result = new PropertyFormInitDataDto
            {
                Comforts = Enum.GetValues(typeof(ComfortType))
                .Cast<ComfortType>()
                .Select(comfort => new EnumOptionsDto
                {
                    Name = comfort.ToString(),
                    DisplayName = comfort.GetAttributeOfType<DisplayAttribute>().Name!
                }).ToList(),

                Conditions = Enum.GetValues(typeof(ConditionType))
                .Cast<ConditionType>()
                .Select(condition => new EnumOptionsDto
                {
                    Name = condition.ToString(),
                    DisplayName = condition.GetAttributeOfType<DisplayAttribute>().Name!
                }).ToList(),

                Floors = Enum.GetValues(typeof(FloorType))
                .Cast<FloorType>()
                .Select(floor => new EnumOptionsDto
                {
                    Name = floor.ToString(),
                    DisplayName = floor.GetAttributeOfType<DisplayAttribute>().Name!
                }).ToList(),

                Heats = Enum.GetValues(typeof(HeatType))
                .Cast<HeatType>()
                .Select(heat => new EnumOptionsDto
                {
                    Name = heat.ToString(),
                    DisplayName = heat.GetAttributeOfType<DisplayAttribute>().Name!
                }).ToList(),

                Counties = Enum.GetValues(typeof(CountyType))
                .Cast<CountyType>()
                .Select(county => new EnumOptionsDto
                {
                    Name = county.ToString(),
                    DisplayName = county.GetAttributeOfType<DisplayAttribute>().Name!
                }).ToList(),

                Districts = Enum.GetValues(typeof(DistrictType))
                .Cast<DistrictType>()
                .Select(district => new EnumOptionsDto
                {
                    Name = district.ToString(),
                    DisplayName = district.GetAttributeOfType<DisplayAttribute>().Name!
                }).ToList(),

                Parkings = Enum.GetValues(typeof(ParkingType))
                .Cast<ParkingType>()
                .Select(parking => new EnumOptionsDto
                {
                    Name = parking.ToString(),
                    DisplayName = parking.GetAttributeOfType<DisplayAttribute>().Name!
                }).ToList(),

                Properties = Enum.GetValues(typeof(PropertyType))
                .Cast<PropertyType>()
                .Select(property => new EnumOptionsDto
                {
                    Name = property.ToString(),
                    DisplayName = property.GetAttributeOfType<DisplayAttribute>().Name!
                }).ToList()
            };

            return result;
        }

        public async Task UpdatePropertyById(int id, UpdatePropertyDto propertyToUpdate)
        {
            Property property = await _propertyRepository.GetPropertyByIdAsync(id);

            if (property is not null)
            {
                property.Price = propertyToUpdate.Price;
                property.Description = propertyToUpdate.Description;
                property.IsForSale = propertyToUpdate.IsForSale;
                property.Comfort =Enum.Parse<ComfortType>(propertyToUpdate.Comfort);
                property.Condition =Enum.Parse<ConditionType>(propertyToUpdate.Condition);
                property.IsAirConditionered = propertyToUpdate.IsAirConditioned;
                property.NumberOfRooms = propertyToUpdate.NumberOfRooms;
                property.NumberOfHalfRooms = propertyToUpdate.NumberOfHalfRooms;
                property.Heat=Enum.Parse<HeatType>(propertyToUpdate.Heat);
                property.Parking=Enum.Parse<ParkingType>(propertyToUpdate.Parking);
                property.PropertySize = propertyToUpdate.PropertySize;

                await _propertyRepository.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException($"There is no Property with this Id: {id}");
            }
        }

        public async Task DeletePropertyByIdAsync(int propertyId, string currentUserId)
        {
            Property result = await _propertyRepository.GetPropertyByIdAsync(propertyId);

            User user = await _propertyRepository.GetUserAsync(currentUserId);

            string role = await _userRepository.GetRoleByIdAsync(currentUserId);

            if (result is not null)
            {
                if (result.User.Id == user.Id || role == "Admin")
                {
                    result.IsDeleted = true;
                    await _propertyRepository.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidPermissionException("It looks like you don’t have permission to delete this property.");
                }
            }
            else
            {
                throw new ArgumentNullException($"There is no Property with this Id: {propertyId}");
            }
        }
    }
}
