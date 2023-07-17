using RealEstateApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.DTOs.Create
{
    public class CreatePropertyDto
    {
        public string UserId { get; set; } = string.Empty;

        public int PropertyId { get; set; }
        [Required(ErrorMessage = "This field is required to fill.")]
        [MinLength(2, ErrorMessage = "There is no country name shorter than two characters!")]

        public string County { get; set; } = string.Empty;
        [Required]
        [MinLength(2, ErrorMessage = "There is no City name shorter than two characters!")]

        public string CityName { get; set; } = string.Empty;
       
        
        public string? District { get; set; } = string.Empty;
        [Required]

        public string Street { get; set; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Street number cannot be a negative number.")]
        public int StreetNumber { get; set; }
      
        public string Photos { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required to fill.")]
        public bool IsForSale { get; set; }

        [Required(ErrorMessage = "This field is required to choose.")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required to fill.")]
        [Range(1, int.MaxValue, ErrorMessage = "Property size cannot be a negative number.")]
        public int PropertySize { get; set; }

        [Required(ErrorMessage = "This field is required to fill.")]
        [Range(1, int.MaxValue, ErrorMessage = "Ground size cannot be a negative number.")]
        public int GroundSize { get; set; }

        [Required(ErrorMessage = "This field is required to fill.")]
        [MinLength(10, ErrorMessage = "The features of the property and its surroundings are important to the viewers." +
            " Please write more about it!"), MaxLength(10000, ErrorMessage = "Maximum 10.000 characters. Please be more concise.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required to fill.")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of rooms cannot be a negative number.")]
        public int NumberOfRooms { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number cannot be a negative number.")]
        public int NumberOfHalfRooms { get; set; }

        [Required(ErrorMessage = "This field is required to fill.")]
        [Range(0, int.MaxValue, ErrorMessage = "Price cannot be a negative number.")]
        public decimal Price { get; set; }

        public string? Condition { get; set; } = string.Empty;

        public string? Heat { get; set; } = string.Empty;

        [Range(0, 2028, ErrorMessage = "Year must be more then 0, but less than this year plus five if it's under construction.")]
        public int YearOfBuild { get; set; }

        public string? NumberOfFloors { get; set; } = string.Empty;

        public string? Comfort { get; set; } = string.Empty;

       // [Required(ErrorMessage = "This field is required to choose.")]
        public bool IsHandicapped { get; set; }

       // [Required(ErrorMessage = "This field is required to choose.")]
        public bool IsAirConditioned { get; set; }

        public string? Parking { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public bool IsSold { get; set; }

        public string CreatedAt { get; set; } = string.Empty;
    }
}