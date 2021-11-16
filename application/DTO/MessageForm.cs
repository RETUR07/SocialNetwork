using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTO
{
    public class MessageForm
    {
        public int From { get; set; }
        public string Text { get; set; }
        public IEnumerable<IFormFile> Content { get; set; }
        public int ChatId { get; set; }
    }
}
