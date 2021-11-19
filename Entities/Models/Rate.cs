using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Entities.Models
{
    public class Rate : ParentModel
    {
        public User User { get; set; }

        public Post Post { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
