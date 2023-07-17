using Microsoft.Data.SqlClient;
using RealEstateApp.Controllers;
using RealEstateApp.Models;
using RealEstateApp.Models.DTOs;
using RealEstateApp.Models.DTOs.Create;

namespace RealEstateApp.Repositories.RepositoryInterfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync(SearchPropertyDto propertyDto);
        Task<List<Property>> GetSelectedProperties(string sortProperty, SortOrder sortOrder);
        Task<Property> GetPropertyByIdAsync(int id);
        Task<User> GetUserAsync(string userId);
        Task SaveChangesAsync();
        Task SavePropertyAsync(Property property);
    }
}