using RealEstateApp.Models;
using RealEstateApp.Models.DTOs.Create;
using RealEstateApp.Repositories;
using RealEstateApp.Repositories.RepositoryInterfaces;
using RealEstateApp.Services.ServiceInterfaces;

namespace RealEstateApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task PostPhotoAsync(PostPhotoDto postPhotoDto)
        {
            Photo photo = new Photo
            {
                PropertyId = postPhotoDto.PropertyId,
                Url = postPhotoDto.Url
            };
            
            await _photoRepository.SavePhotoAsync(photo);
        }
        public async Task<IEnumerable<string>> GetPhotosByPropertyIdAsync(int propertyId)
        {
            IEnumerable<string> photos = await _photoRepository.GetAllPhotosAsync(propertyId);
            if (photos is not null)
            {
                return photos;
            }
            else
            {
                throw new ArgumentNullException($"This property does not have any photos.");
            }
        }
    }
}
