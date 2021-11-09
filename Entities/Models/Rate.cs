using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Entities.Models
{
    public enum LikeStatus
    { 
        Liked,
        Nothing,
        Disliked
    }

    public class Rate
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
