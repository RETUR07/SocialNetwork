using SocialNetwork.Entities.SecurityModels;
using System;
using System.Collections.Generic;

namespace SocialNetwork.Entities.Models
{
    public class User : ParentModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public List<Post> Posts { get; set; }

        public List<User> Friends { get; set; }
        public List<User> MakedFriend { get; set; }

        public List<User> Subscribers { get; set; }
        public List<User> Subscribed { get; set; }

        public List<Chat> Chats { get; set; }
        public List<Message> Messages { get; set; }


        public string PasswordHash { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public string Username { get; set; }

        public string Role { get; set; } = Roles.User;
    }
}
