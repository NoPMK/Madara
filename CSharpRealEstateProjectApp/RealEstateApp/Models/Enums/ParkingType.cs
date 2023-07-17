using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.Enums
{
    public enum ParkingType
    {
        [Display(Name = "Courtyard parking")]
        Garden,

        [Display(Name = "Underground parking space")]
        Together,

        [Display(Name = "Simple garage space")]
        Simple,

        [Display(Name = "Street parking")]
        Street,
    }
}