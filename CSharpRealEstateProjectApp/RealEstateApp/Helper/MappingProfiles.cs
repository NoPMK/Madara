using AutoMapper;
using RealEstateApp.Controllers;
using RealEstateApp.Models;
using RealEstateApp.Models.DTOs.Details;
using RealEstateApp.Models.DTOs.Update;

namespace RealEstateApp.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Property, PropertyListingDto>().ReverseMap();
            CreateMap<Property, PropertyDetailsDto>().ReverseMap();
            CreateMap<Property, UpdatePropertyDto>().ReverseMap(); 
        }
    }
}
