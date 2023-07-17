using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstateApp.Models;

namespace RealEstateApp.Repositories.RepositoryInterfaces
{
    public interface IPhotoRepository
    {
        Task SavePhotoAsync(Photo photo);

        Task<IEnumerable<string>> GetAllPhotosAsync(int propertyId);
    }
}
