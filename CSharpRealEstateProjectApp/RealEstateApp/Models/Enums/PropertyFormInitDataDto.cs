namespace RealEstateApp.Models.Enums
{
    public class PropertyFormInitDataDto
    {
        public List<EnumOptionsDto> Comforts { get; set; } = new();
        public List<EnumOptionsDto> Conditions { get; set; } = new();
        public List<EnumOptionsDto> Floors { get; set; } = new();
        public List<EnumOptionsDto> Counties { get; set; } = new();
        public List<EnumOptionsDto> Districts { get; set; } = new();
        public List<EnumOptionsDto> Heats { get; set; } = new();
        public List<EnumOptionsDto> Parkings { get; set; } = new();
        public List<EnumOptionsDto> Properties { get; set; } = new();
    }
}