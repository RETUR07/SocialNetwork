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


    public class Rate
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Post Post { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual StoredType StoredType { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
