using SocialNetwork.Entities.SecurityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Entities.Models
{
    public class User : ParentModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public virtual List<User> Friends { get; set; }
        public virtual List<User> MakedFriend { get; set; }

        public virtual List<User> Subscribers { get; set; }
        public virtual List<User> Subscribed { get; set; }

        public virtual List<Chat> Chats { get; set; }
        public virtual List<Message> Messages { get; set; }


        public string PasswordHash { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
        public string Username { get; set; }
    }
}
