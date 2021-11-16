using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Entities.Models
{
    public class Chat : ParentModel
    {
        public virtual List<User> Users { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
