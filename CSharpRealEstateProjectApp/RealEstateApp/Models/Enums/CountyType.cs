using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.Enums
{
    public enum CountyType
    {
        [Display(Name = "Other")]
        Other,

        [Display(Name = "Budapest")]
        Budapest,

        [Display(Name = "Bács-Kiskun vármegye")]
        Bacs,

        [Display(Name = "Baranya vármegye")]
        Baranya,

        [Display(Name = "Békés vármegye")]
        Bekes,

        [Display(Name = "Borsod-Abaúj-Zemplén vármegye")]
        BAZ,
        [Display(Name = "Csongrád-Csanád vármegye")]
        Csongrad,

        [Display(Name = "Fejér vármegye")]
        Fejer,

        [Display(Name = "Győr-Moson-Sopron vármegye")]
        Gyor,

        [Display(Name = "Hajdú-Bihar vármegye")]
        Hajdu,

        [Display(Name = "Heves vármegye")]
        Heves,

        [Display(Name = "Jász-Nagykun-Szolnok vármegye")]
        Jasz,

        [Display(Name = "Komárom-Esztergom vármegye")]
        Komarom,

        [Display(Name = "Nógrád vármegye")]
        Nograd,

        [Display(Name = "Pest vármegye")]
        Pest,

        [Display(Name = "Somogy vármegye")]
        Somogy,

        [Display(Name = "Szabolcs-Szatmár-Bereg vármegye")]
        Szabolcs,

        [Display(Name = "Tolna vármegye")]
        Tolna,

        [Display(Name = "Vas vármegye")]
        Vas,

        [Display(Name = "Veszprém vármegye")]
        Veszprem,

        [Display(Name = "Zala vármegye")]
        Zala,

    }
}