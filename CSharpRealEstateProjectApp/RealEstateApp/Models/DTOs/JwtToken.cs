namespace RealEstateApp.Models.DTOs
{
    public class JwtToken
    {
        public required string Value { get; init; }

        public required DateTime Expiration { get; init; }
    }
}
