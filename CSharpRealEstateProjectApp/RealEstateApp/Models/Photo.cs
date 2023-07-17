namespace RealEstateApp.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }

        public int PropertyId { get; set; }
        public string Url { get; set; } = string.Empty;

    }
}