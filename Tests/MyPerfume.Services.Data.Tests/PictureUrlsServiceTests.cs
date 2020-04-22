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

    public class PictureUrlsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            repository.Setup(r => r.All()).Returns(new List<PictureUrl>
                                                        {
                                                            new PictureUrl(),
                                                            new PictureUrl(),
                                                            new PictureUrl(),
                                                        }.AsQueryable());
            var service = new PictureUrlsService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PictureUrlsTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl());
            dbContext.PicturesUrls.Add(new PictureUrl());
            dbContext.PicturesUrls.Add(new PictureUrl());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
