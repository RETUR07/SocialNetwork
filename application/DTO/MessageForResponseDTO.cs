using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTO
{
    public class MessageForResponseDTO
    {
        public int From { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public IEnumerable<FileContentResult> Content { get; set; }
    }
}
