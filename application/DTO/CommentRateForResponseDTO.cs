using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTO
{
    public class CommentRateForResponseDTO
    {
        public int UserId { get; set; }
        public string LikeStatus { get; set; }
    }
}
