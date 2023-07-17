using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Models.DTOs.Create;
using RealEstateApp.Services.ServiceInterfaces;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetPhotosByPropertyId(int propertyId)
        {
            try
            {
                IEnumerable<string> properties = await _photoService.GetPhotosByPropertyIdAsync(propertyId);
                return Ok(properties);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost()]
        public async Task<ActionResult<PostPhotoDto>> PostPhotoAsync(PostPhotoDto postPhotoDto)
        {
            try
            {
                await _photoService.PostPhotoAsync(postPhotoDto);
                return Ok(postPhotoDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
    }
}
