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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Rate>()
                .Property(e => e.LikeStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (LikeStatus)Enum.Parse(typeof(LikeStatus), v));

            modelBuilder
                .Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany(u => u.MakedFriend);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Subscribers)
                .WithMany(u => u.Subscribed);

            modelBuilder
                .Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.ParentPost);
        }
    }
}
