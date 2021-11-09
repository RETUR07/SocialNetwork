using SocialNetwork.Entities.Models;

namespace SocialNetwork.Application.DTO
{
    public class RateForm
    {
        public int UserId { get; set; }

        public int PostId { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
