using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;

namespace SocialNetworks.Repository.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
