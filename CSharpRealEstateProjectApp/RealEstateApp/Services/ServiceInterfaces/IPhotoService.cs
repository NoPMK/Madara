using RealEstateApp.Controllers;
using RealEstateApp.Models;
using RealEstateApp.Models.DTOs.Create;

namespace RealEstateApp.Services.ServiceInterfaces
{
    public interface IPhotoService
    {
        Task PostPhotoAsync(PostPhotoDto postPhotoDto);

        Task<IEnumerable<string>> GetPhotosByPropertyIdAsync(int propertyId);
    }
}