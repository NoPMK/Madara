

namespace RealEstateApp.Models.DTOs.Details
{
    public class UserDetailsDto
    {
      
        public string UserName { get; set; } = string.Empty;
     
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public ICollection<Property>? Properties { get; set; }
    }
}
