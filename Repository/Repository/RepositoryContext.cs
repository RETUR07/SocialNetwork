using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using System;

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
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Rate>()
                .Property(e => e.LikeStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (LikeStatus)Enum.Parse(typeof(LikeStatus), v));
        }
    }
}
