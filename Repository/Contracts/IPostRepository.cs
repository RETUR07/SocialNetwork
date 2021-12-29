using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        public Task<PagedList<Post>> GetAllPostsPagedAsync(int userId, Parameters parameters, bool trackChanges);
        public Task<List<Post>> GetChildrenPostsByPostIdAsync(int postId, bool trackChanges);
        public Task<PagedList<Post>> GetChildrenPostsByPostIdPagedAsync(int postId, Parameters parameters, bool trackChanges);
        public Task<Post> GetPostAsync(int postId, bool trackChanges);
    }
}
