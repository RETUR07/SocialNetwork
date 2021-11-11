using AutoMapper;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using System.Linq;

namespace SocialNetwork.Application.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserForResponseDTO>()
                .ForMember(u => u.DateOfBirth, opt => opt.MapFrom(x => x.DateOfBirth.ToShortDateString()))
                .ForMember(u => u.Subscribers, opt => opt.MapFrom(x => x.Subscribers.Select(x => x.Id)))
                .ForMember(u => u.Friends, opt => opt.MapFrom(x => x.Friends.Select(x => x.Id)));

            CreateMap<UserForm, User>();
        }
    }
}
