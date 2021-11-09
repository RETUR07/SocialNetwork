using SocialNetwork.Entities.Models;

namespace SocialNetwork.Application.DTO
{
    public class RateForResponseDTO
    {
        public int UserId { get; set; }

        public int PostId { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
