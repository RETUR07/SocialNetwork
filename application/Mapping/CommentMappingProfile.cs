using AutoMapper;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Mapping
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentForResponseDTO>();
            CreateMap<CommentForm, Comment>()
                .ForMember(c => c.Text, opt => opt.MapFrom(x => x.Text)).ForAllOtherMembers(x => x.Ignore());
            CreateMap<User, Comment>()
                .ForMember(c => c.User, opt => opt.MapFrom(u => u)).ForAllOtherMembers(x => x.Ignore());
            CreateMap<Post, Comment>()
                .ForMember(c => c.Post, opt => opt.MapFrom(p => p)).ForAllOtherMembers(x => x.Ignore());
        }
    }
}
