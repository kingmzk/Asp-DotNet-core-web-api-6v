using AutoMapper;

namespace MZWalksApi_6.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap(); //if not equal  .Formember(dest => dest.Id, options => options.MapFrom(src => src.RegionId));
        }
    }
}