using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Helper;
using RealEstateApp.Models.DTOs;
using RealEstateApp.Models.DTOs.Create;
using RealEstateApp.Models.DTOs.Details;
using RealEstateApp.Models.DTOs.Update;
using RealEstateApp.Models.Enums;
using RealEstateApp.Services.ServiceInterfaces;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PropertyListingDto>> PostProperty(CreatePropertyDto createPropertyDto)
        {
            try
            {
                string userId = User.GetCurrentUserId();

                await _propertyService.CreatePropertyAsync(createPropertyDto, userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<PropertyListingDto>>> GetAllProperties(SearchPropertyDto propertyDto)
        {
            try
            {
                IEnumerable<PropertyListingDto> properties = await _propertyService.GetAllPropertiesAsync(propertyDto);
                return Ok(properties);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("FormData")]
        public ActionResult<PropertyFormInitDataDto> GetFormInitData(string lang = "en")
        {
            PropertyFormInitDataDto result = _propertyService.GetFormInitData(lang);

            return Ok(result);
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<PropertyDetailsDto>> GetPropertyAsync(int propertyId)
        {
            try
            {
                PropertyDetailsDto propertyResult = await _propertyService.GetPropertyByIdAsync(propertyId);
                return Ok(propertyResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPropertyAsync(int id, UpdatePropertyDto propertytoUpdate)
        {
            try
            {
                await _propertyService.UpdatePropertyById(id, propertytoUpdate);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyAsync(int id)
        {
            try
            {
                string currentUserId = User.GetCurrentUserId();
                await _propertyService.DeletePropertyByIdAsync(id, currentUserId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
