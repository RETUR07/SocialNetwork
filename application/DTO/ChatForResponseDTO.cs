using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTO
{
    public class ChatForResponseDTO
    {
        public int Id { get; set; }
        public IEnumerable<int> Users { get; set; }
        public IEnumerable<MessageForResponseDTO> Messages { get; set; }
    }
}
