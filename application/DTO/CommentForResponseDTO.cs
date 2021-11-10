using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTO
{
    public class CommentForResponseDTO
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
        public string Text { get; set; }
    }
}
