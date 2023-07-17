using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.DTOs.Update
{
    public class UpdatePropertyDto
    {
        [Required(ErrorMessage = "This field is required to fill.")]
        [Range(0,int.MaxValue, ErrorMessage = "Price cannot be a negative number.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "This field is required to fill.")]
        [MinLength(10, ErrorMessage = "The features of the property and its surroundings are important to the viewers." +
            " Please write more about it!"), MaxLength(10000, ErrorMessage = "Maximum 10.000 characters. Please be more concise.")]
        public string Description { get; set; } = string.Empty;

        public string Photos { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required to fill.")]
        public bool IsForSale { get; set; }

        [Required(ErrorMessage = "This field is required to fill.")]
        [Range(0, int.MaxValue, ErrorMessage = "Property size cannot be a negative number.")]

        public int PropertySize { get; set; }

        [Required(ErrorMessage = "This field is required to fill.")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of rooms cannot be a negative number.")]
        public int NumberOfRooms { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number cannot be a negative number.")]
        public int NumberOfHalfRooms { get; set; }
        public string Condition { get; set; } = string.Empty;
        public string Heat { get; set; } = string.Empty;
        public string Comfort { get; set; } = string.Empty;
        public bool IsAirConditioned { get; set; }
        public string Parking { get; set; } = string.Empty;
    }
}
