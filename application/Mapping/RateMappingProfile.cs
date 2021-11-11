using AutoMapper;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;

namespace SocialNetwork.Application.Mapping
{
    public class RateMappingProfile : Profile
    {
        public RateMappingProfile()
        {
            CreateMap<Rate, PostRateForResponseDTO>()
                .ForMember(r => r.UserId, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(r => r.LikeStatus, opt => opt.MapFrom(x => x.LikeStatus.ToString()));

            CreateMap<Rate, CommentRateForResponseDTO>()
               .ForMember(r => r.UserId, opt => opt.MapFrom(x => x.User.Id))
               .ForMember(r => r.LikeStatus, opt => opt.MapFrom(x => x.LikeStatus.ToString()));


            CreateMap<User, Rate>()
                .ForMember(r => r.User, opt => opt.MapFrom(x => x)).ForAllOtherMembers(x => x.Ignore());
            CreateMap<Post, Rate>()
                .ForMember(r => r.Post, opt => opt.MapFrom(x => x)).ForAllOtherMembers(x => x.Ignore());
            CreateMap<Comment, Rate>()
                .ForMember(r => r.Comment, opt => opt.MapFrom(x => x)).ForAllOtherMembers(x => x.Ignore());
            CreateMap<RateForm, Rate>()
                .ForMember(r => r.LikeStatus, opt => opt.MapFrom(x => x.LikeStatus)).ForAllOtherMembers(x => x.Ignore());
        }
    }
}
