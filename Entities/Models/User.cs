using System;
using System.Collections.Generic;

namespace SocialNetwork.Entities.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public virtual List<User> Friends { get; set; }
        public virtual List<User> Subscribers { get; set; }
    }
}
