using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Tests
{
    public class InMemoryDatabase
    {
        protected InMemoryDatabase(DbContextOptions<RepositoryContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<RepositoryContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new RepositoryContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new User() { Username = "retur 1", Id = 1, IsEnable = true};
                var two = new User() { Username = "retur 2", Id = 2, IsEnable = true};
                var three = new User() { Username = "retur 3", Id = 3, IsEnable = true};

                context.AddRange(one, two, three);

                context.SaveChanges();
            }
        }
    }
}
