using Application.DTO;
using AutoMapper;
using Entities;

namespace SocialNetwork.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForResponseDTO>()
                .ForMember(c => c.DateOfBirth,
                opt => opt.MapFrom(x => x.DateOfBirth.ToShortDateString()));

            CreateMap<UserForUpdateOrCreationDTO, User>();
        }
    }
}
