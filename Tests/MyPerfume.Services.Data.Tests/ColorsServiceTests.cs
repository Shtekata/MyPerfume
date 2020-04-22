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

    public class ColorsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Color>>();
            repository.Setup(r => r.All()).Returns(new List<Color>
                                                        {
                                                            new Color(),
                                                            new Color(),
                                                            new Color(),
                                                        }.AsQueryable());
            var service = new ColorsService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ColorsTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Colors.Add(new Color());
            dbContext.Colors.Add(new Color());
            dbContext.Colors.Add(new Color());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Color>(dbContext);
            var service = new ColorsService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
