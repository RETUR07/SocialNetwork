using AutoMapper;
using SocialNetwork.Application.DTO;
using SocialNetwork.Application.Services;
using SocialNetwork.Entities.Models;

namespace SocialNetwork.Application.Mapping
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<PostForm, Post>()
                .ForMember(pf => pf.BlobIds,
                opt => opt.MapFrom(x => x.Content.FormFilesToBlobs()));

            CreateMap<Post, PostForResponseDTO>()
                .ForMember(p => p.Content,
                opt => opt.MapFrom(x => x.BlobIds.BlobsToFileContentResults()));
        }
    }
}
