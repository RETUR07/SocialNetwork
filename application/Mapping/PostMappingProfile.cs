using Application.DTO;
using Application.Services;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<PostForm, Post>()
                .ForMember(pf => pf.BlobIds,
                opt => opt.MapFrom(x => Converters.FormFilesToBlobs(x.Content)));

            CreateMap<Post, PostForResponseDTO>()
                .ForMember(p => p.Content,
                opt => opt.MapFrom(x => Converters.BlobsToFormFiles(x.BlobIds)));
        }
    }
}
