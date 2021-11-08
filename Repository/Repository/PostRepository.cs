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

        public void CreatePost(Post post) =>
            Create(post);

        public void DeletePost(Post post) =>
            Delete(post);

        public async Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(p => p.Header).ToListAsync();


        public async Task<Post> GetPostAsync(int postId, bool trackChanges) =>
                        await FindByCondition(p => p.Id == postId, trackChanges).SingleOrDefaultAsync();

    }
}
