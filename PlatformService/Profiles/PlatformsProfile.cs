using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        CreateMap<Platform, PlatformReadDTO>().ReverseMap();
        CreateMap<PlatformCreateDTO, Platform>().ReverseMap();
        CreateMap<PlatformReadDTO, PlatformNotSupportedException>().ReverseMap();

        CreateMap<PlatformReadDTO, PlatformPublishedDto>();
        CreateMap<Platform, GrpcPlatformModel>()
        .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
    }
}