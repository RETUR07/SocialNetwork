using Microsoft.EntityFrameworkCore;
using Moq;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using SocialNetworks.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        [Fact]
        public void MockUnitTest()
        {
            var testObject = new User() { Id = 1 };
            var testList = new List<User>() { testObject };

            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Provider).Returns(testList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Expression).Returns(testList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(testList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());

            var context = new Mock<RepositoryContext>(new DbContextOptionsBuilder().Options);
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            var repository = new UserRepository(context.Object);
            var result = repository.FindByCondition(x=>x.Id == 1, false);

            Assert.Equal(testList, result.ToList());
        }
    }
}
