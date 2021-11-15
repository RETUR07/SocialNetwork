using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Entities.Models
{
    public enum LikeStatus
    { 
        Liked,
        Disliked
    }

    public enum StoredType
    { 
        PostLike,
        CommentLike
    }


    public class Rate : ParentModel
    {
        public virtual User User { get; set; }

        public virtual Post Post { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
