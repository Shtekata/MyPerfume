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

    public class PerfumersServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Perfumer>>();
            repository.Setup(r => r.All()).Returns(new List<Perfumer>
                                                        {
                                                            new Perfumer(),
                                                            new Perfumer(),
                                                            new Perfumer(),
                                                        }.AsQueryable());
            var service = new PerfumersService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PerfumersTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer());
            dbContext.Perfumers.Add(new Perfumer());
            dbContext.Perfumers.Add(new Perfumer());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
