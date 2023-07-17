using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.Enums
{
    public enum PropertyType
    {
        [Display(Name = "Flat")]
        Flat,

        [Display(Name = "House")]
        House,

        [Display(Name = "Plot")]
        Plot,

        [Display(Name = "Office")]
        Office,

        [Display(Name = "Garage")]
        Garage,

        [Display(Name = "Storge")]
        Storage,
    }
}