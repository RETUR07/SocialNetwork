using SocialNetwork.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post>> GetAllPostsAsync(int userId, bool trackChanges);
        public Task<IEnumerable<Post>> GetChildrenPostsByPostIdAsync(int postId, bool trackChanges);
        Task<Post> GetPostAsync(int postId, bool trackChanges);
    }
}
