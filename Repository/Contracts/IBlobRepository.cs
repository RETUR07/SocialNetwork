using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepository.Contracts
{
    public interface IBlobRepository : IRepositoryBase<Blob>
    {
        public void CreateBlob(Blob blob);
        public Task<Blob> GetBlob(int blobId, bool trackChanges);
    }
}
