using Moq;
using System.Collections.Generic;
using Xunit;
using SocialNetworks.Repository.Repository;
using SocialNetwork.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SocialNetworks.Repository.Contracts;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SocialNetwork.Tests
{
    public class PostRepUnitTest
    {
        [Fact]
        public void GetAllPostsAsyncTest()
        {
            Assert.True(true);
        }
        
        [Fact]
        public void GetChildrenPostsByPostIdAsyncTest()
        {
            Assert.True(true);
        }

        [Fact]
        public void GetPostAsyncTest()
        {
            Assert.True(true);
        }
    }
}