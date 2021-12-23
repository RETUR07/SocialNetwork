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


        public new async void Delete(Post entity)
        {
            if (entity != null)
            {
                foreach (var e in await GetChildrenPostsByPostIdAsync(entity.Id, true))
                {
                    Delete(e);
                }
                base.Delete(entity);
            }
        }

        public async Task<List<Post>> GetAllPostsAsync(int userId, bool trackChanges) =>
            await FindByCondition(p => p.Author.Id == userId, trackChanges)
            .Include(p => p.Comments)
            .Include(p => p.ParentPost)
            .Include(p => p.BlobIds)
            .OrderBy(p => p.ParentPost.Id).ToListAsync();

        public async Task<Post> GetPostAsync(int postId, bool trackChanges) =>
            await FindByCondition(p => p.Id == postId, trackChanges)
            .Include(p => p.BlobIds)
            .Include(p => p.Author)
            .Include(p => p.ParentPost)
            .Include(p => p.Comments)
            .Include(p => p.ParentPost)
            .SingleOrDefaultAsync();

        public async Task<List<Post>> GetChildrenPostsByPostIdAsync(int postId, bool trackChanges) =>
            await FindByCondition(r => r.ParentPost.Id == postId, trackChanges).Include(x => x.BlobIds).ToListAsync();

    }
}
