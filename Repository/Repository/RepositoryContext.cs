using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectRepository.Repository
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
