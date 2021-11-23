using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SocialNetwork.Tests.RepUnitTests
{
    public class RepBaseUnitTest : InMemoryDatabase
    {
        public RepBaseUnitTest()
        : base(
            new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Filename=RepBaseTest.db")
                .Options)
        {
        }

        [Fact]
        public void FindAllUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = userRep.FindAll(false);
                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void FindByConditionUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = userRep.FindByCondition(x => x.Id == 1, false);
                Assert.Equal(1, result.Count());
                Assert.Equal(1, result.First().Id);
            }
        }

        [Fact]
        public async Task CreateUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                userRep.Create(new User() { Username = "retur007" });
                await repositoryContext.SaveChangesAsync();
                var result = userRep.FindByCondition(x => x.Username == "retur007", false);
                Assert.Equal(1, result.Count());
                Assert.Equal(4, result.First().Id);
            }
        }

        [Fact]
        public async Task UpdateUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = await userRep.FindByCondition(x => x.Id == 1, false).SingleOrDefaultAsync();
                result.Username = "no retur";
                userRep.Update(result);
                await repositoryContext.SaveChangesAsync();
                var afterChange = await userRep.FindByCondition(x => x.Id == 1, false).SingleOrDefaultAsync();
                Assert.Equal(1, afterChange.Id);
                Assert.Equal("no retur", afterChange.Username);
            }
        }

        [Fact]
        public async Task DeleteUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = await userRep.FindByCondition(x => x.Id == 1, false).SingleOrDefaultAsync();
                userRep.Delete(result);
                await repositoryContext.SaveChangesAsync();
                var afterChange = await userRep.FindByCondition(x => x.Id == 1, false).SingleOrDefaultAsync();
                Assert.Null(afterChange);
            }
        }

        [Fact]
        public async Task NotSoftDeleteUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = await userRep.FindByCondition(x => x.Id == 1, false).SingleOrDefaultAsync();
                userRep.Delete(result);
                await repositoryContext.SaveChangesAsync();
                var afterChange = await userRep.FindByCondition(x => x.Id == 1, false).SingleOrDefaultAsync();
                Assert.Null(afterChange);
            }
        }
    }
}
