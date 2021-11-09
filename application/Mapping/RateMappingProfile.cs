using AutoMapper;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;

namespace SocialNetwork.Application.Mapping
{
    public class RateMappingProfile : Profile
    {
        public RateMappingProfile()
        {
            CreateMap<Rate, RateForResponseDTO>();
            CreateMap<RateForm, Rate>();
        }
    }
}
