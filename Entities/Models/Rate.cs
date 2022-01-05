using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Entities.Models
{
    public class Rate : ParentModel
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("PostId")]
        public int PostId { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
