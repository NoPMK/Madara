using RealEstateApp.Models.Enums;

namespace RealEstateApp.Models.DTOs
{
    public class SearchPropertyShadowDto
    {
        public CountyType County { get; set; }
        public string CityName { get; set; }
        public DistrictType District { get; set; }
        public bool isForSale { get; set; }
        public int MinPropertySize { get; set; }
        public int MaxPropertySize { get; set; }
        public int MinNumberOfRooms { get; set; }
        public int MaxNumberOfRooms { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
