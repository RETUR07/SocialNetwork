using SocialNetwork.Entities.Models;
using System;

namespace SocialNetwork.Application.DTO
{
    public class PostRateForResponseDTO
    {
        public int UserId { get; set; }
        public string LikeStatus { get; set; }
    }
}
