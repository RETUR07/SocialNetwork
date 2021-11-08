 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public string Text { get; set; }

        public IEnumerable<int> BlobIds { get; set; }
    }
}
