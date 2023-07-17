using RealEstateApp.Models.Enums;

namespace RealEstateApp.Models.DTOs.Details
{
    public class PropertyDetailsDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string County { get; set; }

        public string CityName { get; set; }

        public string District { get; set; }

        public string Street { get; set; } = string.Empty;

        public int StreetNumber { get; set; }

        public string Photos { get; set; } = string.Empty;

        public bool IsForSale { get; set; }

        public string Type { get; set; }

        public int PropertySize { get; set; }

        public int GroundSize { get; set; }

        public string Description { get; set; } = string.Empty;

        public int NumberOfRooms { get; set; }

        public int NumberOfHalfRooms { get; set; }

        public decimal Price { get; set; }

        public string Condition { get; set; }

        public string Heat { get; set; }

        public int YearOfBuild { get; set; }

        public string NumberOfFloors { get; set; }

        public string Comfort { get; set; }

        public bool IsHandicapped { get; set; }

        public bool IsAirConditionered { get; set; }

        public string Parking { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
