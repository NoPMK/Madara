using Microsoft.Data.SqlClient;
using RealEstateApp.Controllers;
using RealEstateApp.Models;
using RealEstateApp.Models.DTOs;
using RealEstateApp.Models.DTOs.Create;
using RealEstateApp.Models.DTOs.Details;
using RealEstateApp.Models.DTOs.Update;
using RealEstateApp.Models.Enums;

namespace RealEstateApp.Services.ServiceInterfaces
{
    public interface IPropertyService
    {
        Task CreatePropertyAsync(CreatePropertyDto createPropertyDto, string userId);
        Task DeletePropertyByIdAsync(int propertyId, string currentUserId);
        Task<IEnumerable<PropertyListingDto>> GetAllPropertiesAsync(SearchPropertyDto propertyDto);
        PropertyFormInitDataDto GetFormInitData(string lang);
        Task<PropertyDetailsDto> GetPropertyByIdAsync(int id);
        Task<List<Property>> GetSelectedProperties(string sortProperty, SortOrder sortOrder);
        Task UpdatePropertyById(int id, UpdatePropertyDto propertyToUpdate);
    }
}