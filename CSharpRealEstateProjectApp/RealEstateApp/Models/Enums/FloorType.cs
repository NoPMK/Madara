using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.Enums
{
    public enum FloorType
    {
        [Display(Name = "Basement")]
        Basement,

        [Display(Name = "Ground floor")]
        GroundFloor,

        [Display(Name = "Mezzanine")]
        HalfFloor,

        [Display(Name = "1st floor")]
        One,

        [Display(Name = "2nd floor")]
        Two,

        [Display(Name = "3rd floor")]
        Three,

        [Display(Name = "4th floor")]
        Four,

        [Display(Name = "5th floor")]
        Five,

        [Display(Name = "6th floor")]
        Six,

        [Display(Name = "7th floor")]
        Seven,

        [Display(Name = "8th floor")]
        Eight,

        [Display(Name = "9th floor")]
        Nine,

        [Display(Name = "10th floor or higher")]
        TenOrMore,

        [Display(Name = "Loft")]
        Attic,
    }
}