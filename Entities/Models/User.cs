using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{ 
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<Post> posts { get; set; }
    }
}
