using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using RealEstateApp.Repositories.RepositoryInterfaces;

namespace RealEstateApp.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly AppDbContext _appDbContext;

        public PhotoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task SavePhotoAsync(Photo photo)
        {
            await _appDbContext.Photos.AddAsync(photo);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<string>> GetAllPhotosAsync(int propertyId)
        {
            List<string> photos = await _appDbContext.Photos
                .Where(x => x.PropertyId == propertyId)
                .Select(x => x.Url)
                .ToListAsync();

            if (photos is not null)
            {
                return photos;
            }
            else
            {
                throw new ArgumentNullException($"The property with this id: {propertyId} does not have any photos.");
            }
        }
    }
}
