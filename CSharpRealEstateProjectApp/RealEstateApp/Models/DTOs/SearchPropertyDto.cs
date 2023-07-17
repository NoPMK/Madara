namespace RealEstateApp.Models.DTOs
{
    public class SearchPropertyDto
    {
        public string? County { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string? District { get; set; }
        public string isForSale { get; set; }
        public int MinPropertySize { get; set; }
        public int MaxPropertySize { get; set; }
        public int MinNumberOfRooms { get; set; }
        public int MaxNumberOfRooms { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
