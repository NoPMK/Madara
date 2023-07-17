namespace RealEstateApp.Models.DTOs.Create
{
    public class PostPhotoDto
    {
        public string Url { get; set; } = string.Empty;
        public int PropertyId { get; set; }
    }
}