using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
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
        public int userId { get; set; }

        [ForeignKey("Post")]
        public int postId { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
