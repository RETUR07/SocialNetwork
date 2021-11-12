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

        public async Task<IEnumerable<Post>> GetAllPostsAsync(int userId, bool trackChanges) =>
            await FindByCondition(p => p.Author.Id == userId, trackChanges).OrderBy(p => p.ParentPost.Id).ToListAsync();

        public async Task<Post> GetPostAsync(int postId, bool trackChanges) =>
            await FindByCondition(p => p.Id == postId, trackChanges)
            .Include(p => p.BlobIds)
            .Include(p => p.Author)
            .Include(p => p.ParentPost)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Post>> GetChildrenPostsByPostIdAsync(int postId, bool trackChanges) =>
            await FindByCondition(r => r.ParentPost.Id == postId, trackChanges).ToListAsync();

    }
}
