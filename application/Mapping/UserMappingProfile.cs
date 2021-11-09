using AutoMapper;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;

namespace SocialNetwork.Application.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserForResponseDTO>()
                .ForMember(c => c.DateOfBirth,
                opt => opt.MapFrom(x => x.DateOfBirth.ToShortDateString()));

            CreateMap<UserForm, User>();
        }
    }
}
