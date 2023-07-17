using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models.DTOs.Create
{
    public class ConfirmEmailDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}