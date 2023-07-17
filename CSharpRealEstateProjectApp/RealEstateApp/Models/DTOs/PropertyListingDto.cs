using RealEstateApp.Models.Enums;
using RealEstateApp.Models;
using RealEstateApp.Models.DTOs.Details;

namespace RealEstateApp.Controllers
{
    public class PropertyListingDto
    { 
        public int Id { get; set; }
        public string County { get; set; } = string.Empty;

        public string CityName { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public int StreetNumber { get; set; }

        public string Photos { get; set; }

        public bool IsForSale { get; set; }

        public int PropertySize { get; set; }

        public int NumberOfRooms { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}