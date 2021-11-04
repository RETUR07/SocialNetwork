using AutoMapper;
using SocialNetwork.Models;
using SocialNetwork.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
