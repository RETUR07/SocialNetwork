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
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(p => p.Header).ToListAsync();


        public async Task<Post> GetPostAsync(int postId, bool trackChanges) =>
                        await FindByCondition(p => p.Id == postId, trackChanges).Include(p => p.BlobIds).SingleOrDefaultAsync();

    }
}
