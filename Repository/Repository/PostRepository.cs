using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
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
