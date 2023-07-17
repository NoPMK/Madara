using RealEstateApp.Models.DTOs;

namespace RealEstateApp.Models
{
    public class AuthenticationResponse
    {
        public JwtToken? AccessToken { get; set; }

        public JwtToken? RefreshToken { get; set; }
    }
}
