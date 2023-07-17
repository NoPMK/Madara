using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.Enums
{
    public enum HeatType
    {
        [Display(Name = "Without heating")]
        NoHeating,

        [Display(Name = "Gas heating")]
        Gas,

        [Display(Name = "Electric heating")]
        Electric,

        [Display(Name = "Central heating")]
        District,

        [Display(Name = "Custom heating")]
        Other,
    }
}