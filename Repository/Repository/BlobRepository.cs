using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ProjectRepository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepository.Repository
{
    public class BlobRepository : RepositoryBase<Blob>, IBlobRepository
    {
        public BlobRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateBlob(Blob blob) => Create(blob);

        public async Task<Blob> GetBlob(int blobId, bool trackChanges) =>
             await FindByCondition(b => b.Id == blobId, trackChanges).SingleOrDefaultAsync();

    }
}
