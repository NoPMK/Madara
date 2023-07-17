using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.Enums
{
    public enum ConditionType
    {
        [Display(Name = "Brand new")]
        BrandNew,

        [Display(Name = "Like-new")]
        Novel,

        [Display(Name = "Renovated")]
        Renovated,

        [Display(Name = "In good condition")]
        Good,

        [Display(Name = "In normal condition")]
        Normal,

        [Display(Name = "To be renovated")]
        ToBeRenovated,

        [Display(Name = "Under construction")]
        NotFinished,
    }
}
