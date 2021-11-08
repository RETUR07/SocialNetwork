using AutoMapper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class PostForm
    {
        public string Header { get; set; }

        public string Text { get; set; }

        public IEnumerable<Blob> Content { get; set; }
    }
}
