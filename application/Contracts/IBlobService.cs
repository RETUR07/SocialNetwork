using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IBlobService
    {
        public Task<IEnumerable<FileContentResult>> GetBLobsAsync(IEnumerable<int> ids, bool trackChanges);
        public Task<IEnumerable<int>> SaveBlobsAsync(IEnumerable<IFormFile> formFile);
    }
}
