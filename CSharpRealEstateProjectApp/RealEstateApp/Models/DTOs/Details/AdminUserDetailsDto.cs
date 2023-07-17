namespace RealEstateApp.Models.DTOs.Details
{
    public class AdminUserDetailsDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public ICollection<Property>? Properties { get; set; }
    }
}
