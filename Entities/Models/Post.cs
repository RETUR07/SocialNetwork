using System.Collections.Generic;

namespace SocialNetwork.Entities.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public string Text { get; set; }

        public IEnumerable<Blob> BlobIds { get; set; }
    }
}
