using Application.DTO;
using AutoMapper;
using Entities;

namespace Application.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserForResponseDTO>()
                .ForMember(c => c.DateOfBirth,
                opt => opt.MapFrom(x => x.DateOfBirth.ToShortDateString()));

            CreateMap<UserForUpdateOrCreationDTO, User>();
        }
    }
}
