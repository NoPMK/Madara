using System.ComponentModel.DataAnnotations;
namespace RealEstateApp.Models
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Username is required.")]
       
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage ="Password is required!")]
        public string Password { get; set; } = string.Empty;
    }
}
