using System;
using Template.Infrastructure.Repositories;
using Xunit;

namespace Infrastructure.Tests
{
    public class PongRepositoryTests
    {
        [Fact]
        public void ShouldReturnPong()
        {
            var repo = new PongRepository();
            var result = repo.Get();
            Assert.Equal("Pong", result);
        }
    }
}