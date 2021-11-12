using System.Collections.Generic;

namespace SocialNetwork.Entities.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public string Text { get; set; }

        public virtual User Author { get; set; }
        public virtual Post ParentPost { get; set; }
        public virtual IEnumerable<Blob> BlobIds { get; set; }
        public virtual IEnumerable<Post> Comments { get; set; }
    }
}
