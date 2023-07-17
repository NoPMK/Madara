namespace RealEstateApp.Models.DTOs.Details
{
    public class UserInfo
    {
        public required string UserId { get; init; }
        public required string UserName { get; init; }
        public required string Email { get; init; }
        public required IEnumerable<string> Roles { get; init; }
    }
}
