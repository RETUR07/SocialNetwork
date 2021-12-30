﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IBlobService
    {
        public Task<List<FileContentResult>> GetBLobsAsync(IEnumerable<int> ids, bool trackChanges);
        public Task<List<Blob>> SaveBlobsAsync(IEnumerable<IFormFile> formFile, string uniqueID);
        public  Task<List<List<FileContentResult>>> GetBLobsAsync(IEnumerable<IEnumerable<int>> collection, bool trackChanges);
    }
}
