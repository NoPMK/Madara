using RealEstateApp.Models;

namespace RealEstateApp.Services.ServiceInterfaces
{
    public interface ITokenCreationService
    {
        void ClearRefreshToken(string refreshToken);
        Task<AuthenticationResponse> CreateTokensAsync(ApplicationUser user);
        Task<AuthenticationResponse> RenewTokensAsync(string refreshToken);
    }
}
