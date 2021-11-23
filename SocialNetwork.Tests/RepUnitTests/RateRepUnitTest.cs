using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SocialNetwork.Tests
{
    public class RateRepUnitTest : InMemoryDatabase
    {
        public RateRepUnitTest()
        : base(
            new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Filename=RateRepTest.db")
                .Options)
        {
        }

        [Fact]
        public async Task GetRatesByPostIdAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var rateRep = new RateRepository(repositoryContext);
                var result = await rateRep.GetRatesByPostIdAsync(2, false);
                Assert.Equal(2, result.Count);
                Assert.Contains("retur 1", result.Select(x => x.User.Username));
                Assert.Contains("retur 2", result.Select(x => x.User.Username));
            }
        }

        [Fact]
        public async Task GetPostRateAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var rateRep = new RateRepository(repositoryContext);
                var result = await rateRep.GetPostRateAsync(2, 2, false);
                Assert.Equal("retur 2", result.User.Username);
                Assert.Equal("2", result.Post.Header);
            }
        }
    }
}
