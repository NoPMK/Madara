using RealEstateApp.Resources;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.Enums
{
    public enum ComfortType
    {
        [Display(Name = nameof(DisplayNames.Substandard), ResourceType = typeof(DisplayNames))]
        //[Display(Name = "Substandard")]
        None,

        [Display(Name = "Low Level convenience")]
        Half,

        [Display(Name = "Middle level convenience")]
        Comfort,

        [Display(Name = "With all conveniences")]
        Double,

        [Display(Name = "Luxury")]
        Luxury,
    }
}