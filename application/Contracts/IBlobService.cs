using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IBlobService
    {
        public Task<IEnumerable<IFormFile>> GetBLobsAsync(IEnumerable<int> Ids, bool trackChanges);
        public Task<IEnumerable<int>> SaveBlobsAsync(IEnumerable<IFormFile> formFile);
    }
}
