using Microsoft.EntityFrameworkCore;
using RealEstateApp.Models.Enums;

namespace RealEstateApp.Models
{
    public class Property
    {

        public int Id { get; set; }

        public CountyType County { get; set; }

        public string CityName { get; set; } = string.Empty;

        public DistrictType District { get; set; }

        public string Street { get; set; } = string.Empty;

        public int StreetNumber { get; set; }

        public bool IsForSale { get; set; } //vagy enum: eladó-kiadó

        public PropertyType Type { get; set; }

        public User? User { get; set; }

        public int PropertySize { get; set; }

        public int GroundSize { get; set; }

        public string Description { get; set; } = string.Empty;

        public int NumberOfRooms { get; set; }

        public int NumberOfHalfRooms { get; set; }

        public decimal Price { get; set; }

        public ConditionType Condition { get; set; }

        public HeatType Heat { get; set; }

        public int YearOfBuild { get; set; }

        public FloorType NumberOfFloors { get; set ; }

        public ComfortType Comfort { get; set; }

        public bool IsHandicapped { get; set; }

        public bool IsAirConditionered { get; set; }

        public ParkingType Parking { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsSold { get; set;}

        public DateTime CreatedAt { get; set; }
    }
}
