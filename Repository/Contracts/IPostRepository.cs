using SocialNetwork.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges);
        Task<Post> GetPostAsync(int postId, bool trackChanges);
    }
}
