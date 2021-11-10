using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTO
{
    public class CommentForm
    {
        public int User { get; set; }
        public int Post { get; set; }
        public string Text { get; set; }
    }
}
