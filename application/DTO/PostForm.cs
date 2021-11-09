using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SocialNetwork.Application.DTO
{
    public class PostForm
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public IEnumerable<IFormFile> Content { get; set; }

    }
}
