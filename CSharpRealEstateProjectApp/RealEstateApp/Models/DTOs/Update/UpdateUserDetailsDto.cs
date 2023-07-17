namespace RealEstateApp.Models.DTOs.Update
{
    public class UpdateUserDetailsDto
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string NewPasswordAgain { get; set; } = string.Empty;
    }
}
