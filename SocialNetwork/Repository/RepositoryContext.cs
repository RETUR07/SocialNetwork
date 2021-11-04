using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;

namespace SocialNetwork.Contracts
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
