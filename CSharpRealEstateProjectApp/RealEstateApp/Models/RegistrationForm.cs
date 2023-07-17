using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class RegistrationForm
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(2, ErrorMessage = "User name must be longer than 2 character!")]
        [MaxLength(50, ErrorMessage = "User name can't be longer than 50 character!")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be longer than 8 character!")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage ="Email is invalid")]
        public string Email { get; set; } = string.Empty;
    }
}
