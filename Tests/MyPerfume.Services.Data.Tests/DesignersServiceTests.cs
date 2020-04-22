namespace MyPerfume.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyPerfume.Data;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Data.Repositories;
    using Xunit;

    public class DesignersServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Designer>>();
            repository.Setup(r => r.All()).Returns(new List<Designer>
                                                        {
                                                            new Designer(),
                                                            new Designer(),
                                                            new Designer(),
                                                        }.AsQueryable());
            var service = new DesignersService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DesignersTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer());
            dbContext.Designers.Add(new Designer());
            dbContext.Designers.Add(new Designer());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
