﻿using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class PostForResponseDTO
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public string Text { get; set; }

        public IEnumerable<FileContentResult> Content { get; set; }
    }
}
